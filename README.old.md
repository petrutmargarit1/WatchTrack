# WatchTrack

WatchTrack este o aplicație web care permite utilizatorilor să urmărească filmele și serialele pe care le vizionează. Utilizatorii pot adăuga titluri în watchlist, pot bifa episoade ca vizionate și pot oferi rating-uri sau review-uri. Pentru seriale, aplicația oferă structură pe sezoane și episoade, împreună cu progresul total vizionat.

Platforma expune un REST API care gestionează filme, seriale, episoade, review-uri, istoricul vizionărilor și watchlist-ul. Datele sunt stocate într-o bază de date relațională, iar logica este organizată pe servicii și DTO-uri. Proiectul este containerizat și rulat cu Docker Compose alături de un server PostgreSQL.

## 🎯 Scopul Proiectului

Scopul WatchTrack este de a oferi o modalitate simplă și centralizată pentru gestionarea și urmărirea conținutului media.

## 📋 Cerințe Proiect IS2

Acest proiect îndeplinește toate cerințele pentru proiectul IS2:

### Arhitectură (Cerinţe Arhitecturale)
- ✅ REST API implementat în C# (.NET 8.0)
- ✅ Bază de date locală (SQLite) și PostgreSQL pentru Docker
- ✅ Arhitectură pe layere: Controllers, Services, Models, DTOs, Data

### Funcționalități (Cerinţe Funcţionare)
- ✅ **1p** - Controllere pentru toate entitățile cu operații CRUD complete
- ✅ **2p** - Servicii care implementează logica aplicației
- ✅ **2p** - Tabele/entități în baza de date cu relații între ele
- ✅ **2p** - Data Transfer Objects (DTOs) pentru toate entitățile
- ✅ **1p** - Deployment în Docker cu docker-compose

## 🏗️ Arhitectura Aplicației

```
WatchTrack/
├── Controllers/          # REST API endpoints
│   ├── UsersController.cs
│   ├── MoviesController.cs
│   ├── SeriesController.cs
│   └── ...
├── Services/            # Business logic layer
│   ├── UserService.cs
│   ├── MovieService.cs
│   └── ...
├── Models/              # Database entities
│   ├── User.cs
│   ├── Movie.cs
│   ├── Series.cs
│   └── ...
├── DTOs/                # Data Transfer Objects
│   ├── UserDto.cs
│   ├── MovieDto.cs
│   └── ...
├── Data/                # Database context
│   └── WatchTrackDbContext.cs
├── Program.cs           # Application entry point
├── Dockerfile           # Docker configuration
└── docker-compose.yml   # Multi-container setup
```

## 📊 Schema Bazei de Date

### Entități și Relații

1. **User** (Utilizatori)
   - Id, Username, Email, CreatedAt
   - Relații: Reviews, WatchHistories, Watchlists

2. **Movie** (Filme)
   - Id, Title, Description, ReleaseYear, Genre, DurationMinutes, PosterUrl
   - Relații: Reviews, WatchHistories, Watchlists

3. **Series** (Seriale)
   - Id, Title, Description, ReleaseYear, Genre, PosterUrl
   - Relații: Seasons, Reviews, Watchlists

4. **Season** (Sezoane)
   - Id, SeasonNumber, Title, ReleaseYear, SeriesId
   - Relații: Series (parent), Episodes (children)

5. **Episode** (Episoade)
   - Id, EpisodeNumber, Title, Description, DurationMinutes, AirDate, SeasonId
   - Relații: Season (parent), WatchHistories

6. **Review** (Review-uri)
   - Id, Rating, Comment, UserId, MovieId/SeriesId
   - Relații: User, Movie sau Series

7. **WatchHistory** (Istoric vizionări)
   - Id, WatchedAt, Completed, UserId, MovieId/EpisodeId
   - Relații: User, Movie sau Episode

8. **Watchlist** (Lista de urmărit)
   - Id, AddedAt, UserId, MovieId/SeriesId
   - Relații: User, Movie sau Series

## 🚀 Instalare și Rulare

### Prerequisite
- .NET 8.0 SDK
- Docker și Docker Compose (pentru containerizare)

### Varianta 1: Rulare Locală (cu SQLite)

```bash
# Restaurare dependențe
dotnet restore

# Rulare aplicație
dotnet run
```

