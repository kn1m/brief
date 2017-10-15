package main

import (
	"fmt"
	"os"
	"brief/briefImporter/exporters"
)

func main() {
	if len(os.Args) != 2 {
		fmt.Fprintf(os.Stderr, "Usage: <textfilepath>\n")
		os.Exit(1)
	}

	notes, _ := exporters.GetNotesFromFile(os.Args[1])

	fmt.Println(notes)
}

