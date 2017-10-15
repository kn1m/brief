package main

import (
	"fmt"
	"os"
	"brief/briefImporter/exporters"
	"brief/briefImporter/common"
	"runtime"
	"log"
)

func main() {
	if len(os.Args) != 2 {
		fmt.Fprintf(os.Stderr, "Usage: <textfilepath>\n")
		os.Exit(1)
	}

	var mem runtime.MemStats
	runtime.ReadMemStats(&mem)
	log.Println(mem.Alloc)
	log.Println(mem.TotalAlloc)
	log.Println(mem.HeapAlloc)
	log.Println(mem.HeapSys)

	notes, err := exporters.GetNotesFromFile(os.Args[1])
	common.Check(err)

	fmt.Println(notes)

	runtime.ReadMemStats(&mem)
	log.Println(mem.Alloc)
	log.Println(mem.TotalAlloc)
	log.Println(mem.HeapAlloc)
	log.Println(mem.HeapSys)
}

