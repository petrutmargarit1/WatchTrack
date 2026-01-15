# WatchTrack - Ghid de Deployment și Testare

## 📦 Deployment

### Opțiune 1: Rulare Locală (Development)

#### Pași:

1. **Verificare .NET SDK**
```bash
dotnet --version
# Trebuie să fie >= 8.0
```

2. **Restaurare pachete NuGet**
```bash
cd /home/alex/Desktop/WatchTrack
dotnet restore
```

3. **Build aplicație**
```bash
dotnet build
```

4. **Rulare aplicație**
```bash
dotnet run
```

5. **Verificare funcționare**
   - API: `http://localhost:5000/api/users`
   - Swagger: `http://localhost:5000/swagger`

### Opțiune 2: Deployment Docker (Production-ready)

#### Pași:

1. **Verificare Docker**
```bash
docker --version
docker-compose --version
```

2. **Build și start containere**
```bash
cd /home/alex/Desktop/WatchTrack
docker-compose up --build
```

3. **Verificare logs**
```bash
# În terminal separat
docker-compose logs -f api
docker-compose logs -f db
```

4. **Verificare funcționare**
   - API: `http://localhost:8080/api/users`
   - Swagger: `http://localhost:8080/swagger`
   - PostgreSQL: `localhost:5432`

5. **Stop aplicație**
```bash
docker-compose down
```

6. **Stop și ștergere date**
```bash
docker-compose down -v
```

## 🧪 Testare Aplicație

### Test 1: Verificare Health

```bash
# Local
curl http://localhost:5000/api/users

# Docker
curl http://localhost:8080/api/users
```

**Răspuns așteptat:** `[]` (listă goală)

### Test 2: Creare Utilizator

```bash
curl -X POST http://localhost:8080/api/users \
  -H "Content-Type: application/json" \
  -d '{"username":"test_user","email":"test@test.com"}'
```

**Răspuns așteptat:**
```json
{
  "id": 1,
  "username": "test_user",
  "email": "test@test.com",
  "createdAt": "2026-01-15T..."
}
```

### Test 3: Verificare Utilizator Creat

```bash
curl http://localhost:8080/api/users
```

**Răspuns așteptat:** Lista cu utilizatorul creat

### Test 4: Adăugare Film

```bash
curl -X POST http://localhost:8080/api/movies \
  -H "Content-Type: application/json" \
  -d '{
    "title": "The Matrix",
    "description": "A computer hacker learns the truth",
    "releaseYear": 1999,
    "genre": "Sci-Fi",
    "durationMinutes": 136
  }'
```

### Test 5: Creare Review

```bash
curl -X POST http://localhost:8080/api/reviews \
  -H "Content-Type: application/json" \
  -d '{
    "rating": 10,
    "comment": "Best movie ever!",
    "userId": 1,
    "movieId": 1
  }'
```

### Test 6: Adăugare în Watchlist

```bash
curl -X POST http://localhost:8080/api/watchlist \
  -H "Content-Type: application/json" \
  -d '{
    "userId": 1,
    "movieId": 1
  }'
```

### Test 7: Marcare ca Vizionat

```bash
curl -X POST http://localhost:8080/api/watchhistory \
  -H "Content-Type: application/json" \
  -d '{
    "completed": true,
    "userId": 1,
    "movieId": 1
  }'
```

## 🔍 Verificare Bază de Date

### SQLite (Local)

```bash
# Verificare fișier
ls -lh watchtrack.db

# Conectare la baza de date
sqlite3 watchtrack.db

# În sqlite shell:
.tables
SELECT * FROM Users;
SELECT * FROM Movies;
.exit
```

### PostgreSQL (Docker)

```bash
# Conectare la container
docker exec -it watchtrack-db psql -U watchtrack_user -d watchtrack

# În PostgreSQL shell:
\dt                    -- Lista tabele
SELECT * FROM "Users";
SELECT * FROM "Movies";
\q                     -- Exit
```

## 📊 Testare Complete - Script

Salvează următorul script ca `test_api.sh`:

```bash
#!/bin/bash

API_URL="http://localhost:8080/api"

echo "=== Test 1: Creare utilizator ==="
curl -X POST $API_URL/users \
  -H "Content-Type: application/json" \
  -d '{"username":"john_doe","email":"john@example.com"}'
echo -e "\n"

echo "=== Test 2: Lista utilizatori ==="
curl $API_URL/users
echo -e "\n"

echo "=== Test 3: Adăugare film ==="
curl -X POST $API_URL/movies \
  -H "Content-Type: application/json" \
  -d '{"title":"Inception","releaseYear":2010,"genre":"Sci-Fi","durationMinutes":148}'
echo -e "\n"

echo "=== Test 4: Lista filme ==="
curl $API_URL/movies
echo -e "\n"

echo "=== Test 5: Creare serial ==="
curl -X POST $API_URL/series \
  -H "Content-Type: application/json" \
  -d '{"title":"Breaking Bad","releaseYear":2008,"genre":"Drama"}'
echo -e "\n"

echo "=== Test 6: Adăugare review ==="
curl -X POST $API_URL/reviews \
  -H "Content-Type: application/json" \
  -d '{"rating":9,"comment":"Great!","userId":1,"movieId":1}'
echo -e "\n"

echo "=== Toate testele finalizate! ==="
```

Rulare:
```bash
chmod +x test_api.sh
./test_api.sh
```

## 🐛 Troubleshooting

### Problema: Port deja în uz

**Eroare:** `Address already in use`

**Soluție:**
```bash
# Verificare procese pe port
lsof -i :8080
lsof -i :5432

# Stop docker-compose existent
docker-compose down

# Sau schimbă portul în docker-compose.yml
```

### Problema: Container nu pornește

**Soluție:**
```bash
# Verificare logs
docker-compose logs api
docker-compose logs db

# Rebuild imagine
docker-compose build --no-cache
docker-compose up
```

### Problema: Eroare conectare bază de date

**Soluție:**
```bash
# Verificare că PostgreSQL este pornit
docker ps

# Verificare health check
docker inspect watchtrack-db | grep Health

# Restart containere
docker-compose restart
```

## 📝 Checklist Finalizare Proiect

- [x] REST API implementat în C#
- [x] Operații CRUD pentru toate entitățile
- [x] Servicii cu logica aplicației
- [x] Entități cu relații în baza de date
- [x] Data Transfer Objects (DTOs)
- [x] Dockerfile funcțional
- [x] docker-compose.yml configurat
- [x] README complet
- [x] Exemple API documentate
- [x] .gitignore configurat

## 🎓 Documentație Suplimentară

Pentru documentație completă a API-ului, accesează Swagger UI:
- Local: http://localhost:5000/swagger
- Docker: http://localhost:8080/swagger

## ⚠️ Note Importante

1. **Nu commitați** fișierele `.db` în Git
2. **Nu expuneți** parolele în producție (folosiți variabile de mediu)
3. **Testați** API-ul înainte de prezentare
4. **Verificați** că toate endpoint-urile funcționează
5. **Documentați** orice modificări adiționale în README
