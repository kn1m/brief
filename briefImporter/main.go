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
	var path string
	flag.StringVar(&path, "filepath", "", "path to text file")
	logFlag := flag.Bool("log", false, "true if provide logs in output")
	flag.Parse()

	if path == "" {
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

	notes, err := exporters.GetPaperwhiteNotesFromFile(path)
	common.Check(err)

	for i := range notes {
		fmt.Printf("\n%d: %s %s %+v p: %d-%d l:%d-%d :: %s :: %s %s", i, notes[i].BookTitle,
			notes[i].BookOriginalName, notes[i].BookAuthor, notes[i].FirstPage, notes[i].SecondPage,
			notes[i].FirstLocation, notes[i].SecondLocation, notes[i].NoteTitle, notes[i].NoteText, notes[i].CreatedOn)
	}

	//json_notes, err := json.Marshal(notes)
	//common.Check(err)

	//net.SendNotesToServer(&json_notes)

	if *logFlag {
		runtime.ReadMemStats(&mem)
		log.Println(mem.Alloc)
		log.Println(mem.TotalAlloc)
		log.Println(mem.HeapAlloc)
		log.Println(mem.HeapSys)
	}
}

