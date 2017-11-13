package common

import (
	"io/ioutil"
	"encoding/json"
	"log"
	"os"
)

type Config struct{
	HostAddress string  `json:"host_address"`
	RetrieveUrl string  `json:"retrieve_url"`
	SendUrl string      `json:"send_url"`
}

func GetConfig(path string) (*Config, error) {
	var config *Config

	data, err := ioutil.ReadFile(path)
	Check(err)

	err = json.Unmarshal(data, &config)

	return config, err
}

func Check(err error) {
	if err != nil {
		log.Fatalln(err)
		os.Exit(1)
	}
}
