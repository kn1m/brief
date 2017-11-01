package exporters

import (
	"errors"
	"strings"
	"regexp"
	"brief/briefImporter/common"
	"strconv"
	"os"
	"log"
	"io"
	"crypto/md5"
)

const titleGroupName = "title"
const alttitleGroupName = "alttitle"
const authorGroupName = "author"
const pageGroupName = "page"
const firstLocationGroupName = "location"
const secondLocationGroupName = "slocation"
const createdOnDateGroupName = "createdondate"
const createdOnTimeGroupName = "createdontime"
const noteDataGroupName = "notedata"
const recordTypeGroupName  = "recordtype"

var recordTypesToSkip  = []string{"Highlight", "Bookmark"}

type NoteRecord struct
{
	BookTitle string        `json:"book_title"`
	BookOriginalName string `json:"book_original_name"`
	BookAuthor []Author     `json:"authors"`
	FirstPage int 			`json:"first_page"`
	SecondPage int          `json:"second_page"`
	FirstLocation int       `json:"first_location"`
	SecondLocation int      `json:"second_location"`
	CreatedOn string        `json:"created_on"`
	NoteTitle string        `json:"note_title"`
	NoteText string         `json:"note_text"`
}

type Author struct {
	FirstName string
	SecondaryName string
	Surname string
}

type NoteData struct {
	titleNoteData map[string]string
	noteData map[string]string
}

func GetPaperwhiteNotesFromFile(path string) ([]NoteRecord, error){

	file, err := os.Open(path)
	common.Check(err)
	defer file.Close()

	stat, _ := file.Stat()
	file_data := make([]byte, stat.Size())

	file.Read(file_data)

	h := md5.New()
	if _, err := io.Copy(h, file); err != nil {
		log.Fatal(err)
	}

	log.Printf("Current file hash: %x\n", h.Sum(nil))

	str := string(file_data)

	recordRegexp := regexp.MustCompile(`(g?)(i?)(?P<`+ titleGroupName +`>[\wА-Яа-яіїєґ'#\-*:*\s*\.*\,*]+)\s` +
		`(?P<`+ alttitleGroupName +`>\({1}[\wА-Яа-яіїєґ\s*\.*\,*]+\){1})?\s?` +
		`(\({1}(?P<`+ authorGroupName +`>[\wА-Яа-яіїєґ\;*\s*\.*\,*]+)\){1}){1}` +
		`[\r\n]*-\sYour\s(?P<`+ recordTypeGroupName +`>(Note|Highlight|Bookmark))\son\s` +
		`(page\s(?P<`+ pageGroupName +`>[\d]+)\s\|\s)?` +
		`Location\s(?P<`+ firstLocationGroupName +`>[\d]+)\-?`+
		`(?P<`+ secondLocationGroupName +`>[\d]+)?\s\|\sAdded\son\s` +
		`[\w]+\,{1}\s(?P<`+ createdOnDateGroupName +`>[\w]+\s[\d]+\,\s\d{4})`+
		`\s(?P<`+ createdOnTimeGroupName +`>\d{1,2}:\d{2}:\d{2}\s(AM|PM))` +
		`[\r\n]*(?P<`+ noteDataGroupName +`>[\S\s]+)[\r\n]*`)

	split := regexp.MustCompile("={10}[\r\n]*").Split(str, -1)

	var notes []NoteRecord

	i := 0
	for i < len(split) - 1 {
		var noteData NoteData
		titleGroup := common.GetGroupsData(recordRegexp, split[i])

		//handling of Highlights and Bookmarks
		if common.Contains(recordTypesToSkip, titleGroup[recordTypeGroupName]) {
			i++
			continue
		}

		noteData.titleNoteData = titleGroup
		noteData.noteData = common.GetGroupsData(recordRegexp, split[i+1])
		note := &NoteRecord{}

		checkNoteFiled(noteData, note.checkTitle, note.checkAltTitle,
			           note.checkAuthor, note.checkPage, note.checkLocations, note.checkDateAndTime, note.checkNoteTitleAndText)
		notes = append(notes, *note)

		i += 2
	}
	return notes, nil
}

func checkNoteFiled(data NoteData, fns ...func(data NoteData) (*NoteRecord, error)) (err error) {
	for _, fn := range fns {
		if _, err = fn(data); err != nil {
			break
		}
	}
	return
}

func (note *NoteRecord) checkTitle(data NoteData) (*NoteRecord, error) {
	if baseNoteFieldCheck(data, titleGroupName, false) {
		note.BookTitle = data.titleNoteData[titleGroupName]
		return note, nil
	}
	return note, errors.New(titleGroupName + " could not be processed further!")
}

func (note *NoteRecord) checkAltTitle(data NoteData) (*NoteRecord, error) {
	if baseNoteFieldCheck(data, alttitleGroupName, true){
		note.BookOriginalName = data.titleNoteData[alttitleGroupName]
		return note, nil
	}
	return note, errors.New(alttitleGroupName + " could not be processed further!")
}

