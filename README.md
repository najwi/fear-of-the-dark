# Fear of the dark - 2d Dungeon Crawler made with Unity Engine.
![screen1](./screen1.png)
![screen2](./screen2.png)
&nbsp;
&nbsp;

# 1. Tytuł i autorzy
## 1.1 Tytuł
  Fear of the dark
  
## 1.2 Autorzy
   - Bartłomiej Jagiełło
   - Michał Najwer
   - Maciej Kopiński

# 2. Przegląd gry
## 2.1 Koncept gry
  - Gra 2D typu dungeon crawler
  - Widok z góry
  - Mroczny motyw

## 2.2 Gatunek
  Rougelike, strzelanka.
  
## 2.3 Przepływ gry
  Gra składa się z 10 pokojów. Gracz przechodzi kolejne pokoje o rosnącej trudności przeciwników, aby w otstanim pokoju zmierzyć się z bossem. Przejście pokoju polega na pokonaniu wszystkich przeciwników.

## 2.4 Wygląd i odczucia gracza
  Dynamiczna rozgrywka o wysokim poziomie trudności.

# 3. Rozgrywka i mechaniki
## 3.1 Rozgrywka
  Celem gry jest pokonanie Beelzebossa (ostatniego przeciwnika, dużo silniejszego od pozostałych) i odzyskanie Gitary Przeznaczenia.

## 3.2 Mechaniki
### 3.2.1 Ruch w grze
  Ruch w dowolnym z czeterch kierunków: lewo, prawo, góra, dół. Ograniczony przez obiekty na planszy, ściany i przeciwników.
  
### 3.2.2 Przedmioty
  - Serduszko - Uzupełnia brakujące zdrowie gracza.
  - Bomba - Pozwala na wysadzanie terenu.
  - Nutka - Waluta, pozwala kupić przedmioty u sklepikarza.
  - Kostka - Zwiększa statystyki gracza. Różne kolory kostek wzmacniają różne statystki.
  - Gitara - Ostateczny przedmiot do zdobycia, zdobycie kończy grę.

### 3.2.3 Walka
  Gracz strzela pociskami ze swojej gitary oraz ma możliwość postawienia bomby która zadaje obrażenia przeciwnikom i niszczy teren.

### 3.2.4 Ekonomia
  Zbieranie nutek z przeciwników i wydawanie ich w sklepie na przedmioty (z punktu 3.2.2
Przedmioty z wyjątkiem nutek).


## 3.3 Opcje Gry
  - Zmiana głośności.
  - Pauza.
  
# 4. Fabuła i postacie
## 4.1 Historia
  Gra osadzona w świecie współczesnym. Głównym bohaterem jest profesjonalny gitarzysta zespołu metalowego David. Pewnego dnia po koncercie, do jego autokaru włamały się demony i sam Beelzeboss ukradł mu gitarę przeznaczenia, którą David musi teraz odzyskać.

## 4.2 Świat
  Świat w podziemiach, mroczny. Mapa świata podzielona jest na plansze.
  Plansze małe, w większości zawierające przeciwników. Plansze specjalne (początkowa, z bossem).
  Mapa składa się z minimum jednego poziomu, natomiast ten - z przynajmniej 10 mniejszych pokoi (planszy). Każdy pokój będzie powstawał na bazie predefiniowanego szablonu, natomiast generacja mapy polegać będzie na łączeniu ze sobą pokoi. Przy generowaniu mapy, niezbędne będzie sprawdzanie, czy dany pokój może zostać dołożony do już wygenerowanej mapy oraz zapewnienie dalszej ścieżki do ostatniego pokoju.
  
  ![Map1](./screenshots/map1.png) ![Map2](./screenshots/map2.png)
  
## 4.3 Postacie
  David - Głowny bohater, któremu diabeł ukradł gitarę. Zawodowo gra w światowej sławy zespole rockowym. Posiada żelazną wolę i niezwykłe umiejętności gitarowe.

# 5. Interfejs
## 5.1 System interfejsu użytkownika
  W czasie gry wyświetlanie aktualnego i maksymalnego stanu zdrowia gracza oraz wyświetlanie przedmiotów i statystyk gracza. <br />
  W czasie walki z bossem wyświetlanie punktów życia bossa.

## 5.2 Kontroler
  Ruchy i zachowania gracza sterowane za pomocą klawiatury. Interfejs sterowany za pomocą klawiatury lub myszki.
  
## 5.3 Muzyka i dźwięk
  Muzyka spokojna w tle podczas przechodzenia poziomów i muzyka metal/rock podczas walki z bossem.

## 5.4 System pomocy
  W dowolnym momencie możliwość sprawdzenia skrótów klawiszowych w menu.

# 6. Sztuczna inteligencja
## 6.1 SI przeciwników
  Przeciwnicy reagujący na zachowanie gracza poprzez np. unikanie pocisków, zastawianie przejścia do innego przeciwnika.
Każdy przeciwnik posiadający swoją własną strategię, co najmniej 4 przeciwników.
 - Przeciwnik strzelający, który będzie trzymał sie z daleka od gracza.
 - Przeciwnik biegnący na gracza i robiący uniki.
 - Przeciwnik z tarczą, który będzie stawał pomiędzy graczem, a przeciwnikiem strzelającym.
 - Ostatni boss z unikatowym zachowaniem, posiadający znacznie większą ilość zdrowia od
zwykłych przeciwników. Atakuje poprzez rzucanie kulą ognia, gdy jego życie spadnie
poniżej połowy zaczyna rzucać kilkoma kulami naraz Atakuje wręcz gdy gracz podejdzie za
blisko.

## 6.2 Neutralne i przyjazne SI
  Neutralny sklepikarz pojawiający się w trakcie gry oferujący przedmioty dla gracza za
opłatą. Do kupienia są 3 losowe przedmioty (Serduszko, Bomba, Kostka – różne rodzaje).
Mogą się powtarzać. Przedmiot u jednego sklepikarza będzie można kupić tylko raz. Walutą
są Nutki.


# 7. Sprzęt
## 7.1 Sprzęt docelowy
  Komputer stacjonarny/laptop z Windows 10

## 7.2 Silnik gry, środowisko programistyczne
  - Silnik: Unity 2020  
  - Środowisko: Visual Studio 2019

# 8. Grafika
  Grafika pikselowa z nastawieniem na ciemny motyw. Paleta głównych kolorów to czarny, czerwony, szary, zółty, brązowy.
Tło podziemii, lochu lub piekła. Przeciwnicy w podobnym motywie: nieumarli, demony. <br />

