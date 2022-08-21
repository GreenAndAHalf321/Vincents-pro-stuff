# Poker Sim
Jeder kennt doch das Kartenspiel *Poker*. Ein eigentlich sehr simples Spiel welche sehr auf Glück bassiert und darauf wie gut dein Pokerface ist. Es gibt 9 verschiedene Muster die man mit den 2 Karten die man in der Hand hat und den 5 am Tisch bilden kann um zu gewinnen. Doch, wie wahrscheinlich sind eigentlich die einzelnen die einzelnen Kartenreihenfolgen? Wie hoch ist die Wahrscheinlichkeit das du z.B. ein Royal Flush vor dir liegen hast oder eine Straße. 

Um diese Frage zu beantworten gibt es 3 Möglichen:
1. Du googelst aber das ist ja langweilig...
2. Die rechnest es aus. Aber weißt du wie das geht? Nein? Ich schon lol aber viel zu aufwendig und dauert zu lange
3. Du programmierst ein Programm welche 1000de male Poker Spiele Simuliert und immer zählt welche Muster wie oft vor kam und dann alles in Prozent ausgibt

Was für ein Zufall dass das genau deine Aufgabe sein wird

>Bei der Aufgabe übst du *Enums, Nested Classes, IComparable<>, IComparer, Exception handeling, Operator overloading*, try/catch und *Properties*

>Falls du bei etwas nicht weiter kommst schau dir gerne meine Erklärung an. Da ist nicht nur meine Lösung sonder eine genau Beschreibung warum ich was wie gemacht habe damit kann man auch lernen. Und wenn du alles alleine schaffen solltest schau dir danach trotzdem meine Lösugn an schadet ja nicht

## Card Class
Erstelle zuerst eine Klasse für die Karten. Von dieser Klasse wirst du dann später für jede Simulation *56 Instanzen* erstellen für jede einzelne Karte.

Um es für das Beispiel etwas einfacher zu machen legen wir im Programm nicht extra Bube, Dame und König fest sondern jede Karte hat eines von 4 Zeichen *(Herz, Kreuz, Pik, Karo)* und einen Wert von 2-14 *(11 - Bube, 12 - Dame, 13 - König, 14 - As)*

Verwende für die 4 möglichen Zeichen ein Enum **Zeichen** mit den Werten Herz, Kreuz, Pik und Karo.

>Tipp: Ein Enum ist eien besonder Art von Klasse. Du kannst das Enum innerhalb der *Card* Klasse erstellen du kannst es aber auch völlig alleinstehen außerhalb der Klasse erstellen. Erstellst du es in der *Card* Klasse musst du wenn du von außen darauf zugreifen willst immer *Card.Zeichen* schreiben wenn du es außerhalb machst kannst du von überall mit *Zeichen* darauf zugreifen.

### Instanzvariablen
Die Klasse bekommt ein Zeichen *zeichen* und ein Full-Property int *Value* als Instanzvariablen.

* *zeichen* soll readonly sein
* *Value* soll im setter eine **IndexOutOfRangeException()** werfen wenn man versucht eine Wert zuzuweisen der nicht innerhalb von 2-14 ist (Da der Wert einer Karte nur in deisem Bereich sein darf)

### Konstruktor
Der Konstruktor bekommt als Parameter ein Zeichen **zeichen** und ein int **value** und übergibt die Werte an die 2 Instanzvariablen.
Das zuweisen von *Value* soll in ein try/catch verpackt werden. Wenn eine *IndexOutOfRangeException* geworfen wird soll der Wert einfach auf den kleinstmöglichen Kartenwert gesetzt werden.

### Operator overloading
* Überlade die Operatoren < und > die jeweils den *Value* von den Karten auf größe vergleichen sollen.
* Überlade die Operatoren == und != die jeweils schauen soll ob der *Value* von 2 Karten der selbe ist.
* Überlade den Operator | der überprüfen soll ob zwei Karten dieselbe Farbe haben.
> Tipp: Herz/Karo Karten sind rot, Pik/Kreuz Karten sind schwarz

### Interface
Die Klasse soll das IComparable<> interface implementieren und in der *CompareTp()* Methode den Value von von 2 Karten vergleichen. Damit kann man dann später eine Liste von Karten mit List.Sort() ordnen

##CardColorComparer Class
Erstelle eine Klasse die einen IComparer<Card> implementiert. Dieser Comparer Soll zwei Karten auf ihre Farbe vergleichen. Dabei soll Rot kleiner gewertet werden als Schwarz.

Also wenn man eine Liste cards hat und die Farben sind R - S - S - S - R - S - R soll die Liste nach cards.Sort(new CardColorComparer()) so R - R - S - S - S - S - S aussehen