Aplicația va rula pe `http://localhost:5000` (sau portul specificat).
Baza de date SQLite va fi creată automat în fișierul `watchtrack.db`.

### Varianta 2: Rulare cu Docker Compose (cu PostgreSQL)

```bash
# Build și start containere
docker-compose up --build

# Sau în background
docker-compose up -d --build

# Stop containere
docker-compose down

# Stop și ștergere volume-uri
docker-compose down -v
```

Aplicația va rula pe `http://localhost:8080`.
PostgreSQL va rula pe `localhost:5432`.

## 📡 API Endpoints

### Users
- `GET /api/users` - Lista tuturor utilizatorilor
- `GET /api/users/{id}` - Detalii utilizator
- `POST /api/users` - Creare utilizator nou
- `PUT /api/users/{id}` - Actualizare utilizator
- `DELETE /api/users/{id}` - Ștergere utilizator

### Movies
- `GET /api/movies` - Lista tuturor filmelor
- `GET /api/movies/{id}` - Detalii film
- `POST /api/movies` - Adăugare film nou
- `PUT /api/movies/{id}` - Actualizare film
- `DELETE /api/movies/{id}` - Ștergere film

### Series
- `GET /api/series` - Lista tuturor serialelor
- `GET /api/series/{id}` - Detalii serial
- `POST /api/series` - Adăugare serial nou
- `PUT /api/series/{id}` - Actualizare serial
- `DELETE /api/series/{id}` - Ștergere serial

### Seasons
- `GET /api/seasons` - Lista tuturor sezoanelor
- `GET /api/seasons/{id}` - Detalii sezon
- `GET /api/seasons/series/{seriesId}` - Sezoane pentru un serial
- `POST /api/seasons` - Adăugare sezon nou
- `PUT /api/seasons/{id}` - Actualizare sezon
- `DELETE /api/seasons/{id}` - Ștergere sezon

### Episodes
- `GET /api/episodes` - Lista tuturor episoadelor
- `GET /api/episodes/{id}` - Detalii episod
- `GET /api/episodes/season/{seasonId}` - Episoade pentru un sezon
- `POST /api/episodes` - Adăugare episod nou
- `PUT /api/episodes/{id}` - Actualizare episod
- `DELETE /api/episodes/{id}` - Ștergere episod

### Reviews
- `GET /api/reviews` - Lista tuturor review-urilor
- `GET /api/reviews/{id}` - Detalii review
- `POST /api/reviews` - Adăugare review nou
- `PUT /api/reviews/{id}` - Actualizare review
- `DELETE /api/reviews/{id}` - Ștergere review

### WatchHistory
- `GET /api/watchhistory` - Lista întregului istoric
- `GET /api/watchhistory/{id}` - Detalii intrare istoric
- `GET /api/watchhistory/user/{userId}` - Istoric pentru un utilizator
- `POST /api/watchhistory` - Adăugare în istoric
- `PUT /api/watchhistory/{id}` - Actualizare istoric
- `DELETE /api/watchhistory/{id}` - Ștergere din istoric

### Watchlist
- `GET /api/watchlist` - Lista tuturor watchlist-urilor
- `GET /api/watchlist/{id}` - Detalii intrare watchlist
- `GET /api/watchlist/user/{userId}` - Watchlist pentru un utilizator
- `POST /api/watchlist` - Adăugare în watchlist
- `DELETE /api/watchlist/{id}` - Ștergere din watchlist

## 📚 Swagger Documentation

Aplicația include Swagger UI pentru testarea API-ului:
- Local: `http://localhost:5000/swagger`
- Docker: `http://localhost:8080/swagger`

## 🛠️ Tehnologii Utilizate

- **.NET 8.0** - Framework principal
- **ASP.NET Core Web API** - REST API
- **Entity Framework Core 8.0** - ORM pentru baza de date
- **SQLite** - Bază de date locală
- **PostgreSQL** - Bază de date pentru Docker
- **Swagger/OpenAPI** - Documentație API
- **Docker & Docker Compose** - Containerizare

## 👥 Echipa

- Proiect dezvoltat pentru cursul Inginerie Software II
- Deadline: 16.01.2026

## 📄 Licență

Acest proiect este dezvoltat în scop educațional.
