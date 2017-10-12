package main

import (
	"fmt"
	"io/ioutil"
	"regexp"
	"os"
	"brief/briefImporter/common"
)

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

func main() {
	if len(os.Args) != 2 {
		fmt.Fprintf(os.Stderr, "Usage: <textfilepath>\n")
		os.Exit(1)
	}

	b, err := ioutil.ReadFile(os.Args[1])
	common.Check(err)

	str := string(b)

	recordRegexp := regexp.MustCompile(`(g?)(i?)(?P<title>[\wА-Яа-яіїєґ'#\-*:*\s*\.*\,*]+)\s` +
		                                      `(?P<alttitle>\({1}[\wА-Яа-яіїєґ\s*\.*\,*]+\){1})?\s?` +
		                                      `(\({1}(?P<author>[\wА-Яа-яіїєґ\;*\s*\.*\,*]+)\){1}){1}` +
		                                      `[\r\n]*-\sYour\s(Note|Highlight)\son\s` +
		                                      `(page\s(?P<page>[\d]+)\s\|\s)?` +
		                                      `Location\s(?P<location>[\d]+)\-?(?P<slocation>[\d]+)?\s\|\sAdded\son\s` +
		                                      `(?P<createdondate>[\w]+\,{1}\s[\w]+\s[\d]+\,\s\d{4})`+
		                                      `\s(?P<createdontime>\d{1,2}:\d{2}:\d{2}\s(AM|PM))` +
		                                      `[\r\n]*(?P<notedata>[\wА-Яа-яіїєґ'#\-*:*\s*\.*\,*]+)`)

	splitted := regexp.MustCompile("[==========]+").Split(str, -1)

	var notes []NoteRecord
	for i:= 0; i < len(splitted) - 1; i = i + 2 {
		noteTitleGroups := common.GetGroupsData(recordRegexp, splitted[i])
		noteDataGroups := common.GetGroupsData(recordRegexp, splitted[i+1])

		note := NoteRecord{}

		if noteTitleGroups["title"] != "" &&  noteTitleGroups["title"] == noteDataGroups["title"]  {
			note.BookTile = noteTitleGroups["title"]
			notes = append(notes, note)
		} else {
			continue
		}
	}

	fmt.Println(notes)
}
