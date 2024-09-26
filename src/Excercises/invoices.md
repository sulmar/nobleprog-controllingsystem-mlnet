# Księgowanie faktur

Poniżej znajduje się przykładowy zbiór danych faktur w formacie CSV. Zawiera on kolumny takie jak InvoiceID, Amount, Description, MPK oraz IsIncorrect. Kolumna IsIncorrect zawiera etykiety, gdzie 0 oznacza, że faktura jest poprawna, a 1, że jest błędna.

```csv
InvoiceID,Amount,Description,MPK,IsIncorrect
1,1000.00,Zakup materiałów biurowych,001,0
2,25000.00,Zakup sprzętu IT,002,0
3,150.00,Lunch z klientem,003,0
4,3000.00,Naprawa samochodu firmowego,004,0
5,5000.00,Konsultacje prawne,005,0
6,700.00,Usługi marketingowe,001,1
7,12000.00,Zakup laptopów,002,0
8,450.00,Kurs języka angielskiego dla pracowników,003,1
9,35000.00,Wyposażenie biura,006,0
10,2000.00,Zakup oprogramowania,002,0
11,800.00,Kolacja biznesowa,003,0
12,7000.00,Koszty rekrutacji,007,0
13,250.00,Kwiaty na uroczystość firmową,001,1
14,20000.00,Modernizacja serwerowni,002,0
15,1500.00,Naprawa sprzętu komputerowego,002,0
16,50.00,Kawa i przekąski,001,0
17,4500.00,Projekt graficzny strony internetowej,005,0
18,500.00,Książki branżowe,003,0
19,18000.00,Organizacja konferencji,008,0
20,1000.00,Opłata za usługi kurierskie,001,0
21,600.00,Usługi szkoleniowe,001,1
22,80000.00,Remont biura,009,0
23,900.00,Wyjazd integracyjny,003,1
24,450.00,Opłata za media,004,0
25,1200.00,Naprawa drukarki,002,0
```

## Objaśnienia:
- **InvoiceID**: Unikalny identyfikator faktury.
- **Amount**: Kwota faktury (w złotówkach).
- **Description**: Opis faktury.
- **MPK**: Miejsce powstawania kosztów.
- **IsIncorrect**: Etykieta klasyfikacyjna, gdzie 0 oznacza poprawną fakturę, a 1 błędną.

## Potencjalne błędy w fakturach:
- W przypadku błędnych faktur (IsIncorrect = 1), opis nie odpowiada standardowym wydatkom dla danego MPK lub kwota może być nietypowa dla danego typu wydatków. Na przykład, szkolenie językowe dla MPK 003 lub usługi marketingowe przypisane do MPK 001.
- Jeśli MPK 003 w tym przykładzie odpowiada na przykład Kosztom reprezentacyjnym lub Wyjazdom służbowym, to szkolenie językowe nie powinno tam być zaksięgowane.
- Szkolenie językowe zwykle powinno być przypisane do MPK związanego z zasobami ludzkimi (np. MPK 007 - Szkolenia i rozwój pracowników)