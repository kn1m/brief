//+build debug

package common

import (
	"bufio"
	"os"
	"fmt"
	"strings"
)

func GetUserCredentials() (string, string) {
	reader := bufio.NewReader(os.Stdin)

	fmt.Print("Enter Username: ")
	username, _ := reader.ReadString('\n')

	fmt.Print("Enter Password: ")
	password, _ := reader.ReadString('\n')

	return strings.TrimSpace(username), strings.TrimSpace(password)
}
