# Gra Snake - Wersja dla Dwóch Graczy

Gra Snake rozszerzona o tryb dwóch graczy.

## O Grze

Snake to klasyczna gra zręcznościowa, w której sterujemy wężem poruszającym się po planszy. Celem jest zbieranie gwiazdek, które powodują wydłużenie węża. Gra kończy się, gdy wąż uderzy w ścianę, sam siebie lub drugiego gracza.

W tej wersji:
- Dwóch graczy rywalizuje na jednej planszy
- Każdy gracz kontroluje swojego węża
- Wygrywa ten, kto przetrwa dłużej
- Każda zebrana gwiazdka zwiększa wynik i długość węża

## Sterowanie

### Gracz 1 (Czerwony wąż)
- **W** - ruch w górę
- **S** - ruch w dół
- **A** - ruch w lewo
- **D** - ruch w prawo

### Gracz 2 (Niebieski wąż)
- **Strzałka w górę** - ruch w górę
- **Strzałka w dół** - ruch w dół
- **Strzałka w lewo** - ruch w lewo
- **Strzałka w prawo** - ruch w prawo


## Jak Zbudować

### Sposób 1: Visual Studio
1. Otwórz projekt w Visual Studio
2. Wybierz Build → Build Solution (lub naciśnij Ctrl+Shift+B)
3. Plik wykonywalny znajdzie się w folderze `bin/Debug` lub `bin/Release`

## Jak Uruchomić

### Z Visual Studio
Naciśnij F5 lub wybierz Debug → Start Debugging

### Z linii komend
Po zbudowaniu projektu przejdź do folderu z plikiem wykonywalnym:
```bash
cd bin/Debug
Program.exe
```


## Struktura Projektu
```
snake-game/
├── Snake.cs     - główna logika gry
├── Pixel.cs        - klasa reprezentująca element węża
├── Obstakel.cs        - klasa reprezentująca przeszkody
└── README.md       - ten plik
```

## Twórcy

Jakub Grąbka - 147593

Adam Kołt - 151982