##Simulation Class
Erstelle eine Simulation Klasse. Diese Klasse wird dann bei jeder Simulation ein volles Kartendeck erstellen und mit x Spielern ein Pokerspiel simulierne um am Ende zu zählen welche Muster wie oft vorkammen und die Werte auszugeben.


### Nested Class
Erstelle in der Simulation Klasse eine private *Player* Klasse. In diese kommt eine public LinkedList mit Karten für die 2 Karten die jeder Spieler in der Hand hat und eine *AddCard()* Methode die eine Karte zur LinkedList hinzufügt wenn noch nicht schon 2 Karten drinnen sind.

### Instanzvariablen
Die Klasse bekommt drei Listen von Karten *cardDeck*, *cardsONTheTable* und *players* und einen int *playerCount*

* In der cardDeck Liste werden dann die ganzern 56 Karten drinnen sein die im laufe des Spiels ausgeteilt werden
* In der cardsOnTheTable Liste sind dann die 5 Karten die auf dem Tisch aufgelegt werden wie das bei Poker halt so ist haha
* In der players Liste sind dann alle Spieler mit jeweils 2 zufälligen Karten drinnen
* Der *playerCount* int sagt der Klasse wie viele Spieler sie für die Simulation erstellen soll

### Statische Variablen
Die Klasse bekommt 11 public static Properties um zu zählen welches Muster insgeammt in allen Simulationen wie oft vorkam und noch eine *Random* variable die wir dann brauchen um jedem Spieler eine zufällige Karte zu geben

* Ein public static int Property Tries mit einem private setter damit man es nicht von aussen verändern kann und 0 als start Value
* Ein public static int Property RoyalFlush mit einem private setter damit man es nicht von aussen verändern kann und 0 als start Value
* Ein public static int Property StraightFlush mit einem private setter damit man es nicht von aussen verändern kann und 0 als start Value
* Ein public static int Property Vierling mit einem private setter damit man es nicht von aussen verändern kann und 0 als start Value
* Ein public static int Property FluuHouse mit einem private setter damit man es nicht von aussen verändern kann und 0 als start Value
* Ein public static int Property Flush mit einem private setter damit man es nicht von aussen verändern kann und 0 als start Value
* Ein public static int Property Strasse mit einem private setter damit man es nicht von aussen verändern kann und 0 als start Value
* Ein public static int Property Drilling mit einem private setter damit man es nicht von aussen verändern kann und 0 als start Value
* Ein public static int Property DoppelPaar mit einem private setter damit man es nicht von aussen verändern kann und 0 als start Value
* Ein public static int Property Paar mit einem private setter damit man es nicht von aussen verändern kann und 0 als start Value
* Ein public static int Property Nothing mit einem private setter damit man es nicht von aussen verändern kann und 0 als start Value
* Eine private static Random Variable

### Konstruktor
Der Konstruktor soll in den Parametern einen int *playerCount* bekommen und den Wert der playerCount Instanzvariable übergeben
Danach soll er die *StartSimulation()* Methode aufrufen die du gleich erstellen sollst

### Methoden
> Eine Methode die eine neue Liste zurück gibt mit den 5 Karten die am Tisch liegen und den 2 Karten die ein Spieler in der Hand hat (also mit 7 Karten) kann sehr hilfreich sein

#### CheckFlush(Player player)
  Eine Methode die einen Spieler bekommt und einen boolean zurückliefert der angibt ob der übergebene Spieler zusammen mit den 5 Karten am Tisch ein Deck mit 5 gleichfarbigen Karten hat
> Der | Operator den wir in der Card Klasse überladen haben kann hier verwendet werden

#### GetFlush(Player player)
  Eine Methode die einen Spieler bekommt und falls der Spieler ein Flush bilden kann eine Liste zurückliefert die was die 5 Karten die das Flush ergeben beinhaltet
> Wird später noch nützlich

#### CheckStreet(List<Card> cardDeck)
  Eine Methode die eine Liste an Karten bekommt (7 Karten - die 5 am Tisch + die 2 in der Hand vom Spieler) und einen boolean zurückgibt der angibt ob irgendwo in dieser Liste eine Reihe von 5 aufeinanderfolgenden Karten ist

#### CheckStreet(Player player)
  Eine Methode die einen Spieler bekommt und einen boolean zurückgibt der angibt ob der Spieler mit den 5 Karten am Tisch eine Reihe von 5 aufeinanderfolgenden Karten bilden kann
> Du kannst dafür die *GetFlush(List<Card> cardDeck)* in der *CheckStreet(Player player)* aufrufen da brauchst du nicht 2 mal denselben Code schreiben
  
