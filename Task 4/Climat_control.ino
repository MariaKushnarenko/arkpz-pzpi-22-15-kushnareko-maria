#include "DHT.h"
#include <WiFi.h>
#include <HTTPClient.h>
#include <WiFiUdp.h>
#include <NTPClient.h>
#include <ArduinoJson.h>
#include <time.h>

#define DHTPIN 4       // Digital pin connected to the DHT sensor
#define DHTTYPE DHT22  // DHT 22 (AM2302), AM2321

DHT dht(DHTPIN, DHTTYPE);

WiFiUDP udp;
NTPClient timeClient(udp, "pool.ntp.org", 3600, 60000);  

unsigned long lastServerUpdateTime = 0;  // Tracks the last time data was sent
const unsigned long updateInterval = 60000;  // Interval between updates (1 minute)

void setup() {
  Serial.begin(9600);
  dht.begin();

  // Connect to Wi-Fi
  Serial.println("Connecting to WiFi...");
  WiFi.begin("Wokwi-GUEST", "");
  while (WiFi.status() != WL_CONNECTED) {
    delay(1000);
    Serial.print(".");
  }
  Serial.println("Connected to WiFi");

  timeClient.begin();
}

void loop() {
  float humidity = dht.readHumidity();
  float temperature = dht.readTemperature();

  if (isnan(humidity) || isnan(temperature)) {
    Serial.println("Failed to read from DHT sensor!");
    delay(2000);
    return;
  }

  // Print sensor data
  Serial.print("Humidity: ");
  Serial.print(humidity);
  Serial.print(" %\t Temperature: ");
  Serial.print(temperature);
  Serial.println(" °C");


  unsigned long currentTime = millis();
  if (currentTime - lastServerUpdateTime >= updateInterval) {
    lastServerUpdateTime = currentTime;  

    if (uploadToServer(temperature, humidity)) {
      Serial.println("Data sent successfully.");
    } else {
      Serial.println("Data upload failed.");
    }

    Serial.println("Restarting measurement cycle...");
    resetSensorCycle();
  }

  delay(5000);  
}

bool uploadToServer(float temperature, float humidity) {
  WiFiClient client;
  HTTPClient http;

  String url = "http://ce6a-178-150-95-180.ngrok-free.app/api/Sensors/1";  
  // Create JSON payload
  String payload = "{\"id\": 1, ";
  payload += "\"temperature\": " + String(temperature) + ", ";
  payload += "\"humidity\": " + String(humidity) + ", ";
  payload += "\"timestamp\": \"" + getTimestamp() + "\", ";
  payload += "\"installationDate\": \"" + getTimestamp() + "\", ";
  payload += "\"animalId\": 1}";

  Serial.print("Sending data to server: ");
  Serial.println(url);


  http.begin(client, url);
  http.addHeader("Content-Type", "application/json");
  int httpCode = http.PUT(payload);

  if (httpCode > 0) {
    Serial.printf("HTTP code: %d\n", httpCode);
    http.end();
    return (httpCode == 200 || httpCode == 204);  // Success codes
  } else {
    Serial.printf("HTTP request failed: %s\n", http.errorToString(httpCode).c_str());
    http.end();
    return false;
  }
}

String getTimestamp() {
  timeClient.update();
  unsigned long epochTime = timeClient.getEpochTime();
  struct tm *ptm = gmtime((time_t *)&epochTime);

  // Format the timestamp
  char buffer[25];
  snprintf(buffer, sizeof(buffer), "%04d-%02d-%02dT%02d:%02d:%02d", 
           ptm->tm_year + 1900, ptm->tm_mon + 1, ptm->tm_mday,
           ptm->tm_hour, ptm->tm_min, ptm->tm_sec);

  return String(buffer);
}

// Reset the sensor cycle
void resetSensorCycle() {
  dht.begin();
  delay(2000);  // Allow the sensor to stabilize
  Serial.println("Sensor cycle reset complete.");
}
