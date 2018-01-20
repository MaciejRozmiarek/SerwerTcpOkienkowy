---
SerwerTcpOkienkowy (Win Forms) który oczekuje na podłączenie od wielu Clientów
Dane otrzymane z zapytania zostają zserializowane i wysłane jako byte do Clienta któr po otrzymaniu go wykonuje deserializacje.

---

Jak odpalić?

Projekt należy pobrać i skompilować np. Visual Studio

---

Jak przetestować?

---


- Po uruchomieniu aplikacji Server - należy ustawić adres IP oraz Port na którym Serwer oczekuje na połączenia.
- Należy mieć również uruchomiony np. Xampp z uruchomioną bazą MySql do którego dane należy uzupełnić IP bazy, Nazwę Bazy, Login oraz Hasło.
- Po uruchomieniu Server oczekuje na połączenia w nowym wątku - obsługuje wielu Clientów
- Po otrzymaniu zapytania SQL Server łączy się do Bazy Danych i wykonuje zapytanie.
- Otrzymane Dane są serializowane i wysyłane do Clienta
- Jeżeli Baza Danych zwróciła błąd jest to przesyłane do Clienta.

