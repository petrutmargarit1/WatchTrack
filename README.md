# 🎬 WatchTrack Pro

Aplicație web profesională pentru tracking filme și seriale - Proiect IS2 (Software Engineering II)

---

## ⚡ Quick Start (Recomandat - Docker)

Cea mai rapidă metodă de a rula aplicația:

```bash
# 1. Pornește Docker containers
cd /home/alex/Desktop/WatchTrack
docker-compose up --build -d

# 2. Așteaptă ~15 secunde, apoi pornește frontend-ul
python3 -m http.server 3000

# 3. Deschide browser la:
# http://localhost:3000 (Interfață WEB)
# http://localhost:8080/swagger/index.html (Swagger)
```

✅ **Gata! Aplicația rulează!**

---

## 🚀 Cum pornesc aplicația? (Detaliat)

Ai **2 moduri** de a rula aplicația:

| Caracteristică | Local (SQLite) | Docker (PostgreSQL) |
|-----------------|----------------|---------------------|
| Bază de date | SQLite (watchtrack.db) | PostgreSQL (container) |
| Port API | 5000 | 8080 |
| Complexitate | Simplu | Mediu |
| Performanță | Bună | Excelență |
| Producție | Nu | Da |
| Recomandat pentru | Dezvoltare rapidă | Demo/Prezentare |

---

## 📍 OPȚIUNEA 1: Rulare Locală (SQLite)

### Pasul 1: Pornește Backend-ul API (.NET)

Deschide un terminal și rulează:

```bash
cd /home/alex/Desktop/WatchTrack
dotnet run
```

✅ API-ul va rula pe: **http://localhost:5000**

**NU închide acest terminal!** Lasă-l deschis în timpul rulării.

---

### Pasul 2: Configurează Frontend-ul pentru local

Asigură-te că în `index.html` ai:
```javascript
const API_URL = 'http://localhost:5000/api';
```

---

### Pasul 3: Pornește Frontend-ul WEB

Deschide un **NOU terminal** (al doilea) și rulează:

```bash
cd /home/alex/Desktop/WatchTrack
python3 -m http.server 3000
```

✅ Frontend-ul va rula pe: **http://localhost:3000**

**NU închide acest terminal!** Lasă-l deschis în timpul rulării.

---

### 🌐 Accesează aplicația (Local):

**Interfața WEB:**
```
http://localhost:3000
```

**Swagger UI:**
```
http://localhost:5000/swagger/index.html
```

---

### 🛑 Oprire aplicație locală:

```bash
# Oprește API-ul .NET
pkill -f "dotnet run"

# Oprește serverul WEB Python
pkill -f "python3 -m http.server 3000"
```

---

## 🐳 OPȚIUNEA 2: Rulare cu Docker (PostgreSQL) - RECOMANDAT

### Pasul 1: Pornește Docker Containers

```bash
cd /home/alex/Desktop/WatchTrack
docker-compose up --build -d
```

✅ Acest comanda va porni:
- **PostgreSQL** pe port 5432
- **API .NET** pe port 8080

Așteptați ~10-15 secunde pentru ca containerele să pornească complet.

---

### Pasul 2: Verifică statusul containerelor

```bash
docker-compose ps
```

Trebui să vezi ambele containere `Up` și `healthy`.

---

### Pasul 3: Configurează Frontend-ul pentru Docker

Asigură-te că în `index.html` ai:
```javascript
const API_URL = 'http://localhost:8080/api';
```

---

### Pasul 4: Pornește Frontend-ul WEB

Deschide un terminal și rulează:

```bash
cd /home/alex/Desktop/WatchTrack
python3 -m http.server 3000
```

✅ Frontend-ul va rula pe: **http://localhost:3000**

---

### 🌐 Accesează aplicația (Docker):

**Interfața WEB:**
```
http://localhost:3000
```

**Swagger UI:**
```
http://localhost:8080/swagger/index.html
```

**API Direct:**
```
http://localhost:8080/api/users
http://localhost:8080/api/movies
http://localhost:8080/api/series
```

---

### 🛑 Oprire aplicație Docker:

```bash
# Oprește containerele
docker-compose down

# Oprește containerele și șterge datele (reset complet)
docker-compose down -v

# Oprește serverul WEB Python
pkill -f "python3 -m http.server 3000"
```

---

### 📊 Comenzi utile Docker:

```bash
# Vezi log-uri API
docker logs watchtrack-api

# Vezi log-uri live
docker logs -f watchtrack-api

# Accesează PostgreSQL
docker exec -it watchtrack-db psql -U watchtrack_user -d watchtrack

# Vezi tabelele din baza de date
docker exec watchtrack-db psql -U watchtrack_user -d watchtrack -c "\dt"

# Restart containere
docker-compose restart
```

---

## 📋 Structura Proiectului

```
WatchTrack/
├── Controllers/          # 8 controllere REST API
├── Services/            # 8 servicii cu interfețe
├── DTOs/                # 24 DTOs (Read, Create, Update)
├── Entities/            # 8 entități cu relații
├── Data/                # DbContext și migrații
├── index.html           # Interfață WEB profesională
├── Program.cs           # Configurare aplicație
├── appsettings.json     # Configurare SQLite (local)
├── docker-compose.yml   # Deployment Docker
├── Dockerfile           # Container API
├── watchtrack.db        # Bază de date SQLite
└── README.md            # Acest fișier
```

---

## 🔄 Schimbarea între Local și Docker

### Trecare de la Local la Docker:

