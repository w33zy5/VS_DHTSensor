#include <DHT.h>
#define DHTPIN 4
#define DHTTYPE DHT22
DHT dht(DHTPIN, DHTTYPE);

void setup() {
  // put your setup code here, to run once:
  Serial.begin(9600);
  dht.begin();
}

int flag_begin = 0;
bool change;
void loop() {
  // put your main code here, to run repeatedly:  
  if(Serial.available()>0){
    if(Serial.read() == "S"){
      flag_begin = 1;
    }
    if(Serial.read() == "T"){
      flag_begin = 0;
    }
  }
  send_data();
}

void send_data(){
  if(flag_begin = 1){
    delay(500);
  
    float h = dht.readHumidity();
    float t = dht.readTemperature();
  
    if (isnan(h) || isnan(t)) {
      Serial.println("Failed to read from DHT sensor!");
      return;
    }
  
    if(change){
      Serial.print("A");
      Serial.print(h);
      Serial.print("H");
      change = false;
    }
    else if(!change){
      Serial.print("A");
      Serial.print(t);  
      Serial.print("T");
      change = true;
    }
  }
  
}
