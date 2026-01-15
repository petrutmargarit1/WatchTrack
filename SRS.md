# Software Requirements Specification (SRS)
# WatchTrack Pro

**Version:** 1.0  
**Date:** 15 Ianuarie 2026  
**Project:** IS2 (Software Engineering II)

---

## 1. Introducere

### 1.1 Scopul Documentului
Acest document descrie cerințele funcționale și non-funcționale pentru aplicația WatchTrack Pro, o platformă web pentru urmărirea și gestionarea filmelor și serialelor vizionate de utilizatori.

### 1.2 Scopul Produsului
WatchTrack Pro permite utilizatorilor să:
- Urmărească filmele și serialele pe care le vizionează
- Gestioneze un watchlist cu conținut de vizionat
- Ofere rating-uri și review-uri pentru conținut
- Monitorizeze progresul vizionării la nivel de episod pentru seriale

### 1.3 Definiții și Acronime
- **API**: Application Programming Interface
- **REST**: Representational State Transfer
- **DTO**: Data Transfer Object
- **CRUD**: Create, Read, Update, Delete
- **UI**: User Interface

### 1.4 Referințe
- Documentația .NET 8.0
- PostgreSQL Documentation
- REST API Best Practices

---

## 2. Descrierea Generală

### 2.1 Perspectiva Produsului
WatchTrack este o aplicație web independentă compusă din:
- Backend REST API (.NET 8.0)
- Bază de date relațională (SQLite/PostgreSQL)
- Frontend web (HTML/CSS/JavaScript)
- Interfață Swagger pentru testare API

### 2.2 Funcțiile Produsului
- Gestionare utilizatori
- Catalogare filme și seriale
- Organizare seriale pe sezoane și episoade
- Sistem de rating și review-uri
- Watchlist personalizat
- Istoric vizionări

### 2.3 Caracteristicile Utilizatorilor
**Utilizator final:**
- Iubitor de filme și seriale
- Dorește să țină evidența conținutului vizionat
- Nivel mediu de cunoștințe tehnice
- Acces la browser web modern

### 2.4 Constrângeri
- Aplicația trebuie să ruleze în browsere moderne (Chrome, Firefox, Safari, Edge)
- Backend trebuie să fie compatibil cu .NET 8.0
- Baza de date trebuie să suporte relații complexe
- Trebuie să fie deployable cu Docker

### 2.5 Asumpții și Dependențe
- Utilizatorii au acces la internet
- Serverul are resurse suficiente pentru rularea containerelor Docker
- .NET SDK 8.0 este instalat pentru dezvoltare locală

---

## 3. Cerințe Funcționale

### 3.1 Gestionare Utilizatori

#### FR-1.1: Creare Utilizator
**Descriere:** Sistemul trebuie să permită crearea de conturi noi.

**Input:** Username, Email  
**Output:** Utilizator creat cu ID unic și timestamp  
**Validări:**
- Username: obligatoriu, unic, minimum 3 caractere
- Email: obligatoriu, unic, format valid

#### FR-1.2: Vizualizare Utilizatori
**Descriere:** Sistemul trebuie să permită listarea tuturor utilizatorilor.

**Output:** Lista de utilizatori cu detalii complete

#### FR-1.3: Vizualizare Detalii Utilizator
**Descriere:** Sistemul trebuie să permită vizualizarea unui utilizator specific.

**Input:** User ID  
**Output:** Detalii complete utilizator

#### FR-1.4: Actualizare Utilizator
**Descriere:** Sistemul trebuie să permită modificarea datelor utilizatorului.

**Input:** User ID, Username nou, Email nou  
**Output:** Utilizator actualizat

#### FR-1.5: Ștergere Utilizator
**Descriere:** Sistemul trebuie să permită ștergerea unui utilizator.

**Input:** User ID  
**Output:** Confirmare ștergere  
**Efect:** Ștergere în cascadă a tuturor datelor asociate

---

### 3.2 Gestionare Filme

#### FR-2.1: Adăugare Film
**Descriere:** Sistemul trebuie să permită adăugarea de filme noi.

**Input:** Title, Description, Release Year, Genre, Duration (minutes), Poster URL (opțional)  
**Output:** Film creat cu ID unic