1. Oprește aplicația locală (`Ctrl+C` în ambele terminale)
2. Editează `index.html` și schimbă:
   ```javascript
   const API_URL = 'http://localhost:8080/api';
   ```
3. Pornește Docker: `docker-compose up --build -d`
4. Pornește frontend: `python3 -m http.server 3000`

### Trecare de la Docker la Local:

1. Oprește Docker: `docker-compose down`
2. Editează `index.html` și schimbă:
   ```javascript
   const API_URL = 'http://localhost:5000/api';
   ```
3. Pornește API local: `dotnet run`
4. Pornește frontend: `python3 -m http.server 3000`

---

## 🎯 Funcționalități Implementate

### ✅ Backend (.NET 8.0 REST API)
- ✅ 8 Controllers (Users, Movies, Series, Seasons, Episodes, Reviews, WatchHistory, Watchlist)
- ✅ 8 Services cu interfețe
- ✅ 8 Entities cu relații (One-to-Many, Foreign Keys)
- ✅ 24 DTOs (ReadDto, CreateDto, UpdateDto)
- ✅ Entity Framework Core + SQLite/PostgreSQL
- ✅ Swagger UI
- ✅ CORS enabled
- ✅ Docker deployment

### ✅ Frontend (Web Interface)
- ✅ Design modern glassmorphism
- ✅ Navigare prin tab-uri
- ✅ Dashboard cu statistici în timp real
- ✅ Browsing ierarhic: Seriale → Sezoane → Episoade
- ✅ Animații și hover effects
- ✅ Responsive design
- ✅ Conectare live la API

---

## 📊 API Endpoints

### Users
- `GET    /api/users` - Lista utilizatori
- `GET    /api/users/{id}` - Detalii utilizator
- `POST   /api/users` - Creare utilizator
- `PUT    /api/users/{id}` - Actualizare utilizator
- `DELETE /api/users/{id}` - Ștergere utilizator

### Movies
- `GET    /api/movies` - Lista filme
- `GET    /api/movies/{id}` - Detalii film
- `POST   /api/movies` - Adăugare film
- `PUT    /api/movies/{id}` - Actualizare film
- `DELETE /api/movies/{id}` - Ștergere film

### Series
- `GET    /api/series` - Lista seriale
- `GET    /api/series/{id}` - Detalii serial
- `POST   /api/series` - Adăugare serial
- `PUT    /api/series/{id}` - Actualizare serial
- `DELETE /api/series/{id}` - Ștergere serial

### Seasons
- `GET    /api/seasons` - Lista sezoane
- `GET    /api/seasons/{id}` - Detalii sezon
- `POST   /api/seasons` - Adăugare sezon
- `PUT    /api/seasons/{id}` - Actualizare sezon
- `DELETE /api/seasons/{id}` - Ștergere sezon

### Episodes
- `GET    /api/episodes` - Lista episoade
- `GET    /api/episodes/{id}` - Detalii episod
- `POST   /api/episodes` - Adăugare episod
- `PUT    /api/episodes/{id}` - Actualizare episod
- `DELETE /api/episodes/{id}` - Ștergere episod

### Reviews
- `GET    /api/reviews` - Lista review-uri
- `GET    /api/reviews/{id}` - Detalii review
- `POST   /api/reviews` - Adăugare review
- `PUT    /api/reviews/{id}` - Actualizare review
- `DELETE /api/reviews/{id}` - Ștergere review

### Watch History
- `GET    /api/watchhistory` - Istoric vizionări
- `GET    /api/watchhistory/{id}` - Detalii istoric
- `POST   /api/watchhistory` - Marcare ca vizionat
- `PUT    /api/watchhistory/{id}` - Actualizare istoric
- `DELETE /api/watchhistory/{id}` - Ștergere istoric

### Watchlist
- `GET    /api/watchlist` - Lista watchlist
- `GET    /api/watchlist/{id}` - Detalii watchlist
- `POST   /api/watchlist` - Adăugare în watchlist
- `PUT    /api/watchlist/{id}` - Actualizare watchlist
- `DELETE /api/watchlist/{id}` - Ștergere din watchlist

---

## 💡 Tips & Tricks

### Adăugare date de test prin Swagger:

1. Deschide **http://localhost:5000/swagger/index.html**
2. Click pe `POST /api/users` → "Try it out"
3. Introdu JSON:
```json
{
  "username": "john_doe",
  "email": "john@example.com"
}
```
4. Click "Execute"
5. Repetă pentru movies, series, etc.

### Verificare date în interfața WEB:

După adăugarea datelor prin Swagger, deschide **http://localhost:3000** și refresh pagina (F5) pentru a vedea datele noi.

---

## 🎓 Cerințe Proiect IS2

✅ **8 puncte total:**
- ✅ 1p - Controllers (8 controllere)
- ✅ 2p - Services (8 servicii cu interfețe)
- ✅ 2p - Entities (8 entități cu relații)
- ✅ 2p - DTOs (24 DTOs)
- ✅ 1p - Docker deployment

---

## 👨‍💻 Tehnologii Folosite

- **Backend:** .NET 8.0, ASP.NET Core Web API, Entity Framework Core
- **Database:** SQLite (local), PostgreSQL (Docker)
- **Frontend:** HTML5, CSS3, JavaScript (Vanilla)
- **API Documentation:** Swagger/OpenAPI
- **Deployment:** Docker, docker-compose

---

## 📅 Data Limită

**16 ianuarie 2026** ✅

---

## 📧 Contact

Pentru întrebări despre proiect, contactează echipa de dezvoltare.

---

**Enjoy tracking! 🎬📺⭐**
