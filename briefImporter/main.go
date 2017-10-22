package main

import (
	"fmt"
	"brief/briefImporter/exporters"
	"brief/briefImporter/common"
	"runtime"
	"log"
	"flag"
	"errors"
    //"github.com/ahmetb/go-linq"
)

func main() {
	var filepath string
	flag.StringVar(&filepath, "filepath", "", "path to text file")
	logFlag := flag.Bool("log", false, "true if provide logs in output")
	flag.Parse()

	if filepath == "" {
		//fmt.Fprintf(os.Stderr, "Usage: <textfilepath>\n")
		//os.Exit(1)
		panic(errors.New("File must be provided!"))
	}

	var mem runtime.MemStats

	if *logFlag {
		runtime.ReadMemStats(&mem)
		log.Println(mem.Alloc)
		log.Println(mem.TotalAlloc)
		log.Println(mem.HeapAlloc)
		log.Println(mem.HeapSys)
	}

	notes, err := exporters.GetNotesFromFile(filepath)
	common.Check(err)

	for i := range notes {
		fmt.Printf("\n%d: %s %s %+v %d-%d \n:: %s:: %s", i, notes[i].BookTile,
			notes[i].BookOriginalName, notes[i].BookAuthor, notes[i].FirstPage, notes[i].SecondPage, notes[i].NoteTitle, notes[i].NoteText)
	}

	if *logFlag {
		runtime.ReadMemStats(&mem)
		log.Println(mem.Alloc)
		log.Println(mem.TotalAlloc)
		log.Println(mem.HeapAlloc)
		log.Println(mem.HeapSys)
	}
}

