package connectivity

import "brief/briefExporter/common"

type Connector interface {
	GetNotesFromDevice(serialNumber string, config *common.Config) (string, error)
}