package common

import (
	"io/ioutil"
	"encoding/json"
	"log"
	"os"
	"runtime"
)

type Config struct {
	HostAddress string  `json:"host_address"`
	RetrieveUrl string  `json:"retrieve_url"`
	SendUrl string      `json:"send_url"`
	ScanFolder string 	`json:"scan_folder"`
}

func GetConfig(path string) (*Config, error) {
	var config *Config

	data, err := ioutil.ReadFile(path)
	Check(err)

	err = json.Unmarshal(data, &config)

	return config, err
}

func GetFileData(path string) ([]byte, error){
	return ioutil.ReadFile(path)
}

func Check(err error) {
	if err != nil {
		log.Fatalln(err)
		os.Exit(1)
	}
}

func GetSystemPathDelimiter() string {
	switch os := runtime.GOOS; os {
	case "darwin":
		return "/"
	case "linux":
		return "/"
	default:
		return "\\"
	}
}