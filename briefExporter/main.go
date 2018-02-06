package main

import (
	"runtime"
	"log"
	"flag"
	"os"
	"brief/briefExporter/exporters"
	"brief/briefExporter/common"
	"brief/briefExporter/connectivity"
)

func main() {
	runtime.GOMAXPROCS(2)

	var dataPath string
	flag.StringVar(&dataPath, "data_path", "", "path to data file")

	var configPath string
	flag.StringVar(&configPath, "config_path", "", "path to config file")

	logFlag := flag.Bool("log", false, "true if provide logs in output")
	flag.Parse()

	if dataPath == "" || configPath == ""{
		log.Fatalln("configPath and dataPath should been provided!")
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

	config, err := common.GetConfig(configPath)
	common.Check(err)
	log.Println(config)

	var matcher exporters.KindleExporter

	notes, err := matcher.GetNotes(dataPath)
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

	kindleUsb := &connectivity.KindleUsbConnector{}
	log.Println(kindleUsb.GetNotesFromDevice("G090G10560350NP9"))

	/*libDir := &libsync.Directory{Path:config.ScanFolder}
	libsync.CheckPath(libDir)
	libDir.PrintStructure(nil)*/
}

