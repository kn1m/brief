package exporters

import (
	"errors"
	"strings"
	"regexp"
	"io/ioutil"
	"brief/briefImporter/common"
)

const titleGroupName = "title"
const alttitleGroupName = "alttitle"
const authorGroupName = "author"

type NoteRecord struct
{
	BookTile string
	BookOriginalName string
	BookAuthor []Author
	Page int
	FirstLocation int
	SecondLocation int
	CreatedOn string
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

	recordRegexp := regexp.MustCompile(`(g?)(i?)(?P<`+ titleGroupName + `>[\wА-Яа-яіїєґ'#\-*:*\s*\.*\,*]+)\s` +
		`(?P<`+ alttitleGroupName+ `>\({1}[\wА-Яа-яіїєґ\s*\.*\,*]+\){1})?\s?` +
		`(\({1}(?P<`+ authorGroupName+ `>[\wА-Яа-яіїєґ\;*\s*\.*\,*]+)\){1}){1}` +
		`[\r\n]*-\sYour\s(Note|Highlight)\son\s` +
		`(page\s(?P<page>[\d]+)\s\|\s)?` +
		`Location\s(?P<location>[\d]+)\-?(?P<slocation>[\d]+)?\s\|\sAdded\son\s` +
		`(?P<createdondate>[\w]+\,{1}\s[\w]+\s[\d]+\,\s\d{4})`+
		`\s(?P<createdontime>\d{1,2}:\d{2}:\d{2}\s(AM|PM))` +
		`[\r\n]*(?P<notedata>[\wА-Яа-яіїєґ'#\-*:*\s*\.*\,*]+)`)

	splitted := regexp.MustCompile("={10}").Split(str, -1)

	var notes []NoteRecord
	for i:= 0; i < len(splitted) - 1; i = i + 2 {
		var noteData NoteData
		noteData.titleNoteData = common.GetGroupsData(recordRegexp, splitted[i])
		noteData.noteData = common.GetGroupsData(recordRegexp, splitted[i+1])

		note := &NoteRecord{}

		checkNoteFiled(noteData, note.checkTitle, note.checkAuhor)
		notes = append(notes, *note)
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
	if data.titleNoteData[titleGroupName] != "" && data.titleNoteData[titleGroupName] == data.noteData[titleGroupName] {
		note.BookTile = data.titleNoteData[titleGroupName]
	}
	return note, errors.New(titleGroupName + "Could not be processed further!")
}

func (note *NoteRecord) checkAuhor(data NoteData) (*NoteRecord, error) {
	if data.titleNoteData[authorGroupName] != "" && data.titleNoteData[authorGroupName] == data.noteData[authorGroupName] {
		authors := strings.Split(data.titleNoteData[authorGroupName], ";")
		for i := range authors {
			bookAuthor := Author{FirstName:authors[i]}
			note.BookAuthor = append(note.BookAuthor, bookAuthor)
		}
	}
	return note, errors.New(authorGroupName + "Could not be processed further!")
}