#### FR-2.2: Vizualizare Filme
**Descriere:** Sistemul trebuie să permită listarea tuturor filmelor.

**Output:** Lista de filme cu detalii complete

#### FR-2.3: Vizualizare Detalii Film
**Descriere:** Sistemul trebuie să permită vizualizarea unui film specific.

**Input:** Movie ID  
**Output:** Detalii complete film

#### FR-2.4: Actualizare Film
**Descriere:** Sistemul trebuie să permită modificarea informațiilor despre film.

**Input:** Movie ID, Date noi  
**Output:** Film actualizat

#### FR-2.5: Ștergere Film
**Descriere:** Sistemul trebuie să permită ștergerea unui film.

**Input:** Movie ID  
**Output:** Confirmare ștergere

---

### 3.3 Gestionare Seriale

#### FR-3.1: Adăugare Serial
**Descriere:** Sistemul trebuie să permită adăugarea de seriale noi.

**Input:** Title, Description, Release Year, Genre, Poster URL (opțional)  
**Output:** Serial creat cu ID unic

#### FR-3.2: Vizualizare Seriale
**Descriere:** Sistemul trebuie să permită listarea tuturor serialelor.

**Output:** Lista de seriale cu detalii complete

#### FR-3.3: Vizualizare Detalii Serial
**Descriere:** Sistemul trebuie să permită vizualizarea unui serial specific.

**Input:** Series ID  
**Output:** Detalii complete serial

#### FR-3.4: Actualizare Serial
**Descriere:** Sistemul trebuie să permită modificarea informațiilor despre serial.

**Input:** Series ID, Date noi  
**Output:** Serial actualizat

#### FR-3.5: Ștergere Serial
**Descriere:** Sistemul trebuie să permită ștergerea unui serial.

**Input:** Series ID  
**Output:** Confirmare ștergere  
**Efect:** Ștergere în cascadă a sezoanelor și episoadelor

---

### 3.4 Gestionare Sezoane

#### FR-4.1: Adăugare Sezon
**Descriere:** Sistemul trebuie să permită adăugarea de sezoane la un serial.

**Input:** Season Number, Title, Release Year, Series ID  
**Output:** Sezon creat cu ID unic  
**Validări:**
- Series ID trebuie să existe
- Season Number trebuie să fie unic pentru serialul respectiv

#### FR-4.2: Vizualizare Sezoane
**Descriere:** Sistemul trebuie să permită listarea tuturor sezoanelor.

**Output:** Lista de sezoane cu detalii complete

#### FR-4.3: Filtrare Sezoane după Serial
**Descriere:** Sistemul trebuie să permită vizualizarea sezoanelor unui serial specific.

**Input:** Series ID  
**Output:** Lista de sezoane pentru serialul respectiv

#### FR-4.4: Actualizare Sezon
**Descriere:** Sistemul trebuie să permită modificarea informațiilor despre sezon.

**Input:** Season ID, Date noi  
**Output:** Sezon actualizat

#### FR-4.5: Ștergere Sezon
**Descriere:** Sistemul trebuie să permită ștergerea unui sezon.

**Input:** Season ID  
**Output:** Confirmare ștergere  
**Efect:** Ștergere în cascadă a episoadelor

---

### 3.5 Gestionare Episoade

#### FR-5.1: Adăugare Episod
**Descriere:** Sistemul trebuie să permită adăugarea de episoade la un sezon.

**Input:** Episode Number, Title, Description, Duration (minutes), Air Date, Season ID  
**Output:** Episod creat cu ID unic  
**Validări:**
- Season ID trebuie să existe
- Episode Number trebuie să fie unic pentru sezonul respectiv

#### FR-5.2: Vizualizare Episoade
**Descriere:** Sistemul trebuie să permită listarea tuturor episoadelor.

**Output:** Lista de episoade cu detalii complete

#### FR-5.3: Filtrare Episoade după Sezon
**Descriere:** Sistemul trebuie să permită vizualizarea episoadelor unui sezon specific.

**Input:** Season ID  
**Output:** Lista de episoade pentru sezonul respectiv

#### FR-5.4: Actualizare Episod
**Descriere:** Sistemul trebuie să permită modificarea informațiilor despre episod.

**Input:** Episode ID, Date noi  
**Output:** Episod actualizat

