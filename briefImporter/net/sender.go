package net

import (
	"net/http"
	"bytes"
	"io/ioutil"
	"brief/briefImporter/common"
	"time"
	"log"
	"encoding/json"
	"io"
)

type HistoryRecord struct {
	SerialNumber string `json:"serial_number"`
	Checksum [16]byte   `json:"checksum"`
	CreatedOn time.Time `json:"created_on"`
}

func GetPreviousHistoryRecord(deviceSerialNumber string) (HistoryRecord, error)  {
	resp, err := executeRequest("http://localhost/notes", "GET", nil, nil)

	var historyRecord HistoryRecord

	body, _ := ioutil.ReadAll(resp.Body)
	logResponse(resp, body)

	err = json.Unmarshal(body, &historyRecord)

	return historyRecord, err
}

func SendNotesToServer(notes *[]byte) {
	headers := make(map[string]string)
	headers["Set-Type"] = "All"
	headers["Content-Type"] = "application/json"

	resp, err := executeRequest("http://localhost/notes", "POST", bytes.NewBuffer(*notes), headers)
	common.Check(err)

	body, _ := ioutil.ReadAll(resp.Body)

	logResponse(resp, body)
}

func logResponse(response *http.Response, body []byte) {
	log.Println("response status:", response.Status)
	log.Println("response headers:", response.Header)
	log.Println("response body:", string(body))
}

func executeRequest(url string, method string, bodyReader io.Reader, headers map[string]string) (*http.Response, error) {
	log.Println("sending to: ", url)

	req, err := http.NewRequest(method, url, bodyReader)
	common.Check(err)
	for k, v := range headers {
		req.Header.Set(k, v)
	}

	client := &http.Client{}
	resp, err := client.Do(req)
	common.Check(err)
	defer resp.Body.Close()

	return resp, err
}

