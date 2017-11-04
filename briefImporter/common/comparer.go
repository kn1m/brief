package common

import (
	"os"
	"crypto/md5"
	"io"
	"log"
)

func GetFileChecksum(file *os.File) []byte {
	h := md5.New()
	if _, err := io.Copy(h, file); err != nil {
		log.Fatal(err)
	}

	hash := h.Sum(nil)

	log.Printf("Current file hash: %x\n", hash)

	return hash
}