func (note *NoteRecord) checkAuthor(data NoteData) (*NoteRecord, error) {
	if baseNoteFieldCheck(data, authorGroupName, false){
		authors := strings.Split(data.titleNoteData[authorGroupName], ";")
		if len(authors) > 1 {
			for i := range authors {
				parsedAuthor := regexp.MustCompile(",{1}\\s*").Split(authors[i], -1)
				if len(parsedAuthor) > 1 {
					common.Reverse(parsedAuthor)

					author, err := handleAuthor(parsedAuthor)
					if err != nil {
						return note, errors.New(authorGroupName + " could not be processed further!")
					}
					note.BookAuthor = append(note.BookAuthor, *author)

				} else {
					authorData := strings.Split(data.titleNoteData[authorGroupName], " ")

					author, err := handleAuthor(authorData)
					if err != nil {
						return note, errors.New(authorGroupName + " could not be processed further!")
					}
					note.BookAuthor = append(note.BookAuthor, *author)
				}
			}
		} else {
			parsedAuthor := regexp.MustCompile(",{1}\\s*").Split(data.titleNoteData[authorGroupName], -1)
			if len(parsedAuthor) > 1 {
				common.Reverse(parsedAuthor)

				author, err := handleAuthor(parsedAuthor)
				if err != nil {
					return note, errors.New(authorGroupName + " could not be processed further!")
				}
				note.BookAuthor = append(note.BookAuthor, *author)
			} else {
				authorData := strings.Split(data.titleNoteData[authorGroupName], " ")

				author, err := handleAuthor(authorData)
				if err != nil {
					return note, errors.New(authorGroupName + " could not be processed further!")
				}

				note.BookAuthor = append(note.BookAuthor, *author)
			}
		}
		return note, nil
	}
	return note, errors.New(authorGroupName + " could not be processed further!")
}

func (note *NoteRecord) checkPage(data NoteData) (*NoteRecord, error) {
	var err error
	if data.noteData[pageGroupName] != "" || data.titleNoteData[pageGroupName] != "" {
		if data.titleNoteData[pageGroupName] != "" {
			note.FirstPage, err = strconv.Atoi(data.titleNoteData[pageGroupName])
		}
		if data.noteData[pageGroupName] != "" {
			note.SecondPage, err = strconv.Atoi(data.noteData[pageGroupName])
		}
		return note, err
	}
	return note, nil
}

func (note *NoteRecord) checkLocations(data NoteData) (*NoteRecord, error) {
	var err error
	if data.noteData[firstLocationGroupName] != "" || data.titleNoteData[firstLocationGroupName] != "" {
		if data.titleNoteData[firstLocationGroupName] != "" {
			note.FirstLocation, err = strconv.Atoi(data.titleNoteData[firstLocationGroupName])
		}
		if data.noteData[secondLocationGroupName] != "" {
			note.SecondLocation, err = strconv.Atoi(data.noteData[secondLocationGroupName])
		}
		return note, err
	}
	return note, errors.New(firstLocationGroupName + " could not be processed further!")
}

func (note *NoteRecord) checkNoteTitleAndText(data NoteData) (*NoteRecord, error) {
	if data.noteData[noteDataGroupName] != "" {
		note.NoteTitle = data.titleNoteData[noteDataGroupName]
		note.NoteText = data.noteData[noteDataGroupName]
		return note, nil
	}
	return note, errors.New(noteDataGroupName + " could not be processed further!")
}

func (note *NoteRecord) checkDateAndTime(data NoteData) (*NoteRecord, error) {
	if baseNoteFieldCheck(data, createdOnDateGroupName, false) || baseNoteFieldCheck(data, createdOnTimeGroupName, false) {
		note.CreatedOn = data.noteData[createdOnDateGroupName] + " " + data.noteData[createdOnTimeGroupName]
		return note, nil
	}
	return note, errors.New(noteDataGroupName + " could not be processed further!")
}

func baseNoteFieldCheck(data NoteData, groupName string, isOptional bool) bool {
	if !isOptional {
		if data.titleNoteData[groupName] != "" && data.titleNoteData[groupName] == data.noteData[groupName] {
			return true
		}
	} else {
		return true
	}
	return false
}

func handleAuthor(authorData []string) (*Author, error) {
	author := &Author{}
	switch len(authorData) {
		case 1:
			author.FirstName = authorData[0]
		case 2:
			author.FirstName = authorData[0]
			author.Surname = authorData[1]
		case 3:
			author.FirstName = authorData[0]
			author.SecondaryName = authorData[1]
			author.Surname = authorData[2]
		default:
			return &Author{FirstName:"Authors unknown"}, nil
	}
	return author, nil
}