#### CheckVierling(Player player)
  Eine Methode die einen Spieler bekommt und einen boolean zurückgibt der angibt ob der Spieler mit den 5 Karten am Tisch ein Deck mti 4 Karten bilden kann die alle denselben Wert haben
  
#### CheckDrilling(Player player)
   Eine Methode die einen Spieler bekommt und einen boolean zurückgibt der angibt ob der Spieler mit den 5 Karten am Tisch ein Deck mti 3 Karten bilden kann die alle denselben Wert haben
  
#### GetDrilling(Player player)
   Eine Methode die einen Spieler bekommt und, falls der Spieler mit den 5 Karten am Tisch ein Deck mit 3 Karten bilden kann die alle denselben Wert haben, eine Liste zurückgibt die diese 3 Karten enthält
> Wird später noch nützlich
  
#### CheckPaar(Player player)
  Eine Methode die einen Spieler bekommt und einen boolean zurückgibt der angibt ob der Spieler mit den 5 Karten am Tisch ein Paar bilden kann wo beide Karten denselben Wert haben
  
#### GetPaar(Player player)
  Eine Methode die einen Spieler bekommt und, falls der Spieler mit den 5 Karten am Tisch ein Paar bilden kann wo beide Karten denselben Wert haben, eine Liste zurückgibt die diese 2 Karten enthält
  
#### CheckPaarWithout(Player player, int forbittenValue)
  Eine Methode die einen Spieler bekommt und einen int. Sie soll wie *CheckPaar(Player player)* einen boolean zurückgeben der angibt ob der Spieler ein Paar bilden kann. Sollte er aber ein Paar bilden wo der Wert der *forbittenValue* ist soll trotzdem false zurückgeliefert werden. Also wenn die 5 Karten + die 2 Spieler Karten z.B. die Werte 3 - 5 - 7 - 3 - 4 - 9 - 11 haben und der *forbittenValue* ist 3 dann gibt es zwar eigentlich ein Paar aber es wird trotzdem false zurückgegeben
  
#### CheckDoublePaar(Player player)
  Eine Methode die einen Spieler bekommt und einen boolean zurückgibt der angibt ob der Spieler mit den 5 Karten die am Tisch liegen 2 verschieden Paare bilden kann wo jeweils beide Karten denselben Wert haben
> Hier kannst du die *CheckPaarWithout()* Methode und die *GetPaar()* Methode verwenden um sicherzustellen das beim überprüfen nicht 2x dasselbe Paar genommen wird
  
