package exporters

type BaseNote struct {
	BookTitle        string `json:"book_title"`
	BookOriginalName string `json:"book_original_name"`
	CreatedOn        string `json:"created_on"`
	NoteTitle        string `json:"note_title"`
	NoteText         string `json:"note_text"`
}

type Exporter interface {
	GetNotes(path string) ([]*NoteRecord, error)
}