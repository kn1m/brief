package main

import (
	"fmt"
	"io/ioutil"
	"regexp"
)

type NoteRecord struct
{
	BookTile string
	BookOriginalName string
	BookAuthor string
}

func main() {
	b, err := ioutil.ReadFile("D:\\brief\\unittestdata\\My Clippings.txt")
	if err != nil {
		fmt.Print(err)
	}

	str := string(b)

	recordRegexp := regexp.MustCompile(`(g?)(?P<title>[\wА-Яа-я#\-*:*\s*\.*\,*]+)\s` +
		`(?P<publishingyear>[\d]+)?\s?` +
		`(?P<alttitle>\({1}[\wА-Яа-я\s*\.*\,*]+\){1})?\s?`+
		`(?P<author>\({1}[\wА-Яа-я\;*\s*\.*\,*]+\){1}){1}`)

	splitted := regexp.MustCompile("[==========]+").Split(str, -1)

	for i:= range splitted {
		fmt.Printf("record: %d", i)
		params := getParams(recordRegexp, splitted[i])
		fmt.Printf("\nBook name: %s, publishing year: %s, original title: %s, book author: %s\n", params["title"],
			params["publishingyear"],
			params["alttitle"],
			params["author"])
	}

	//fmt.Println(str) // print the content as a 'string'
}

func getParams(regEx *regexp.Regexp, url string) (paramsMap map[string]string) {
	match := regEx.FindStringSubmatch(url)

	paramsMap = make(map[string]string)
	for i, name := range regEx.SubexpNames() {
		if i > 0 && i <= len(match) {
			paramsMap[name] = match[i]
		}
	}
	return
}