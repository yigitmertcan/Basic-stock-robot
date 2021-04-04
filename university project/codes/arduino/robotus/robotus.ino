int motorustPin1=2;
int motorustPin2=3;

int motor1altPin1=4;
int motoraltPin2=5;

int trigPin = 8; /* Sensorun trig pini Arduinonun 6 numaralı ayağına bağlandı */
int echoPin = 7;

int enablePin2=12,motorustundegeri;
int enablePin=11,a=1;

bool sifirci=true;
long sure;
long uzaklik;

void setup() {
  // put your setup code here, to run once:
pinMode(motor1altPin1,OUTPUT);
pinMode(motoraltPin2,OUTPUT);
pinMode(motorustPin1,OUTPUT);
pinMode(motorustPin2,OUTPUT);
pinMode(enablePin,INPUT);
digitalWrite(enablePin,HIGH);
pinMode(enablePin2,INPUT);
digitalWrite(enablePin2,HIGH);
 pinMode(trigPin, OUTPUT); /* trig pini çıkış olarak ayarlandı */
  pinMode(echoPin,INPUT); /* echo pini giriş olarak ayarlandı */
  Serial.begin(9600);

}

void loop() {
  // put your main code here, to run repeatedly:

  digitalWrite(trigPin, LOW); /* sensör pasif hale getirildi */
  delayMicroseconds(5);
  digitalWrite(trigPin, HIGH); /* Sensore ses dalgasının üretmesi için emir verildi */
  delayMicroseconds(10);
  digitalWrite(trigPin, LOW);  /* Yeni dalgaların üretilmemesi için trig pini LOW konumuna getirildi */ 
  sure = pulseIn(echoPin, HIGH); /* ses dalgasının geri dönmesi için geçen sure ölçülüyor */
  uzaklik= sure /29.1/2; /* ölçülen sure uzaklığa çevriliyor */            
  if(uzaklik > 200)
    uzaklik = 200;
  Serial.println(uzaklik); /* hesaplanan uzaklık bilgisayara aktarılıyor */  
  delay(500); 
}
void serialEvent()
{
 int okuyucu=Serial.parseInt();
    // loop();
  
 
 if(okuyucu<0)
  {
    
    if(sifirci==true)
    {
   
      digitalWrite(motor1altPin1,1);
    digitalWrite(motoraltPin2,0);
    delay(okuyucu*-1);
    digitalWrite(motor1altPin1,0);
    digitalWrite(motoraltPin2,0);
      }
    else
    {
     
      digitalWrite(motor1altPin1,0);
    digitalWrite(motoraltPin2,1);
    delay(okuyucu*-1);
    digitalWrite(motor1altPin1,0);
    digitalWrite(motoraltPin2,0);
      }
    
    
    }
  else if(okuyucu>1)
  {
    if(sifirci==true)
    {
      digitalWrite(motorustPin1,1);
    digitalWrite(motorustPin2,0);
    delay(okuyucu);
    digitalWrite(motorustPin1,0);
    digitalWrite(motorustPin2,0);
      }
    else
    {
      digitalWrite(motorustPin1,0);
    digitalWrite(motorustPin2,1);
    delay(okuyucu);
    digitalWrite(motorustPin1,0);
    digitalWrite(motorustPin2,0);
   
      }
    }
    else if(okuyucu==1)
    {
      if(sifirci==true)
      sifirci=false;
      else
      sifirci=true;
      
     
    
      }
  }
  /*/dev/ttyACM0
  /dev/ttyUSB0*/
/*if(a==1)
{
  //30cm=1100 sn
digitalWrite(motor1Pin1,1);
  digitalWrite(motor1Pin2,0);
  delay(4000);
a=0;
  }
  else
  {
    
digitalWrite(motor1Pin1,LOW);
  digitalWrite(motor1Pin2,0);
    }
*/  

