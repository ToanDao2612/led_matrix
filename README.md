# Led matrix (MAX7219) and Arduino UNO
Generate led matrix model and send it to the led matrix module through the serial port.

<p align="middle" ><img src="/images/base_connection.png" alt="Led matrix app" width="600"></p>
	

## Connection schematics
The connection between the led matrix [module](/schema/lib/led_matrix.fzpz) and the Arduino UNO:

<p align="middle" ><img src="/images/schema_connection.png" alt="Led matrix module" width="600"></p>

	Led matrix ▶ Arduino UNO

	CLK (green)  ▶ D10
	CS (yellow)  ▶ D11
	DIN (orange) ▶ D12
	GND (black)  ▶ GND
	VCC (red)    ▶ 5V

## Install

To send the led matrix model to the led matrix module, we must have 3 things:
- Arduino sketch file
- Arduino led matrix library
- Led matrix generator

### Arduino sketch serial

First, we must import the led matrix [library](/src/arduino/lib/LedControl.zip) to the Arduino IDE: `Sketch->Import Library->Add Library`. Then open and upload the Arduino sketch [file (led_matrix_serial.ino)](/src/arduino/led_matrix/led_matrix_serial.ino) to your Arduino UNO.

### Led matrix generator

With the led matrix MAX2719 [program (Led_matrix.exe)](/src/Led_matrix/Led_matrix/bin/Debug/Led_matrix.exe), you can do 2 things:
- Send through the serial port the current led model
- Generate raw byte array from led model and paste it into Arduino sketch file

<p align="middle" ><img src="/images/led_matrix_app.png" alt="Led matrix app" width="400"></p>

### Send through serial

If the Arduino sketch [file (led_matrix.ino)](/src/arduino/led_matrix_serial/led_matrix_serial.ino) is open and uploaded to the Arduino UNO, select the serial port attached to your Arduino and click `Send`. You will see the current led matrix model applied to the led matrix module.

### Generate raw byte array (string)
If you don't want to set the led matrix module through the serial port, open the Arduino sketch [file (led_matrix.ino)](/src/arduino/led_matrix/led_matrix.ino) to your Arduino UNO. Then, click on `Copy to clipboard` and paste the led matrix model "byte array string" to your Arduino sketch like the following: 

	byte leds[8] = {<copied byte array string>};  
	byte leds[8] = {0x00,0x18,0x24,0x42,0x42,0x24,0x18,0x00}; 

## Test

We can see that that we can simply change the led state

<p align="middle" ><img src="/images/test_serial.gif" alt="Led matrix app" width="800"></p>

## Library and composant ref
[led matrix library](https://github.com/wayoda/LedControl/tree/master/src)

[led matrix module](http://forum.fritzing.org/t/max7219-dot-matrix-led-modul/1914)

## Ref
[An Arduino library for MAX7219 and MAX7221 Led display drivers](https://github.com/wayoda/LedControl)

[Arduino 8×8 LED Matrix Tutorial](http://educ8s.tv/arduino-8x8-led-matrix-tutorial/)

[Arduino tutorial: LED Matrix red 8x8 64 Led driven by MAX7219 (or MAX7221) and Arduino Uno](https://www.youtube.com/watch?v=TOuKnOG8atk)

[Android Things Port of the Arduino LedControl library for the MAX7219 LED matrix module](https://github.com/Nilhcem/ledcontrol-androidthings)
