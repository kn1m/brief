package connectivity

import (
	"github.com/gotmc/libusb"
	"log"
)

const (
	manufacturerName = "Amazon"
	productName = "Amazon Kindle"
)

type KindleUsbConnector struct {}

func (c *KindleUsbConnector) GetNotes() string {
	return ""
}

func getNotesDataFromDevice(deviceType string) ([]byte, error) {
	return nil, nil
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