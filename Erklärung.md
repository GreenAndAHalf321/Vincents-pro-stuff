# Erklärung
Das hier ist meine Lösung wie ich das Programm programmiert hab + eine Erklärung für alles warum ich das so gemacht habe. Es lohnt sich glaube ich wirklich das durchzulesen weil dann siehst du vill ein paar Sachen die praktisch sind sich zu merken

Was noch wichtig ist zu wissen. Es gibt viele Möglichkeiten die Aufgabe zu Lösen. Meine ist nicht "die Richtige" wenn deine funktioniert ist sie genauso richtig. Ich habe bei meiner Lösung immer versucht so wenig Code wie möglich zu schreiben und deswegen manchmal Sachen versucht abzukürzen das muss man aber nicht machen.

## Cards Klasse
Hier eine ganze Erklärung wie ich die Cards Klasse gemacht habe

### Das Enum

![image](https://user-images.githubusercontent.com/80975675/185791072-adf2d6dc-89bc-45e5-a2ff-13d1c2462e2c.png)

Das Enum habe ich außerhalb der Klasse einfach darüber erstellt. Das kannst du machen wie du willst aber ich hab es so gemacht weil ich nicht jedes mal wenn ich auf das Enum zugreife *Card.Zeichen* schreiben wollte. Du kannst aber auch argumentieren das es mehr Sinn macht es innerhalb der Card Klasse zu haben weil nur eine Karte kann ein Zeichen haben deswegen macht es ja Sinn das man *Card.Zeichen* schreiben musst.

In dem Enum habe ich einfach die 4 Zeichen Herz, Kreuz, Pik, Karo reingeschrieben.

### IComparable<Card>

![image](https://user-images.githubusercontent.com/80975675/185791334-7d795480-2a86-4d87-a6f3-4a1afb8cc3a1.png)

![image](https://user-images.githubusercontent.com/80975675/185791349-ba860fa2-a3fb-43b3-bd68-7dcdc356fab1.png)

Die Klasse soll ja in einer Liste nach den Werten sortierbar sein. Dafür habe ich das *IComparable<Card>* interface implementiert
> Das *Card* in den spitzen Klammern ist weil ich die Karte ja mit anderen Karten vergleichen will. Ich kann mit dem Interface meine Karte auch mit anderen Klassen vergleichen wenn ich z.B. ICompareable<String> schreiben würde vergleiche ich eine Karte mit einem String. Da das kein Sinn macht und wir das garnicht wollen mach ich das natürlich nicht.

Wenn man das *ICompareable<Card>* Interface implementiert muss man die CompareTo(Card other) Methode hinzufügen. Wir wollen die Karten ja nach dem Wert der Karte sortieren deswegen vergleichen wir eben den Wert (Value). 
Das Gute ist:
  Der Value ist ein int. Und die Klasse int hat selber schon eine CompareTo() Methode. Das bedeutet wir müssen nicht mal ein Code zum Vergleichen selber schreiben sondern können die 2 ints einfach mit der schon verhandenen CompareTo() Methode von int vergleichen
  
### Die Instanzvariablen

![image](https://user-images.githubusercontent.com/80975675/185791725-f94a284e-40a3-4e27-b53a-4d99bafe5cc7.png)

Die Klasse sollte ja ein Zeichen haben. Das hab ich readonly gemacht weil man soll das Zeichen im nachhinein ja nicht mehr ändern können. Wenn ich eine Herz 4 erstelle soll das eine Herz 4 bleiben und nicht plötzlich zur Pik 4 werden

Bei dem Value sollte ja eine Exception geworfen werden wenn man einen Falschen Wert setzt. Da die Exception beim setzten geworfen werden soll müssen wir einen Setter selber programmieren. Also brauchen wir ein Full-Property

> Ein Full-Property ist ein Property wo man Getter und Setter komplett selber programmiert. Das Auto-Implemented-Property hingegen ist die kurze vorm und sieht so aus:
``` public int Value{ get; private set; } ```

Beim Getter müssen wir nichts besonderes machen deswegen können da einfach normal den value returnen

Den Setter machen wir erst mal private es dem selbem Grund warum *zeichen* readonly ist. Niemand soll von ausen einfach den Wert einer Karte ändern können nachdem sie erstellt wurde.  Dann kommt eine if-Abfrage rein die schaut ob der Wert der versucht wird zu setzten auch wirklich im richtigem Bereich ist (2-14) und falls das nicht der Fall ist wird die Exception geworfen. Wenn es aber passt wird der Wert einfach problemlos gesetzt.


Falls du dich fragst warum über dem *public int Value* nochmal ein *private int value* ist hier nochmal eine Erklärung wie sie Properties funktionierten (falls du das eh weißt brauchst du das nicht lesen):

Ein Property ist eigentlich dasselbe wie eine Variable nur das der Programmiere die Möglichkeit hat den Getter und Setter individuell zu bearbeiten. Eine Variable hat genauso einen Getter und Setter wie ein Property nur das wir die nicht bearbeiten können

```C# 
object name2 = name; 
```
Hier wurde jetzt der Setter von *name2* Aufgerufen weil *name2* einen Wert geSETZT bekommt und es wurde der Getter von *name* aufgerufen weil wir GETen uns den Wert von *name*

Bei einem Full-Property können wir jetzt Getter und Setter selber schreiben. 
```C# 
private String nameAlsProperty;
public String NameAlsProperty {
  get {
    return nameAlsProperty;
  }
  
  set {
    nameAlsProperty = vale;
  }
}

public String nameAlsVariable;
```

In dem Beispiel sind *NameAlsProperty* und *nameAlsVariable* genau dasselbe. Wenn ich irgendwo ``` nameAlsVariable = ... ``` schreibe passiert intern genau dasselbe wie wenn ich ``` NameAlsProperty = ... ``` schreibe weil so wie ich Getter und Setter im Property programmiert hab sind das die Standart Getter und Setter und nichts anderes tun als Werte setzen und nehmen. So sieht das intern in der Variable auch aus nur sehen wir das eben nicht.

Jetzt ist aber die Frage warum ist über dem Property nochmal eine Variable? Warum kann ich nicht einfach nur das Property verwenden?

Schauen wir uns einfach mal den Setter vom Property an wenn wir die Variable nicht hätten.

```C#
NameAlsProperty = "Vincent";
```

Wenn ich versuchen würde diesen Wert beim Property zu setzten ohne das es die private Variable gibt würde im Setter jetzt das passierten:

```C#
set{
  NameAlsProperty = value //value ist in dem Beispiel jetzt "Vincent" weil wir ja versuchen diesen Wert zu setzen
}
```
Dadurch das wir jetzt im Setter *NameAlsProperty = ...* schreiben rufen wir ja im Setter den Setter auf. Weil wir versuchen im Setter einen Wert zu setzten. Jetzt wird unendlich oft der Setter aufgerufen bis eine Overflow Exception geworfen wird. Deswegen verwenden wir eine zusätzliche private Variable und anstatt den Wert dem Property zu geben geben wir ihn einfach der Variable und weil der Wert jetzt in der Variable ist returnen wir im Getter auch die Variable und nicht das Property

### Der KOnstruktor

![image](https://user-images.githubusercontent.com/80975675/185793321-5b0dab3d-e285-4a46-b7a0-d3d24d67f6c7.png)

Im Konstruktor war ja die Aufgabe die Werte zu setzten und falls eine IndexOutOfRangeException kommt den Wert einfach auf 2 setzen. Muss ich da was erklären? Wenn ja frag mich einfach haha

### Die Überladenen Operatoren

![image](https://user-images.githubusercontent.com/80975675/185793998-3fc4bb63-03e1-4497-9fd1-1ba568a6662b.png)

Kommen wir zum Operator overloading. Hier wollen wir machen das die Werte der Karten einfach durch <, >, ==, != und | vergleichbar sind. 

Ein paar Sachen generell zum operator overloading:
1. Wenn du einen Operator überladest muss das IMMER static sein weil es soll ja nicht jede Instanz seine eigenen Vergleichoperatoren haben sonder die ganze Klasse.
2. Nach dem static kommt der Rückgabewert. In unserem Fall ist es immer boolean aber wenn man z.B. den + Operator überladen würde könnte der Rückgabewert z.B. ein int sein oder so
3. Nach dem Rückgabewert kommt das Wort *operator* und danach kommt das Zeichen das du gerne überladen wollen würdest
4. Danach kommen die Parameter als was du mit was Vergleichen willst. Wenn du z.b. willst das du ``` if(card1 < card2) ``` schreiben kannst also eine Karte mit einer Karte vergleichen willst dann schreibst du in die Parameter 2 Karten.
5. Danach kommt der Code der am Ende eben das ergebnis liefern soll. Wenn es nur eine Zeile ist kannst du auch ein => verwenden, bei mehr Zeilen eben { }

Bei <, >, == und != hab ich einfach die Values miteinander verglichen.
Bei | soll ja geschaut werden ob 2 Karten dieselbe Farbe haben. Wir haben aber keine Variable die die Farbe einer Karte angibt. Aber man weiß ja welche Zeichen welche Farben haben. Deswegen wusste ich wenn eine Karte entweder Herz oder Karo als Zeichen hat muss sie Rot sein und bei Kreuz oder Pik muss sie Schwarz sein. 

## Der CardColorComparer

![image](https://user-images.githubusercontent.com/80975675/185794137-2091116c-f659-4ad5-aa72-287d3fbc5b54.png)

Der *CardComparer* macht im prinzp genau dasselbe wir der | Operator. also es ist legit der selbe code nur statt true wird 0 returnt und statt false -1 oder 1. Da Rot kleiner gewertet werden soll als Schwarz wird -1 returnt wenn die Karte Rot ist und 1 wenn sie Schwarz ist

##Simulation Klasse

### Die statischen Variablen/Properties

![image](https://user-images.githubusercontent.com/80975675/185796733-6da8854c-66ab-4e92-a666-62172962a629.png)

Die ganzen Properties sind dazu da um zu zählen wie Oft welches Muster vorkommt. Am anfang müssen sie alle auf 0 stehen weil am Anfang gibt es ja noch keine Muster. Der Setter ist immer auf private weil wir ja nicht wollen das die Werte von außen einfach verändert werden können.

Die *rnd* Variable ist eben dazu da um jedem Spieler immer zufällig Karten zu geben.

### Die Player nested-class

![image](https://user-images.githubusercontent.com/80975675/185796895-f00ff25d-e467-47c1-8e94-4ac53694f26a.png)

Hier hab ich einfach eine simple Klasse mit einer LinkedList für die 2 Karten die ja jeder Spieler in der Hand haben soll und eine AddCard(Card card) Methode. Die Methode hab ich gemacht weil so kann ich überprüfen das man niemals mehr als 2 Karten einem Spieler gibt. Falls eine Karte hinzugefügt werden soll aber der Spieler schon 2 hat wird einfach nichts gemacht.

### Die Instanzvariablen

![image](https://user-images.githubusercontent.com/80975675/185797011-0051e0f0-4223-4a93-8192-7637deded961.png)

Bei den Instanzvariablen hab ich einmal eine Liste wo die gesamten 52 Karten reinkommen sollen die dann ausgeteilt werden, eine Liste für die 5 Karten die dann am Tisch liegen sollen und eine Liste für alle Spieler die in einer Simulation mitspielen. Außerdem gibt es noch einen int der angibt wie viele Spieler bei Start der Simulation erstellt werden sollen

### Der Konstruktor

![image](https://user-images.githubusercontent.com/80975675/185797142-ffbf5491-667b-48b0-84ae-f57514c45204.png)

Im Konstruktor wird sich nur der *playerCount* geholt und dann wird schon die Simulation gestartet

### Die StartSimulation() Methode

Da das die größte Methode ist gliedere ich die nochmal in Einzelteile:

#### Das Erstellen des Kartendecks

![image](https://user-images.githubusercontent.com/80975675/185797584-059b7750-20b6-4d5c-bd79-f48143fdcf0e.png)

Zuerst einmal brauchen wir 52 Karten die Später an alle Spieler verteilt werden. Da es vier Zeichen gibt und pro Zeichen 13 verschiedene Karten brauchen wir 4*13 Karten. Also dachte ich mir man kann 2 for-Schleifen verwenden: Die erste würde 4x wiederholt werden um die 4 Zeichen durchzugehen und die zweite wird 13 mal wiederholt um die 13 Werte pro Zeichen durchzugehen.

> Das musst du dir sicher nicht merken aber ich habe rausgefunden das man mit ```Enum.ToObject(typeOf(DEIN_ENUM), int)``` mit einem int auf einen Enum Wert kommt. Also ist in dem Fall 0 = Herz, 1 = Kreuz, 2 = Pik, 3 = Karo. So kann ich immer den Wert aus der ersten for-Schleife nehmen und bekomme immer das richtige Zeichen

#### Das Erstellen der Spieler

![image](https://user-images.githubusercontent.com/80975675/185797610-02f6da1c-9048-458a-a1b7-f697a8895d72.png)

Hier wird einfach eine Schleife so oft wiederholt wie *playerCount* ist und in der Schleife wird immer ein neuer Spieler erstellt und in die Liste getan

#### Das Austeilen der Karten

![image](https://user-images.githubusercontent.com/80975675/185797674-7ab0a677-0e20-41bc-9604-05acc647e8f7.png)

Was diese Aufgabe etwas schwieriger gemacht hat ist das jeder Spieler zuerst nur EINE Karte bekommt, danach nur DREI auf den Tisch gelegt werden, dann bekommt jeder Spieler erst die zweite Karte und erst nach dem werden die letzten 2 Karten auf den Tisch gelegt.

Ich Teile diese Aufgabe einmal auf:
* 2x muss an jeden Spieler einie einzige Karte verteilt werden
* 2x müssen Karten auf den Tisch gelegt werden (Bei ersten mal 3 Karten beim zweitem mal 2 Karten)

Für beide dieser Aufgaben kann man Schleifen verwenden. Ich habe für die Spieler eine foreach-Schleife genommen die durch alle Spieler durchgeht und jedem Spieler genau eine zufällige Karte gibt und diese Karte aus dem Kartendeck entfernt damit sie niemand Anderes bekommen kann.

Für den Tisch hab ich eine for-Schleife verwendet die bei jeder Wiederholung eine zufällige Karte auf den Tisch legt und die Karte auch wieder aus dem Deck entfernt

Weil diese beiden Sachen genau 2x hin der Reihenfolge passieren müssen habe ich einfach diese beiden Schleifen nochmal in eine for-Schleife getan die 2x wiederholt wird.

Jetzt gibt es nur noch das Problem das beim ersten mal ja 3 Karten auf den Tisch gelegt werden sollen und beim zweiten mal 2 Karten. Wie du im Screenshot siehst habe ich in der Schleife für den Tisch ```j < 3 - i ``` geschrieben. Das - i löst das Problem weil es wird ja bei jeder Wiederholung von der Tisch schleife nur EINE Karte auf den Tisch gelegt. Das heißt wenn die Schleife 3x durchläuft kommen 3 neue Karten auf den Tisch, wenn sie 2x durchläuft kommen 2 neue Karten auf den Tisch,... und das i ist die Variable von der ganz oberen Schleife. Das heißt beim ersten Durchgang von der oberen Schleife ist i = 0 und beim zweitem ist i = 1. Alsoooo steht beim ersten mal ```j < 3 - 0``` da also wird die Schleife 3x wiederholt (also 3 Karten kommen auf den Tisch) und bei zweitem mal steht ```j < 3 - 1``` da also wird die Schleife 2x wiederholt (also 2 Karten kommen auf den Tisch)

#### Das Überprüfen Welches Muster die Spieler haben

```C#
 //Überprüfen was der Spieler hat

            Tries++;

            foreach(Player player in players)
            {
                if(CheckFlush(player))
                {
                    List<Card> flush = GetFlush(player);
                    if(CeckStreet(flush)) //Wenn das Flush auch eine Straße ist, ist es ein Straight flush
                    {
                        flush.Sort();
                        if(flush.ElementAt(4).Value == 14) //Wenn dieses Straight Flush jetzt auch noch als höchste Karte ein Ass hat ist ein ein Royal Flush. Das beste was man haben kann.
                        {
                            RoyalFlush++;
                            break;
                        }

                        StraightFlush++;
                        break;
                    }

                    Flush++;
                    break;
                }

                if(CeckStreet(player))
                {
                    Strasse++;
                    break;
                }

                if(CeckVireling(player))
                {
                    Vierling++;
                    break;
                }

                if (CeckDrilling(player))
                {
                    if(CheckPaarWithou(player, GetDrilling(player))) //Falls es ein Paar und ein Drilling gibt ist das ein Full House
                    {
                        FullHouse++;
                        break;
                    }

                    Drilling++;
                    break;
                }

                if(CheckDoublePaar(player))
                {
                    DoppelPaar++;
                    break;
                }

                if (CheckPaar(player))
                {
                    Paar++;
                    break;
                }

                Nothing++;
            }
```

1. Tries um 1 erhöhen weil egal was passiert es ist ja ein Versuch mehr also muss in jedem Fall Tries erhöt werden
2. Eine foreach-Schleife die jeden Spieler durchgeht weil ja bei jedem Spieler geschaut werden muss was er hat
3. Es gibt 3 Arten von Flush. Flush, Straight Flush und Royal Flush. Da Royal Flush das höchste ist was man haben kann überprüfe ich die Flushes als erstes
  * Ein Straight Flush ist ein Flush was auch eine Straße ist und ein Royal Flush ist ein Straight Flush wo die höchste Karte ein As ist. Das heißt egal welche Art von Flush es ist es ist immer ein Flush also wenn es kein Flush ist kann es auch kein Straight Flush oder Royal Flush sein. Deswegen überprüfe ich zuerst ob es ein Flush ist mit *CheckFlush(player)*
  * Wenn es ein Flush ist hol ich mit die Karten die ein Flush ergeben mit *GetFlush(player)* und überprüfe jetzt ob es ein Straight Flush ist indem ich schau ob das Flush auch eine Straße ist mit *CheckStreet(flush)* Wenn es kein Straight Flush ist kann es auch kein Royal Flush sein also weiß ich es ist ein normales Flush und kann Flush um 1 erhöhen und muss nicht weiter machen
  * Wenn es Straight Flush ist muss ich noch überprüfen ob es ein Royal Flush ist also ob die höchste Karte ein As ist (also den Wert 14 hat). Dafür sortiere ich die Liste damit die höchste Karte an letzer Stelle ist und schau ob der Wert der letzten Karte 14 ist. Wenn es das nicht ist kann ich Straight Flush um 1 erhöhen und kann aufhören. Wenn es doch so ist ist es Royal Flush also erhöhe ich das um 1 und kann auch aufhören
4. Wenn *CheckFlush(player)* schon false returnt also es keine Art von Flush ist kann ich mit *Checkstreet(player)* überprüfen ob es eine Straße ist
5. Ist es keien Straße überprüfe ich ob es ein Vierling ist
6. Ist es kein Vierling schau ich ob es ein Drilling ist
  * Wenn es ein Drilling ist kann es auch ein Full House sein falls es zusätzlich noch ein Paar gibt. Da muss man aber aufpassen das das Programm nicht den Drilling aus als Paar erkennt. Deswegen verwende ich *CheckPaarWithou(player, GetDrilling(player))* weil *GetDrilling(player)* gibt mir den Wert von den 3 gleihen Karten somit wird das von CheckPaarWithout ignoriert. Wenn das true returnt ist es ein Full House sonst ist es ein Drilling. In beiden Fällen kann ich aufhören
7. Danach schau ich ob es 2 verschieden Paare gibt
8. Wenn es das auch nicht schau ich ob es ein Paar gibt
9. Wenn nicht einmal das zutrifft dann erhöhe ich am Ende noch einmal Nothing um 1

#### Die Endrechnung und Ausgabe

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

Am Ende habe ich die Anzahl aller Muster die es gab. Die Dividiere ich jewails durch die Anzahl aller Durchgänge (Tries) und multipliziere es mit 100. So bekomm ich die Prozentuelle Wahrscheinlichkeit von allen Musten und kann diese jetzt ausgeben

### Die GetTableDeckAndPlayerDeck(Player player) Methode

Das war keine Aufgabe aber das ist eine Sehr nützliche Methode die ich sehr oft verwendet habe

![image](https://user-images.githubusercontent.com/80975675/185799755-4748cbd7-e0bd-428e-9b50-c9318314d691.png)

Dafür habe ich einfach eine neue Liste erstellt und in die neue Liste alle Karten vom Tisch und die 2 Karten vom Spieler reingeten. Dann hatte ich eine Liste mit 7 Karten und hab die returnt
> Damit kann man viel leichter alle Karten sortieren und Muster erkennen

### Die CheckFlush(Player player) Methode

![image](https://user-images.githubusercontent.com/80975675/185799848-1fe6b3b5-4e92-424c-8684-0e8001ab3e03.png)

Zuerst habe ich mir die Liste mit den 7 Karten geholt und mit dem *CardColorComparer()* nach Farben sortiert.
Dann bin ich alle Karten durchgegangen und habe imemr eine Karte mit der darauffolgenden mit dem | Operator verglichen (Der sagt ja ob 2 Karten dieselbe Farbe haben)
Wenn eine Karte mit der nächsten dieselbe Farbe hat wird *equalColorCount* um 1 erhöt bis es 5 gleiche Karten mit der selben Farbe hat. Wenn es bis zum ende der Schleife keine 5 Karten mit der gleichen Farbe gibt wird false returnt sobald *equalColorCount* auf 5 steht wird true zurückgegeben

### Die GetFlush(Player player) Methode

![image](https://user-images.githubusercontent.com/80975675/185800091-bdbbdd98-a408-4dba-84f9-09e50d320c18.png)

Funktioniert genau gleich wie die *CheckFlush* Methode nur das sie nicht eine int Variable um 1 erhöt wenn 2 Karten dieselbe Farbe haben sonder sie gibt die Karten mit der gleichen Farbe in eine Liste und wenn diese Liste 5 Karten drinnen hat wird sie zurückgegeben wenn nicht wird null zurückgegeben

### Die CheckStreet(List<Card> cardDeck) Methode

![image](https://user-images.githubusercontent.com/80975675/185800434-6378bdcd-24f3-47dd-8cca-9b1b9045757c.png)

Diese Methode bekommt eine Liste mit 7 Karten, geht diese Liste durch und schaut ob die nächste Karten eine Wert hat der um 1 höher ist als die der der aktuellen Karte. Wenn es 5 Karten in eienr Reihe gibt bei denne das so ist wird true zurückgegeben sonst false

### Die CheckStreet(Player player) Methode

![image](https://user-images.githubusercontent.com/80975675/185800449-2040963e-e5e4-43d1-95a5-0c5091e569bd.png)

Diese Methode macht genau dasseleb wie die CheckStreet(List<Crad> cardDeck) Methode nur das sie einen Spieler bekommt. Deswegen hol ich mit mit GetTableDeckAndPlayerDeck(player) die Karten und rufe die schon geschriebene Methode auf damit ich nicht 2x denselben Code habe.

### Die CheckViewling(Player player) Methode

![image](https://user-images.githubusercontent.com/80975675/185800570-d8d7f8be-a1ac-4071-8d5a-3bc3050f78cb.png)

Diese Methode vergleicht jede Karte mit jeder anderen und schaut wie viele Karten es gibt mit demselben Wert. Wenn es 4 gleiche gibt returnt sie true

### Die CheckDrilling(Player player) Methode

![image](https://user-images.githubusercontent.com/80975675/185800810-e42d314b-c7e1-44bb-af2b-d0abf3b2d045.png)

Dasselbe wie die *CheckVierling* Merhode nur sie returnt schon bei 3 true

### Die GetDrilling(Player player) Methode

![image](https://user-images.githubusercontent.com/80975675/185800866-00e6fd1b-6bb2-4d8c-8321-ab73123b90f8.png)

Dasselbe wie die *CheckDrilling* Methode nur sie gibt den Wert zurück den die 3 gleichen Karten haben

### Die CheckPaar(Player player) Methode

![image](https://user-images.githubusercontent.com/80975675/185800986-8361aa54-b913-4b95-bf63-3868e91a9a6f.png)

Vergleicht jede Karte mit jeder und schaut ob eine Karten den gleichen Wert hat wie eine andere

### Die GetPaar(Player player) Methode

![image](https://user-images.githubusercontent.com/80975675/185801040-07ec7658-18ae-434a-999b-aeaa037d48a2.png)

Macht dasselbe wie die *CheckPaar* Methode nur das sie am Ende den Wert von den beiden Karten zurückgibt

### Die CheckPaarWithout(Player player, int forbittenValue) Methode

![image](https://user-images.githubusercontent.com/80975675/185801136-1d3e3d9b-b7fa-45c2-848c-2c927341918d.png)

Macht dasselbe wie die *CheckPaar* Methode nur das sie alle Paare mit einem bestimmten Wert auslässt

### Die CheckDoublePaar(Player player) Methode

![image](https://user-images.githubusercontent.com/80975675/185801365-f1f8acd3-5724-4c1c-a68e-a342316c91a1.png)

Sucht nach 2 verschiedenen Paaren

## Main Klasse

![image](https://user-images.githubusercontent.com/80975675/185801439-00e83fcc-e899-484b-8dbe-43f774bbac22.png)

Da gibt es ganz viele Möglichkeiten das zu lösen aber das ist eine Möglichkeit