#### FR-5.5: Ștergere Episod
**Descriere:** Sistemul trebuie să permită ștergerea unui episod.

**Input:** Episode ID  
**Output:** Confirmare ștergere

---

### 3.6 Sistem Review-uri

#### FR-6.1: Adăugare Review
**Descriere:** Sistemul trebuie să permită utilizatorilor să adauge review-uri pentru filme sau seriale.

**Input:** Rating (1-10), Comment, User ID, Movie ID sau Series ID  
**Output:** Review creat cu ID unic și timestamp  
**Validări:**
- Rating trebuie să fie între 1 și 10
- User ID trebuie să existe
- Movie ID SAU Series ID trebuie să existe (unul din cele două)

#### FR-6.2: Vizualizare Review-uri
**Descriere:** Sistemul trebuie să permită listarea tuturor review-urilor.

**Output:** Lista de review-uri cu detalii complete

#### FR-6.3: Filtrare Review-uri după Utilizator
**Descriere:** Sistemul trebuie să permită vizualizarea review-urilor unui utilizator.

**Input:** User ID  
**Output:** Lista de review-uri ale utilizatorului

#### FR-6.4: Filtrare Review-uri după Film/Serial
**Descriere:** Sistemul trebuie să permită vizualizarea review-urilor pentru un film sau serial.

**Input:** Movie ID sau Series ID  
**Output:** Lista de review-uri pentru conținutul respectiv

#### FR-6.5: Actualizare Review
**Descriere:** Sistemul trebuie să permită modificarea unui review.

**Input:** Review ID, Rating nou, Comment nou  
**Output:** Review actualizat

#### FR-6.6: Ștergere Review
**Descriere:** Sistemul trebuie să permită ștergerea unui review.

**Input:** Review ID  
**Output:** Confirmare ștergere

---

### 3.7 Istoric Vizionări

#### FR-7.1: Marcare Conținut ca Vizionat
**Descriere:** Sistemul trebuie să permită marcarea unui film sau episod ca vizionat.

**Input:** User ID, Movie ID sau Episode ID, Completed (boolean)  
**Output:** Înregistrare în istoric cu timestamp  
**Validări:**
- User ID trebuie să existe
- Movie ID SAU Episode ID trebuie să existe

#### FR-7.2: Vizualizare Istoric
**Descriere:** Sistemul trebuie să permită vizualizarea întregului istoric de vizionări.

**Output:** Lista completă de înregistrări din istoric

#### FR-7.3: Filtrare Istoric după Utilizator
**Descriere:** Sistemul trebuie să permită vizualizarea istoricului unui utilizator.

**Input:** User ID  
**Output:** Lista de vizionări ale utilizatorului

#### FR-7.4: Actualizare Status Vizionare
**Descriere:** Sistemul trebuie să permită modificarea statusului de vizionare.

**Input:** History ID, Completed nou  
**Output:** Înregistrare actualizată

#### FR-7.5: Ștergere din Istoric
**Descriere:** Sistemul trebuie să permită ștergerea unei înregistrări din istoric.

**Input:** History ID  
**Output:** Confirmare ștergere

---

### 3.8 Watchlist

#### FR-8.1: Adăugare în Watchlist
**Descriere:** Sistemul trebuie să permită adăugarea de filme sau seriale în watchlist.

**Input:** User ID, Movie ID sau Series ID  
**Output:** Înregistrare în watchlist cu timestamp  
**Validări:**
- User ID trebuie să existe
- Movie ID SAU Series ID trebuie să existe

#### FR-8.2: Vizualizare Watchlist
**Descriere:** Sistemul trebuie să permită vizualizarea întregului watchlist.

**Output:** Lista completă de înregistrări din watchlist

#### FR-8.3: Filtrare Watchlist după Utilizator
**Descriere:** Sistemul trebuie să permită vizualizarea watchlist-ului unui utilizator.

**Input:** User ID  
**Output:** Lista de conținut din watchlist-ul utilizatorului

#### FR-8.4: Ștergere din Watchlist
**Descriere:** Sistemul trebuie să permită ștergerea unui item din watchlist.

**Input:** Watchlist ID  
**Output:** Confirmare ștergere

---

### 3.9 Interfață Web

#### FR-9.1: Dashboard
**Descriere:** Interfața trebuie să afișeze un dashboard cu statistici generale.

