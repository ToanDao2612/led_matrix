#include <LedControl.h>

#define MAX_WAIT 1000

int DIN = 12;
int CS =  11;
int CLK = 10;
LedControl lc = LedControl(DIN,CLK,CS,0);

void setup(){
  
  // The MAX7219 is in power-saving mode on startup
  lc.shutdown(0,false);       
  
  // Set the brightness to maximum value
  lc.setIntensity(0,15);      
  
  // Clear the display
  lc.clearDisplay(0);         
}

void loop(){
  byte leds[8] = {0x00,0x18,0x24,0x42,0x42,0x24,0x18,0x00};
  print_byte(leds);
}

void print_byte(byte character []){
  int i = 0;
  for(i=0;i<num_bytes;i++){
    lc.setRow(0,i,character[i]);
  }
}