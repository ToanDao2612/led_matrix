#include <LedControl.h>

#define MAX_WAIT 1000

int DIN = 12;
int CS =  11;
int CLK = 10;
LedControl lc = LedControl(DIN,CLK,CS,0);
unsigned long start_time;
const byte num_bytes = 8;
byte received_bytes[num_bytes];

void setup(){
  Serial.begin(9600);
  
  // The MAX7219 is in power-saving mode on startup
  lc.shutdown(0,false);       
  
  // Set the brightness to maximum value
  lc.setIntensity(0,15);      
  
  // Clear the display
  lc.clearDisplay(0);         
}

void loop(){ 
  
  // Hang in this loop until we either get 8 bytes of data or 1 second has gone by
  start_time = millis();
  while ((Serial.available()<num_bytes) && ((millis() - start_time) < MAX_WAIT)){}
  
  // Check if get all bytes
  if(Serial.available() >= num_bytes){
    
    // Get serial data and print it on the matrix
    for(int n=0; n<num_bytes; n++){
      received_bytes[n] = Serial.read(); // Then: Get them. 
    }
    print_byte(received_bytes);
  }
}

void print_byte(byte character []){
  int i = 0;
  for(i=0;i<num_bytes;i++){
    lc.setRow(0,i,character[i]);
  }
}