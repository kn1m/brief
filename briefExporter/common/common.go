package common

import (
	"encoding/json"
	"io/ioutil"
	"log"
	"os"
	"runtime"
)

type Config struct {
	NotesRetrieveUrl           string `json:"retrieve_url"`
	NotesSendUrl               string `json:"send_url"`
	LibraryCheckUrl            string `json:"library_check_url"`
	LibrarySyncUrl             string `json:"library_sync_url"`
	ScanFolder                 string `json:"scan_folder"`
	ScanMountPathScript        string `json:"scan_mount_path_script"`
}

func GetConfig(path string) (*Config, error) {
	var config *Config

	data, err := ioutil.ReadFile(path)
	Check(err)

	err = json.Unmarshal(data, &config)

	return config, err
}

func GetFileData(path string) ([]byte, error) {
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
