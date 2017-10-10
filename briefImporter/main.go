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
	BookAuthor []Author
}

type Author struct {
	FirstName string
	SecondName string
	PaternalName string
	Surname string
}

func main() {
	b, err := ioutil.ReadFile("D:\\brief\\unittestdata\\My Clippings.txt")
	if err != nil {
		fmt.Print(err)
	}

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
		params := getParams(recordRegexp, splitted[i])

		fmt.Printf("\nBook name: %s, publishing year: %s, original title: %s, book author: %s, on page: %s, locations: %s - %s \n", params["title"],
			params["publishingyear"],
			params["alttitle"],
			params["author"],
		    params["page"],
			params["location"],
			params["slocation"])
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