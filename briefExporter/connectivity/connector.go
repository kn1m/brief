package connectivity

type Connector interface {
	GetNotesFromDevice(serialNumber string) string
}