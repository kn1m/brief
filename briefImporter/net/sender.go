package net

import (
	"net/http"
	"bytes"
	"io/ioutil"
	"brief/briefImporter/common"
	"time"
	"log"
	"encoding/json"
)

type HistoryRecord struct {
	SerialNumber string `json:"serial_number"`
	Checksum []byte     `json:"checksum"`
	CreatedOn time.Time `json:"created_on"`
}

func GetPreviousHistoryRecord(deviceSerialNumber string) (HistoryRecord, error)  {
	url := "http://localhost/notes"
	log.Println("sending to: ", url)

	req, err := http.NewRequest("GET", url, nil)

	client := &http.Client{}
	resp, err := client.Do(req)
	common.Check(err)
	defer resp.Body.Close()

	var historyRecord HistoryRecord

	log.Println("response status:", resp.Status)
	log.Println("response headers:", resp.Header)
	body, _ := ioutil.ReadAll(resp.Body)
	log.Println("response body:", string(body))

	err = json.Unmarshal(body, &historyRecord)

	return historyRecord, err
}

func SendNotesToServer(notes *[]byte) {
	url := "http://localhost/notes"
	log.Println("sending to: ", url)

	req, err := http.NewRequest("POST", url, bytes.NewBuffer(*notes))
	req.Header.Set("Set-Type", "All")
	req.Header.Set("Content-Type", "application/json")

	client := &http.Client{}
	resp, err := client.Do(req)
	common.Check(err)
	defer resp.Body.Close()

	log.Println("response status:", resp.Status)
	log.Println("response headers:", resp.Header)
	body, _ := ioutil.ReadAll(resp.Body)
	log.Println("response body:", string(body))
}