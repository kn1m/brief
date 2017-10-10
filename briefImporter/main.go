package main

import (
	"fmt"
	"io/ioutil"
	"regexp"
	"os"
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
	check(err)

	str := string(b)

	recordRegexp := regexp.MustCompile(`(g?)(?P<title>[\wА-Яа-я#\-*:*\s*\.*\,*]+)\s` +
		                                      `(?P<alttitle>\({1}[\wА-Яа-я\s*\.*\,*]+\){1})?\s?` +
		                                      `(\({1}(?P<author>[\wА-Яа-я\;*\s*\.*\,*]+)\){1}){1}` +
		                                      `[\r\n]*-\sYour\s(Note|Highlight)\son\s` +
		                                      `(page\s(?P<page>[\d]+)\s\|\s)?` +
		                                      `Location\s(?P<location>[\d]+)\-?(?P<slocation>[\d]+)?\s\|\sAdded\son\s`)

	splitted := regexp.MustCompile("[==========]+").Split(str, -1)

	for i:= range splitted {
		fmt.Printf("record: %d", i)
		groups := getGroupsData(recordRegexp, splitted[i])

		fmt.Printf("\nBook name: %s, publishing year: %s, original title: %s, book author: %s, on page: %s, locations: %s - %s \n",
			groups["title"],
			groups["publishingyear"],
			groups["alttitle"],
			groups["author"],
			groups["page"],
			groups["location"],
			groups["slocation"])
	}
}

func getGroupsData(regEx *regexp.Regexp, matchedString string) (paramsMap map[string]string) {
	match := regEx.FindStringSubmatch(matchedString)

	paramsMap = make(map[string]string)
	for i, name := range regEx.SubexpNames() {
		if i > 0 && i <= len(match) {
			paramsMap[name] = match[i]
		}
	}
	return
}

func check(e error) {
	if e != nil {
		panic(e)
	}
}
