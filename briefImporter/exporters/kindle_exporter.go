package exporters

import (
	"errors"
	"strings"
	"regexp"
	"io/ioutil"
	"brief/briefImporter/common"
	"sort"
	"strconv"
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

type NoteRecord struct
{
	BookTile string
	BookOriginalName string
	BookAuthor []Author
	FirstPage int
	SecondPage int
	FirstLocation int
	SecondLocation int
	CreatedOn string
	NoteTitle string
	NoteText string
}

type Author struct {
	FirstName string
	SecondName string
	PaternalName string
	Surname string
}

type NoteData struct {
	titleNoteData map[string]string
	noteData map[string]string
}

func GetNotesFromFile(path string) ([]NoteRecord, error){

	b, err := ioutil.ReadFile(path)
	common.Check(err)

	str := string(b)

	recordRegexp := regexp.MustCompile(`(g?)(i?)(?P<`+ titleGroupName +`>[\wА-Яа-яіїєґ'#\-*:*\s*\.*\,*]+)\s` +
		`(?P<`+ alttitleGroupName +`>\({1}[\wА-Яа-яіїєґ\s*\.*\,*]+\){1})?\s?` +
		`(\({1}(?P<`+ authorGroupName +`>[\wА-Яа-яіїєґ\;*\s*\.*\,*]+)\){1}){1}` +
		`[\r\n]*-\sYour\s(Note|Highlight)\son\s` +
		`(page\s(?P<`+ pageGroupName +`>[\d]+)\s\|\s)?` +
		`Location\s(?P<`+ firstLocationGroupName +`>[\d]+)\-?`+
		`(?P<`+ secondLocationGroupName +`>[\d]+)?\s\|\sAdded\son\s` +
		`(?P<`+ createdOnDateGroupName +`>[\w]+\,{1}\s[\w]+\s[\d]+\,\s\d{4})`+
		`\s(?P<`+ createdOnTimeGroupName +`>\d{1,2}:\d{2}:\d{2}\s(AM|PM))` +
		`[\r\n]*(?P<`+ noteDataGroupName +`>[\wА-Яа-яіІїЇєЄґҐ\s*\'*#*\(*\)*\-*:*\*\;*\=*\.*\,*—*]+)[\r\n]*`)

	splitted := regexp.MustCompile("={10}[\r\n]*").Split(str, -1)

	var notes []NoteRecord

	i := 0
	for i < len(splitted) - 1 {
		var noteData NoteData
		titleGroup := common.GetGroupsData(recordRegexp, splitted[i])

		//handle of Bookmark records
		if len(titleGroup) == 0 {
			i++
			continue
		}

		noteData.titleNoteData = titleGroup
		noteData.noteData = common.GetGroupsData(recordRegexp, splitted[i+1])
		note := &NoteRecord{}

		checkNoteFiled(noteData, note.checkTitle, note.checkAltTitle, note.checkPageOrLocations, note.checkNoteTitleAndText)
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
		note.BookTile = data.titleNoteData[titleGroupName]
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
		if len(authors) != 0 {
			for i := range authors {
				parsedAuthor := strings.Split(authors[i], ",")
				if len(parsedAuthor) != 0 {
					sort.Sort(sort.Reverse(sort.StringSlice(parsedAuthor)))
				} else {

				}

				bookAuthor := Author{FirstName: authors[i]}
				note.BookAuthor = append(note.BookAuthor, bookAuthor)

			}
		} else {

		}

		return note, nil
	}
	return note, errors.New(authorGroupName + " could not be processed further!")
}

func (note *NoteRecord) checkPageOrLocations(data NoteData) (*NoteRecord, error) {
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
	return note, errors.New(pageGroupName + " could not be processed further!")
}

func (note *NoteRecord) checkNoteTitleAndText(data NoteData) (*NoteRecord, error) {
	if data.noteData[noteDataGroupName] != "" {
		note.NoteTitle = data.titleNoteData[noteDataGroupName]
		note.NoteText = data.noteData[noteDataGroupName]
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
