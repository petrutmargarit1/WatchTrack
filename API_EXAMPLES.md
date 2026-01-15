# WatchTrack API - Exemple de Utilizare

Acest document conține exemple practice de utilizare a API-ului WatchTrack.

## Базa URL

- **Local**: `http://localhost:5000/api`
- **Docker**: `http://localhost:8080/api`

## Exemple de Request-uri

### 1. Creare Utilizator

```bash
POST /api/users
Content-Type: application/json

{
  "username": "john_doe",
  "email": "john@example.com"
}
```

**Răspuns:**
```json
{
  "id": 1,
  "username": "john_doe",
  "email": "john@example.com",
  "createdAt": "2026-01-15T12:00:00Z"
}
```

### 2. Adăugare Film

```bash
POST /api/movies
Content-Type: application/json

{
  "title": "Inception",
  "description": "A thief who steals corporate secrets through dream-sharing technology",
  "releaseYear": 2010,
  "genre": "Sci-Fi",
  "durationMinutes": 148,
  "posterUrl": "https://example.com/inception.jpg"
}
```

### 3. Creare Serial

```bash
POST /api/series
Content-Type: application/json

{
  "title": "Breaking Bad",
  "description": "A high school chemistry teacher turned meth producer",
  "releaseYear": 2008,
  "genre": "Crime Drama",
  "posterUrl": "https://example.com/breaking-bad.jpg"
}
```

### 4. Adăugare Sezon

```bash
POST /api/seasons
Content-Type: application/json

{
  "seasonNumber": 1,
  "title": "Season 1",
  "releaseYear": 2008,
  "seriesId": 1
}
```

### 5. Adăugare Episod

```bash
POST /api/episodes
Content-Type: application/json

{
  "episodeNumber": 1,
  "title": "Pilot",
  "description": "Walter White's life changes when he is diagnosed with cancer",
  "durationMinutes": 58,
  "airDate": "2008-01-20",
  "seasonId": 1
}
```

### 6. Adăugare Review

```bash
POST /api/reviews
Content-Type: application/json

{
  "rating": 9,
  "comment": "Amazing movie! Highly recommended!",
  "userId": 1,
  "movieId": 1
}
```

### 7. Marcare Film ca Vizionat

```bash
POST /api/watchhistory
Content-Type: application/json

{
  "completed": true,
  "userId": 1,
  "movieId": 1
}
```

### 8. Adăugare în Watchlist

```bash
POST /api/watchlist
Content-Type: application/json

{
  "userId": 1,
  "seriesId": 1
}
```

### 9. Obținere Istoric Utilizator

```bash
GET /api/watchhistory/user/1
```

**Răspuns:**
```json
[
  {
    "id": 1,
    "watchedAt": "2026-01-15T14:30:00Z",
    "completed": true,
    "userId": 1,
    "movieId": 1,
    "episodeId": null
  }
]
```

### 10. Obținere Watchlist Utilizator

```bash
GET /api/watchlist/user/1
```

### 11. Obținere Sezoane pentru Serial

```bash
GET /api/seasons/series/1
```

### 12. Obținere Episoade pentru Sezon

```bash
GET /api/episodes/season/1
```

### 13. Actualizare Film

```bash
PUT /api/movies/1
Content-Type: application/json

{
  "title": "Inception (Director's Cut)",
  "releaseYear": 2010
}
```

### 14. Ștergere Review

```bash
DELETE /api/reviews/1
```

## Testare cu cURL

### Creare utilizator
```bash
curl -X POST http://localhost:8080/api/users \
  -H "Content-Type: application/json" \
  -d '{"username":"test_user","email":"test@example.com"}'
```

### Obținere lista de filme
```bash
curl http://localhost:8080/api/movies
```

### Adăugare film
```bash
curl -X POST http://localhost:8080/api/movies \
  -H "Content-Type: application/json" \
  -d '{
    "title":"The Matrix",
    "description":"A computer hacker learns about the true nature of reality",
    "releaseYear":1999,
    "genre":"Sci-Fi",
    "durationMinutes":136
  }'
```

## Testare cu Postman

1. Deschide Postman
2. Creează un request nou
3. Setează metoda (GET, POST, PUT, DELETE)
4. Introdu URL-ul (ex: `http://localhost:8080/api/movies`)
5. Pentru POST/PUT, adaugă body JSON în tab-ul "Body" → "raw" → "JSON"
6. Apasă "Send"

## Coduri de Status HTTP

- `200 OK` - Request reușit (GET, PUT)
- `201 Created` - Resursă creată cu succes (POST)
- `204 No Content` - Ștergere reușită (DELETE)
- `404 Not Found` - Resursa nu există
- `400 Bad Request` - Date invalide în request
- `500 Internal Server Error` - Eroare server

## Note

- Toate endpoint-urile returnează JSON
- ID-urile sunt generate automat de bază de date
- Câmpurile nullable pot fi omise în request-uri
- Pentru review-uri, trebuie specificat fie `movieId`, fie `seriesId`
- Pentru watchHistory, trebuie specificat fie `movieId`, fie `episodeId`
- Pentru watchlist, trebuie specificat fie `movieId`, fie `seriesId`