#### StartSimulation()
  Das ist die Methode die gleich im Konstruktor der Simulation Klasse aufgerufen wird.
  
  1. Zuerst soll sie die *cardDeck* Liste mit 52 Instanzen von Karten auffüllen. Dafür erstellt sie für jedes Zeichen jeweils 13 Karten mit den Werten von 2-14
  > Tipp: Dafür würden sich 2 inneinander verschachtelte for-Schleifen super eignen eine für die 4 Zeichen die andere für die 13 Werte pro Zeichen
  2. Danach sollen die Spieler erstellt werden. Dafür wird die *players* Liste mit *playerCount* Spielern gefüllt werden
  3. Jetzt sollen die Karten ausgeteilt werden. Dabei bekommt jeder Spieler erstmal eine zufällig Karte aus dem Deck. Danach werden drei zufällige Karten auf den Tisch gelegt, dann bekommt jeder Spieler erst seine zweite Karte und danach werden noch 2 Karten auf den Tisch gelegt. In der Reihenfolge sollen die Karten aus der *cardDeck* Liste zur *cardsOnTheTable* Liste und zur *cards* Liste in der Player Klasse hinzugefügt werden
  > Vergiss nicht die Karten die du austeilst aus der *cardDeck* Liste zu entfernen damit du nicht 2x dieselbe Karte austeilst
  4. Jetzt wird *Tries* um 1 erhöt da wir nach jeder Simulation einen Try mehr haben
  5. Nach dem soll das Program mit einer foreach Schleife durch alle Spieler durchgehen und bei jedem Spieler überprüfen ob er ein Muster hat und wenn ja welches. Wenn er eines hat soll das jewailig Property (z.B. Royal Flush) um 1 erhöt werden. Achte dabei auf die Reihenfolge in der du alles überprüfst. Wenn der Spieler ein Drilling hat darfst du nicht merh überprüfen ob er ein Paar hat weil wenn er 3 Karten mit demselben Wert hat hat er natürlich auch 2 Karten mit demselben Wert das heißt CheckPaar() würde auch true zurückliefern. Also wenn CheckDrilling true zurückliefert darfst du nicht mehr CheckPaar() überprüfen. Das gilt aber auch für eine andere Fälle du solltest du selsbt draufkommen ;)
  > Tipp: Du brauchst einige Methoden wie z.B. *CheckStraightFlush()* nicht weil ein Straight Flush ist ein Flush was gleichzeitig auch eine Straße ist da kannst du einfach *CheckFlush()* und *CheckStreet()* verwenden. Dasselbe gilt für RoyalFlush und FullHouse
  Wenn nichts der Muster zutrifft soll *Nothing* um 1 erhöt werden
  > Falls du Probleme damit hast die richtige Reihenfolge zu finden in der du alles überprüfen musst schau einfach in meinem Beispiel an das hat auch nicht wirklich was mit Programmieren zutun deswegen ist es nd so schlimm
  6. Als letztes soll die Methode die berechneten Werte ausgeben. Das sollte dann so aussehen:
  
  ![image](https://user-images.githubusercontent.com/80975675/185790243-e0708669-a24c-4e83-8346-c1c29555ee5d.png)
  
Dafür musst du dir die Prozente ausrechnen und damit es bisi schöner aussieht hab ich noch gemacht das die Zahl bei Simulation #-------- immer 8 Zeichen lang ist und die Prozente Maximal 2 nachkommerstellen haben.
Das könntest du auch machen aber das ist echt mühsam und dauert nur lange und man lernt nicht wirklich was deswegen kannst dus einfach hier kopieren wie ichs gemacht hab
  
```C#
  double royalFlushProzentage = RoyalFlush / (double)Tries * 100;
  double straightFlushProzentage = StraightFlush / (double)Tries * 100;
  double VierlingeProzentage = Vierling / (double)Tries * 100;
  double fullHouseProzentage = FullHouse / (double)Tries * 100;
  double flushProzentage = Flush / (double)Tries * 100;
  double StreetProzentage = Strasse / (double)Tries * 100;
  double drillingProzentage = Drilling / (double)Tries * 100;
  double doppelPaarProzentage = DoppelPaar / (double)Tries * 100;
  double paarProzentage = Paar / (double)Tries * 100;
  double nothingProzentage = Nothing / (double)Tries * 100;
  
  Console.WriteLine($"Simulation #{String.Format("{0:00000000}", Tries)} beendet: \n" +
                $"Royal Flush: {String.Format("{0:0.00}", royalFlushProzentage)}% - {RoyalFlush}\n" +
                $"Straight Flush: {String.Format("{0:0.00}", straightFlushProzentage)}% - {StraightFlush}\n" +
                $"Vierling: {String.Format("{0:0.00}", VierlingeProzentage)}% - {Vierling}\n" +
                $"Full House: {String.Format("{0:0.00}", fullHouseProzentage)}% - {FullHouse}\n" +
                $"Flush: {String.Format("{0:0.00}", flushProzentage)}% - {Flush}\n" +
                $"Straße: {String.Format("{0:0.00}", StreetProzentage)}% - {Strasse}\n" +
                $"Drilling: {String.Format("{0:0.00}", drillingProzentage)}% - {Drilling}\n" +
                $"Doppeltes Paar: {String.Format("{0:0.00}", doppelPaarProzentage)}% - {DoppelPaar}\n" +
                $"Paar: {String.Format("{0:0.00}", paarProzentage)}% - {Paar}\n" +
                $"Nichts: {String.Format("{0:0.00}", nothingProzentage)}% - {Nothing}\n" );
```
  

##Main Klasse
  Als letzes musst du nur noch in Program.cs die Simulation starten. Mach dafür eine Schleife die 1.000.000 neue Simulationen erstellt mit jeweils 4 Spielern. Als letzte Aufgabe soll nur noch jede Simulation asynchron gestartet werden. 
  
  Wenn du das alles hast bist du fertigggg. Mit 1.000.000 Simulation dauert es bisi bis alle fertig ist du kannst auch gerne weniger machen aber mach nicht weniger als 100.000 weil sonst sind die Werte die am Ende rauskommen zu ungenau.
  
  Zum Vergleich, die Werte die du am Ende haben solltest sind:
  
  Royal Flush: 0,01%
  Straight Flush: 0,12%
  Vierling: 0,16%
  Full House: 2,51%
  Flush: 11,33%
  Straße: 3,85%
  Drilling: 4,94%
  Doppeltes Paar: 24,11%
  Paar: 52,41%
  Nichts: 24,02%
  
  > Die Werte können um ein Paar nachkommerstellen abweichen aber es sollte keine großen Unterschiede geben
