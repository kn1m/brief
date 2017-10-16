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
	"strings"
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

	var k int
	for i := range notes {
		if strings.Contains(notes[i].BookTile,"Async") {
			k++
		}

		fmt.Printf("\n%d: %s %s %d-%d", i, notes[i].BookTile,
			notes[i].BookOriginalName, notes[i].FirstPage, notes[i].SecondPage)

	}

	if *logFlag {
		runtime.ReadMemStats(&mem)
		log.Println(mem.Alloc)
		log.Println(mem.TotalAlloc)
		log.Println(mem.HeapAlloc)
		log.Println(mem.HeapSys)
	}
}

