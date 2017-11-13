package main

import (
	"brief/briefImporter/exporters"
	"brief/briefImporter/common"
	"runtime"
	"log"
	"flag"
	"os"
)

func main() {
	runtime.GOMAXPROCS(runtime.NumCPU())

	var data_path string
	flag.StringVar(&data_path, "data_path", "", "path to data file")

	var config_path string
	flag.StringVar(&config_path, "config_path", "", "path to config gile")

	logFlag := flag.Bool("log", false, "true if provide logs in output")
	flag.Parse()

	if data_path == "" || config_path == ""{
		log.Fatalln("config_path and data_path should been provided!")
		os.Exit(1)
	}

	var mem runtime.MemStats

	if *logFlag {
		runtime.ReadMemStats(&mem)
		log.Println(mem.Alloc)
		log.Println(mem.TotalAlloc)
		log.Println(mem.HeapAlloc)
		log.Println(mem.HeapSys)
	}

	config, err := common.GetConfig(config_path)
	common.Check(err)
	log.Println(config)

	notes, err := exporters.GetNotes(data_path)
	common.Check(err)

	for i := range notes {
		log.Printf("\n%d: %s %s %+v p: %d-%d l:%d-%d :: %s :: %s %s", i, notes[i].BookTitle,
			notes[i].BookOriginalName, notes[i].BookAuthor, notes[i].FirstPage, notes[i].SecondPage,
			notes[i].FirstLocation, notes[i].SecondLocation, notes[i].NoteTitle, notes[i].NoteText, notes[i].CreatedOn)
	}

	if *logFlag {
		runtime.ReadMemStats(&mem)
		log.Println(mem.Alloc)
		log.Println(mem.TotalAlloc)
		log.Println(mem.HeapAlloc)
		log.Println(mem.HeapSys)
	}
}

