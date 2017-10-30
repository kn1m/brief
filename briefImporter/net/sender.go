package net

import (
	"fmt"
	"net/http"
	"bytes"
	"io/ioutil"
	"brief/briefImporter/common"
)

func SendNotesToServer(notes *[]byte) {
	url := "http://localhost/notes"
	fmt.Println("sending to: ", url)

	req, err := http.NewRequest("POST", url, bytes.NewBuffer(*notes))
	req.Header.Set("Set-Type", "All")
	req.Header.Set("Content-Type", "application/json")

	client := &http.Client{}
	resp, err := client.Do(req)
	common.Check(err)
	defer resp.Body.Close()

	fmt.Println("response status:", resp.Status)
	fmt.Println("response headers:", resp.Header)
	body, _ := ioutil.ReadAll(resp.Body)
	fmt.Println("response body:", string(body))
}