**Output:** 
- Număr total utilizatori
- Număr total filme
- Număr total seriale
- Număr total sezoane
- Număr total episoade
- Număr total review-uri

#### FR-9.2: Navigare prin Tab-uri
**Descriere:** Interfața trebuie să permită navigarea între diferite secțiuni.

**Secțiuni:**
- Dashboard
- Utilizatori
- Filme
- Seriale
- Review-uri
- Watchlist

#### FR-9.3: Vizualizare Ierarhică Seriale
**Descriere:** Interfața trebuie să permită navigarea ierarhică: Serial → Sezoane → Episoade.

**Flow:**
1. Utilizatorul vede lista de seriale
2. Click pe serial → Afișează sezoanele serialului
3. Click pe sezon → Afișează episoadele sezonului
4. Buton "Înapoi" pentru navigare inversă

#### FR-9.4: Actualizare Automată Date
**Descriere:** Interfața trebuie să încarce automat datele la pornire și la refresh.

**Comportament:** Fetch toate datele din API și afișare în timp real

---

## 4. Cerințe Non-Funcționale

### 4.1 Performanță

#### NFR-1.1: Timp de Răspuns API
- Cerință: API-ul trebuie să răspundă la cereri în maxim 500ms pentru operații simple
- Măsurare: Timpul între request și response pentru operații CRUD

#### NFR-1.2: Capacitate
- Cerință: Sistemul trebuie să suporte minimum 100 utilizatori concurenți
- Măsurare: Load testing cu 100+ conexiuni simultane

#### NFR-1.3: Timp de Încărcare Frontend
- Cerință: Pagina web trebuie să se încarce în maxim 3 secunde
- Măsurare: Timpul de la request până la DOMContentLoaded

### 4.2 Securitate

#### NFR-2.1: Validare Input
- Cerință: Toate input-urile utilizatorilor trebuie validate server-side
- Implementare: Data annotations și validări în servicii

#### NFR-2.2: Prevenție SQL Injection
- Cerință: Sistemul trebuie să fie protejat împotriva SQL injection
- Implementare: Folosire Entity Framework Core cu parametrizare automată

#### NFR-2.3: CORS
- Cerință: API-ul trebuie să permită CORS pentru frontend
- Implementare: Configurare CORS policy în Program.cs

### 4.3 Disponibilitate

#### NFR-3.1: Uptime
- Cerință: Sistemul trebuie să fie disponibil 99% din timp
- Măsurare: Monitorizare uptime

#### NFR-3.2: Recuperare după Erori
- Cerință: Sistemul trebuie să gestioneze elegant erorile
- Implementare: Try-catch blocks, status codes HTTP corecte

### 4.4 Mentenabilitate

#### NFR-4.1: Arhitectură Modulară
- Cerință: Codul trebuie organizat în straturi separate (Controller, Service, Repository)
- Implementare: Separare clară a responsabilităților

#### NFR-4.2: Documentație API
- Cerință: API-ul trebuie să fie documentat complet
- Implementare: Swagger/OpenAPI cu descrieri pentru toate endpoint-urile

#### NFR-4.3: Cod Curat
- Cerință: Codul trebuie să respecte naming conventions și best practices C#
- Măsurare: Code review, linting

### 4.5 Portabilitate

#### NFR-5.1: Deployment Docker
- Cerință: Aplicația trebuie să poată fi deployată cu Docker
- Implementare: Dockerfile și docker-compose.yml

#### NFR-5.2: Compatibilitate Browsere
- Cerință: Interfața trebuie să funcționeze pe browsere moderne
- Suport: Chrome 90+, Firefox 88+, Safari 14+, Edge 90+

#### NFR-5.3: Cross-Platform Backend
- Cerință: Backend-ul trebuie să ruleze pe Windows, Linux, macOS
- Implementare: .NET 8.0 (cross-platform)

### 4.6 Scalabilitate

#### NFR-6.1: Bază de Date Scalabilă
- Cerință: Baza de date trebuie să suporte creștere de date
- Implementare: PostgreSQL cu indexare corespunzătoare

#### NFR-6.2: API Stateless
- Cerință: API-ul trebuie să fie stateless pentru scalabilitate orizontală
- Implementare: Fără session state pe server

---

