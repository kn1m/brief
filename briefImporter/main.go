package main

import (
	"fmt"
	"os"
	"brief/briefImporter/exporters"
	"brief/briefImporter/common"
)

func main() {
	if len(os.Args) != 2 {
		fmt.Fprintf(os.Stderr, "Usage: <textfilepath>\n")
		os.Exit(1)
	}

	notes, err := exporters.GetNotesFromFile(os.Args[1])
	common.Check(err)

	fmt.Println(notes)
}

