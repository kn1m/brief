package connectivity

import (
	"log"
	"os/exec"
	"github.com/gotmc/libusb"
	"brief/briefExporter/common"
)

const (
	manufacturerName = "Amazon"
	productName = "Amazon Kindle"
)

type KindleUsbConnector struct {}

func (c *KindleUsbConnector) GetNotesFromDevice(serialNumber string, config *common.Config) (string, error) {
	deviceVerified := verifyDevice(serialNumber)
	if deviceVerified {
		mountPath, err := getDeviceMountPath(serialNumber, config)
		if err == nil {
			log.Printf("Mount path of device %s with serial number %s : %s", productName, serialNumber, mountPath)
			return mountPath, err
		}
	}

	return "", nil
}

func getDeviceMountPath(serialNumber string, config *common.Config) (string, error) {
	mountPath, err := exec.Command("sh", config.ScanMountPathScript).Output()
	return string(mountPath), err
}

func verifyDevice(serialNumberToCheck string) bool {
	manufacturerConfirmed := false
	productConfirmed := false

	ctx, err := libusb.NewContext()
	if err != nil {
		log.Fatal("Couldn't create USB context. Ending now.")
	}

	defer ctx.Close()
	devices, err := ctx.GetDeviceList()
	if err != nil {
		log.Fatalf("Couldn't get devices")
	}

	log.Printf("Found %v USB devices.\n", len(devices))
	for _, device := range devices {
		usbDeviceDescriptor, err := device.GetDeviceDescriptor()
		if err != nil {
			log.Printf("Error getting device descriptor: %s", err)
			continue
		}
		handle, err := device.Open()
		if err != nil {
			log.Printf("Error opening device: %s", err)
			continue
		}
		defer handle.Close()
		manufacturer, err := handle.GetStringDescriptorASCII(usbDeviceDescriptor.ManufacturerIndex)
		if err == nil && manufacturer == manufacturerName {
			manufacturerConfirmed = true
		}
		product, err := handle.GetStringDescriptorASCII(usbDeviceDescriptor.ProductIndex)
		if err == nil && product == productName {
			productConfirmed = true
		}
		serialNumber, err := handle.GetStringDescriptorASCII(usbDeviceDescriptor.SerialNumberIndex)
		if err == nil && manufacturerConfirmed && productConfirmed && serialNumber == serialNumberToCheck {
			return true
		}
	}
	return false
}