## 5. Cerințe de Interfață

### 5.1 Interfață Utilizator

#### UI-1: Design Responsive
- Cerință: Interfața trebuie să fie responsive și să funcționeze pe desktop și tabletă
- Implementare: CSS media queries, grid layout

#### UI-2: Design Modern
- Cerință: Interfața trebuie să folosească design modern (glassmorphism, gradienți)
- Implementare: CSS cu backdrop-filter, gradienți, animații

#### UI-3: Feedback Vizual
- Cerință: Interfața trebuie să ofere feedback vizual pentru acțiuni
- Implementare: Hover effects, animații, status bar cu stări

### 5.2 Interfață API

#### API-1: REST Standard
- Cerință: API-ul trebuie să respecte principiile REST
- Implementare: HTTP methods corecte (GET, POST, PUT, DELETE)

#### API-2: Status Codes HTTP
- Cerință: API-ul trebuie să returneze status codes corecte
- Implementare:
  - 200 OK pentru succes
  - 201 Created pentru creare
  - 404 Not Found pentru resurse inexistente
  - 400 Bad Request pentru validări eșuate

#### API-3: Format JSON
- Cerință: API-ul trebuie să folosească JSON pentru request/response
- Implementare: Content-Type: application/json

### 5.3 Interfață Bază de Date

#### DB-1: Relații între Entități
- Cerință: Baza de date trebuie să mențină integritatea referențială
- Implementare: Foreign keys, cascade delete

#### DB-2: Indexare
- Cerință: Câmpurile folosite frecvent în căutări trebuie indexate
- Implementare: Index pe Username, Email (unique)

---

## 6. Constrângeri de Design

### 6.1 Tehnologii Obligatorii
- Backend: .NET 8.0, ASP.NET Core Web API
- ORM: Entity Framework Core
- Bază de date: SQLite (dev) / PostgreSQL (production)
- Deployment: Docker, docker-compose
- Documentație API: Swagger/OpenAPI

### 6.2 Arhitectură
- Pattern: Layered Architecture (Controller → Service → Repository)
- DTO: Folosire obligatorie pentru separare concern-uri
- Dependency Injection: Folosire pentru toate serviciile

---

## 7. Cerințe Proiect IS2

### 7.1 Punctaj Minim (8 puncte)

#### Componente Obligatorii:
1. **Controllers (1 punct)**: Minimum 8 controllere REST
2. **Services (2 puncte)**: Minimum 8 servicii cu interfețe
3. **Entities (2 puncte)**: Minimum 8 entități cu relații
4. **DTOs (2 puncte)**: Minimum 24 DTOs (Read, Create, Update pentru fiecare entitate)
5. **Docker (1 punct)**: Deployment funcțional cu docker-compose

### 7.2 Entități Implementate
1. User
2. Movie
3. Series
4. Season
5. Episode
6. Review
7. WatchHistory
8. Watchlist

---

## 8. Cazuri de Utilizare (Use Cases)

### UC-1: Utilizator Adaugă Film în Watchlist

**Actor:** Utilizator  
**Precondiție:** Utilizatorul și filmul există în sistem  
**Flow Principal:**
1. Utilizatorul selectează un film
2. Sistemul afișează detaliile filmului
3. Utilizatorul apasă "Adaugă în Watchlist"
4. Sistemul creează înregistrare în watchlist cu timestamp
5. Sistemul confirmă adăugarea

**Postcondiție:** Filmul apare în watchlist-ul utilizatorului

---

### UC-2: Utilizator Marchează Episod ca Vizionat

**Actor:** Utilizator  
**Precondiție:** Utilizatorul și episodul există în sistem  
**Flow Principal:**
1. Utilizatorul navighează la un serial
2. Sistemul afișează sezoanele
3. Utilizatorul selectează un sezon
4. Sistemul afișează episoadele
5. Utilizatorul marchează episod ca vizionat
6. Sistemul creează înregistrare în WatchHistory
7. Sistemul actualizează UI-ul

**Postcondiție:** Episodul apare ca vizionat în istoric

---

### UC-3: Utilizator Adaugă Review la Serial

