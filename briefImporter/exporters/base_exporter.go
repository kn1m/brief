package exporters

type Importer interface {
	GetNotes(path string) ([]NoteRecord, error)
}