**Actor:** Utilizator  
**Precondiție:** Utilizatorul și serialul există în sistem  
**Flow Principal:**
1. Utilizatorul navighează la un serial
2. Sistemul afișează detaliile serialului
3. Utilizatorul introduce rating (1-10) și comentariu
4. Utilizatorul apasă "Trimite Review"
5. Sistemul validează datele
6. Sistemul creează review cu timestamp
7. Sistemul afișează confirmare

**Postcondiție:** Review-ul apare în lista de review-uri

---

### UC-4: Administrator Adaugă Serial Complet

**Actor:** Administrator  
**Precondiție:** Utilizator admin autentificat  
**Flow Principal:**
1. Admin accesează Swagger UI
2. Admin adaugă serial via POST /api/series
3. Sistemul returnează Series ID
4. Admin adaugă sezoane via POST /api/seasons cu Series ID
5. Sistemul returnează Season IDs
6. Admin adaugă episoade via POST /api/episodes cu Season IDs
7. Sistemul confirmă toate creările

**Postcondiție:** Serial complet cu sezoane și episoade disponibil în sistem

---

## 9. Scenarii de Test

### Test Scenario 1: CRUD Complet pentru Movies
1. POST /api/movies - Creare film → Expect 201 Created
2. GET /api/movies - Listare filme → Expect 200 OK, filmul apare
3. GET /api/movies/{id} - Detalii film → Expect 200 OK
4. PUT /api/movies/{id} - Actualizare film → Expect 200 OK
5. DELETE /api/movies/{id} - Ștergere film → Expect 204 No Content
6. GET /api/movies/{id} - Verificare ștergere → Expect 404 Not Found

### Test Scenario 2: Ierarhie Series → Seasons → Episodes
1. POST /api/series - Creare serial
2. POST /api/seasons cu seriesId - Creare sezon
3. POST /api/episodes cu seasonId - Creare episod
4. GET /api/series/{id} - Verificare serial
5. GET /api/seasons (filtrat) - Verificare sezoane pentru serial
6. GET /api/episodes (filtrat) - Verificare episoade pentru sezon
7. DELETE /api/series/{id} - Ștergere serial
8. Verificare cascade delete pentru sezoane și episoade

### Test Scenario 3: Review Workflow
1. POST /api/users - Creare utilizator
2. POST /api/movies - Creare film
3. POST /api/reviews - Adăugare review cu userId și movieId
4. Expect rating între 1-10
5. GET /api/reviews - Verificare review apare
6. PUT /api/reviews/{id} - Actualizare rating
7. DELETE /api/reviews/{id} - Ștergere review

---

## 10. Criterii de Acceptare

### Criteriu 1: Toate Endpoint-urile Funcționează
- Toate cele 40+ endpoint-uri returnează status codes corecte
- Validările funcționează corect
- Relațiile între entități sunt respectate

### Criteriu 2: Interfața Web Este Funcțională
- Toate tab-urile se încarcă și afișează date
- Navigarea ierarhică funcționează pentru seriale
- Statisticile dashboard sunt corecte

### Criteriu 3: Docker Deployment Funcționează
- docker-compose up pornește toate containerele
- API-ul se conectează la PostgreSQL
- Datele persistă între restart-uri

### Criteriu 4: Documentația Este Completă
- README.md cu instrucțiuni clare
- SRS și SDD documente complete
- Swagger UI cu toate endpoint-urile documentate

---

## 11. Glosar

**Watchlist**: Listă de filme/seriale pe care utilizatorul dorește să le vizioneze în viitor

**Watch History**: Istoric al filmelor/episoadelor vizionate de utilizator

**Review**: Evaluare (rating + comentariu) oferită de utilizator pentru un film/serial

**Episode**: Unitate de conținut dintr-un sezon al unui serial

**Season**: Colecție de episoade care formează un sezon al unui serial

**Series**: Serial TV organizat pe sezoane și episoade

**DTO**: Data Transfer Object - Obiect folosit pentru transfer de date între layere

**Entity**: Obiect de business care reprezintă o entitate din domeniu

**REST API**: API web care folosește principiile REST (Representational State Transfer)

**Cascade Delete**: Ștergere automată a entităților dependente când entitatea părinte este ștearsă

---

**Document Control:**
- **Autor:** Sofron Alexandru, Necula Valentin, Margarit Pertrut
- **Revizie:** 1.0
- **Data:** 15 Ianuarie 2026
- **Status:** Final
