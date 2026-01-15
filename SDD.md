# Software Design Document (SDD)
# WatchTrack Pro

**Version:** 1.0  
**Date:** 15 Ianuarie 2026  
**Project:** IS2 (Software Engineering II)

---

## 1. Introducere

### 1.1 Scopul Documentului
Acest document descrie arhitectura, designul și implementarea tehnică a aplicației WatchTrack Pro. Documentul oferă o vedere detaliată asupra structurii sistemului, componentelor software, modelului de date și deciziilor de design.

### 1.2 Scop și Context
WatchTrack Pro este o aplicație web RESTful pentru tracking filme și seriale, construită cu .NET 8.0 și PostgreSQL, deployabilă cu Docker.

### 1.3 Definiții și Acronime
- **REST**: Representational State Transfer
- **API**: Application Programming Interface
- **DTO**: Data Transfer Object
- **ORM**: Object-Relational Mapping
- **EF Core**: Entity Framework Core
- **DI**: Dependency Injection
- **CORS**: Cross-Origin Resource Sharing

---

## 2. Arhitectura Sistemului

### 2.1 Vedere Generală

```
┌─────────────────────────────────────────────────────────┐
│                    CLIENT LAYER                         │
│                                                         │
│  ┌──────────────┐      ┌──────────────┐               │
│  │   Browser    │      │   Postman    │               │
│  │  (Frontend)  │      │  (Testing)   │               │
│  └──────────────┘      └──────────────┘               │
└─────────────────────────────────────────────────────────┘
                         │
                    HTTP/HTTPS
                         │
┌─────────────────────────────────────────────────────────┐
│              APPLICATION LAYER (API)                    │
│                                                         │
│  ┌─────────────────────────────────────────────────┐  │
│  │            Controllers (REST Endpoints)          │  │
│  │  UsersController | MoviesController | ...        │  │
│  └─────────────────────────────────────────────────┘  │
│                         │                               │
│  ┌─────────────────────────────────────────────────┐  │
│  │           Services (Business Logic)              │  │
│  │  UserService | MovieService | ...                │  │
│  └─────────────────────────────────────────────────┘  │
│                         │                               │
│  ┌─────────────────────────────────────────────────┐  │
│  │         DbContext (Data Access Layer)            │  │
│  │         Entity Framework Core                    │  │
│  └─────────────────────────────────────────────────┘  │
└─────────────────────────────────────────────────────────┘
                         │
                         │
┌─────────────────────────────────────────────────────────┐
│                   DATA LAYER                            │
│                                                         │
│  ┌──────────────┐      ┌──────────────┐               │
│  │   SQLite     │  or  │  PostgreSQL  │               │
│  │   (Local)    │      │   (Docker)   │               │
│  └──────────────┘      └──────────────┘               │
└─────────────────────────────────────────────────────────┘
```

### 2.2 Pattern-uri de Design Utilizate

#### 2.2.1 Layered Architecture (N-Tier)
- **Presentation Layer**: Controllers (REST API endpoints)
- **Business Logic Layer**: Services (logică de business)
- **Data Access Layer**: DbContext și Entity Framework Core
- **Data Layer**: PostgreSQL/SQLite

#### 2.2.2 Repository Pattern
- Entity Framework Core DbContext acționează ca Repository
- DbSet<T> pentru fiecare entitate

#### 2.2.3 Service Layer Pattern
- Logica de business separată de controllere
- Interfețe pentru servicii (IUserService, IMovieService, etc.)
- Implementare Dependency Injection

#### 2.2.4 Data Transfer Object (DTO) Pattern
- Separare între entități și obiecte de transfer
- DTOs pentru Read, Create, Update operații
- Protecție împotriva over-posting

---

## 3. Arhitectura Componentelor

### 3.1 Structura Proiectului

```
WatchTrack/
│
├── Controllers/              # REST API Controllers
│   ├── UsersController.cs
│   ├── MoviesController.cs
│   ├── SeriesController.cs
│   ├── SeasonsController.cs
│   ├── EpisodesController.cs
│   ├── ReviewsController.cs
│   ├── WatchHistoryController.cs
│   └── WatchlistController.cs
│
├── Services/                 # Business Logic Layer
│   ├── Interfaces/
│   │   ├── IUserService.cs
│   │   ├── IMovieService.cs
│   │   ├── ISeriesService.cs
│   │   ├── ISeasonService.cs
│   │   ├── IEpisodeService.cs
│   │   ├── IReviewService.cs
│   │   ├── IWatchHistoryService.cs
│   │   └── IWatchlistService.cs
│   │
│   └── Implementations/
│       ├── UserService.cs
│       ├── MovieService.cs
│       ├── SeriesService.cs
│       ├── SeasonService.cs
│       ├── EpisodeService.cs
│       ├── ReviewService.cs
│       ├── WatchHistoryService.cs
│       └── WatchlistService.cs
│
├── DTOs/                     # Data Transfer Objects
│   ├── UserDto.cs
│   ├── CreateUserDto.cs
│   ├── UpdateUserDto.cs
│   ├── MovieDto.cs
│   ├── CreateMovieDto.cs
│   ├── UpdateMovieDto.cs
│   └── ... (24 DTOs total)
│
├── Entities/                 # Domain Models
│   ├── User.cs
│   ├── Movie.cs
│   ├── Series.cs
│   ├── Season.cs
│   ├── Episode.cs
│   ├── Review.cs
│   ├── WatchHistory.cs
│   └── Watchlist.cs
│
├── Data/                     # Data Access Layer
│   └── WatchTrackDbContext.cs
│
├── Program.cs                # Application Entry Point
├── appsettings.json          # Configuration (SQLite)
├── appsettings.Docker.json   # Configuration (PostgreSQL)
├── WatchTrack.csproj         # Project File
├── Dockerfile                # Docker Image Definition
├── docker-compose.yml        # Multi-Container Orchestration
├── .dockerignore             # Docker Build Exclusions
└── index.html                # Web Frontend
```

### 3.2 Dependency Injection Configuration

**Program.cs:**
```csharp
// Register DbContext
builder.Services.AddDbContext<WatchTrackDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseNpgsql(connectionString); // PostgreSQL
    // or options.UseSqlite(connectionString); // SQLite
});

// Register Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<ISeriesService, SeriesService>();
builder.Services.AddScoped<ISeasonService, SeasonService>();
builder.Services.AddScoped<IEpisodeService, EpisodeService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IWatchHistoryService, WatchHistoryService>();
builder.Services.AddScoped<IWatchlistService, WatchlistService>();
```

---

## 4. Model de Date (Database Schema)

### 4.1 Entity Relationship Diagram (ERD)

```
┌────────────────┐
│     User       │
│────────────────│
│ Id (PK)        │
│ Username       │◄─────────┐
│ Email          │          │
│ CreatedAt      │          │ 1:N
└────────────────┘          │
                            │
                    ┌───────┴────────┐
                    │                │
         ┌──────────▼─────┐  ┌──────▼─────────┐
         │    Review      │  │  WatchHistory  │
         │────────────────│  │────────────────│
         │ Id (PK)        │  │ Id (PK)        │
         │ Rating         │  │ WatchedAt      │
         │ Comment        │  │ Completed      │
         │ UserId (FK)    │  │ UserId (FK)    │
         │ MovieId (FK)   │  │ MovieId (FK)   │
         │ SeriesId (FK)  │  │ EpisodeId (FK) │
         │ CreatedAt      │  └────────────────┘
         └────────────────┘
                 │
         ┌───────┴───────┐
         │               │
  ┌──────▼──────┐  ┌────▼─────────┐
  │    Movie    │  │    Series    │
  │─────────────│  │──────────────│
  │ Id (PK)     │  │ Id (PK)      │
  │ Title       │  │ Title        │
  │ Description │  │ Description  │
  │ ReleaseYear │  │ ReleaseYear  │
  │ Genre       │  │ Genre        │
  │ Duration    │  │ PosterUrl    │
  │ PosterUrl   │  │ CreatedAt    │
  │ CreatedAt   │  └──────────────┘
  └─────────────┘         │
         │                │ 1:N
         │          ┌─────▼──────┐
         │          │   Season   │
         │          │────────────│
         │          │ Id (PK)    │
         │          │ SeasonNum  │
         │          │ Title      │
         │          │ ReleaseYr  │
         │          │ SeriesId(FK)│
         │          └────────────┘
         │                │ 1:N
         │          ┌─────▼──────┐
         │          │  Episode   │
         │          │────────────│
         │          │ Id (PK)    │
         │          │ EpisodeNum │
         │          │ Title      │
         │          │ Description│
         │          │ Duration   │
         │          │ AirDate    │
         │          │ SeasonId(FK)│
         │          └────────────┘
         │
  ┌──────▼──────────┐
  │   Watchlist     │
  │─────────────────│
  │ Id (PK)         │
  │ AddedAt         │
  │ UserId (FK)     │
  │ MovieId (FK)    │
  │ SeriesId (FK)   │
  └─────────────────┘
```

### 4.2 Entități și Relații

#### 4.2.1 User Entity
```csharp
public class User
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Username { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    // Navigation Properties
    public ICollection<Review> Reviews { get; set; }
    public ICollection<WatchHistory> WatchHistories { get; set; }
    public ICollection<Watchlist> Watchlists { get; set; }
}
```

**Constraints:**
- Username: Unique, Index
- Email: Unique, Index

#### 4.2.2 Movie Entity
```csharp
public class Movie
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(200)]
    public string Title { get; set; }
    
    [MaxLength(1000)]
    public string? Description { get; set; }
    
    public int? ReleaseYear { get; set; }
    
    [MaxLength(100)]
    public string? Genre { get; set; }
    
    public int? DurationMinutes { get; set; }
    
    [MaxLength(500)]
    public string? PosterUrl { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    // Navigation Properties
    public ICollection<Review> Reviews { get; set; }
    public ICollection<WatchHistory> WatchHistories { get; set; }
    public ICollection<Watchlist> Watchlists { get; set; }
}
```

#### 4.2.3 Series Entity
```csharp
public class Series
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(200)]
    public string Title { get; set; }
    
    [MaxLength(1000)]
    public string? Description { get; set; }
    
    public int? ReleaseYear { get; set; }
    
    [MaxLength(100)]
    public string? Genre { get; set; }
    
    [MaxLength(500)]
    public string? PosterUrl { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    // Navigation Properties
    public ICollection<Season> Seasons { get; set; }
    public ICollection<Review> Reviews { get; set; }
    public ICollection<Watchlist> Watchlists { get; set; }
}
```

**Relationship:** One-to-Many with Season (1:N)

#### 4.2.4 Season Entity
```csharp
public class Season
{
    public int Id { get; set; }
    
    [Required]
    public int SeasonNumber { get; set; }
    
    [MaxLength(200)]
    public string? Title { get; set; }
    
    public int? ReleaseYear { get; set; }
    
    // Foreign Key
    [Required]
    public int SeriesId { get; set; }
    
    // Navigation Property
    public Series Series { get; set; }
    public ICollection<Episode> Episodes { get; set; }
}
```

**Relationship:** 
- Many-to-One with Series (N:1)
- One-to-Many with Episode (1:N)

#### 4.2.5 Episode Entity
```csharp
public class Episode
{
    public int Id { get; set; }
    
    [Required]
    public int EpisodeNumber { get; set; }
    
    [Required]
    [MaxLength(200)]
    public string Title { get; set; }
    
    [MaxLength(1000)]
    public string? Description { get; set; }
    
    public int? DurationMinutes { get; set; }
    
    public DateTime? AirDate { get; set; }
    
    // Foreign Key
    [Required]
    public int SeasonId { get; set; }
    
    // Navigation Property
    public Season Season { get; set; }
    public ICollection<WatchHistory> WatchHistories { get; set; }
}
```

**Relationship:** Many-to-One with Season (N:1)

#### 4.2.6 Review Entity
```csharp
public class Review
{
    public int Id { get; set; }
    
    [Required]
    [Range(1, 10)]
    public int Rating { get; set; }
    
    [MaxLength(2000)]
    public string? Comment { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    // Foreign Keys
    [Required]
    public int UserId { get; set; }
    
    public int? MovieId { get; set; }
    public int? SeriesId { get; set; }
    
    // Navigation Properties
    public User User { get; set; }
    public Movie? Movie { get; set; }
    public Series? Series { get; set; }
}
```

**Relationships:**
- Many-to-One with User (N:1)
- Many-to-One with Movie (N:1) - Optional
- Many-to-One with Series (N:1) - Optional

**Business Rule:** MovieId OR SeriesId must be set (not both)

#### 4.2.7 WatchHistory Entity
```csharp
public class WatchHistory
{
    public int Id { get; set; }
    
    public DateTime WatchedAt { get; set; } = DateTime.UtcNow;
    
    [Required]
    public bool Completed { get; set; }
    
    // Foreign Keys
    [Required]
    public int UserId { get; set; }
    
    public int? MovieId { get; set; }
    public int? EpisodeId { get; set; }
    
    // Navigation Properties
    public User User { get; set; }
    public Movie? Movie { get; set; }
    public Episode? Episode { get; set; }
}
```

**Relationships:**
- Many-to-One with User (N:1)
- Many-to-One with Movie (N:1) - Optional
- Many-to-One with Episode (N:1) - Optional

**Business Rule:** MovieId OR EpisodeId must be set (not both)

#### 4.2.8 Watchlist Entity
```csharp
public class Watchlist
{
    public int Id { get; set; }
    
    public DateTime AddedAt { get; set; } = DateTime.UtcNow;
    
    // Foreign Keys
    [Required]
    public int UserId { get; set; }
    
    public int? MovieId { get; set; }
    public int? SeriesId { get; set; }
    
    // Navigation Properties
    public User User { get; set; }
    public Movie? Movie { get; set; }
    public Series? Series { get; set; }
}
```

**Relationships:**
- Many-to-One with User (N:1)
- Many-to-One with Movie (N:1) - Optional
- Many-to-One with Series (N:1) - Optional

**Business Rule:** MovieId OR SeriesId must be set (not both)

### 4.3 Database Configuration (DbContext)

```csharp
public class WatchTrackDbContext : DbContext
{
    public WatchTrackDbContext(DbContextOptions<WatchTrackDbContext> options)
        : base(options) { }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Series> Series { get; set; }
    public DbSet<Season> Seasons { get; set; }
    public DbSet<Episode> Episodes { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<WatchHistory> WatchHistories { get; set; }
    public DbSet<Watchlist> Watchlists { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // User Indexes
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Username)
            .IsUnique();
            
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();
        
        // Series -> Seasons (1:N with Cascade Delete)
        modelBuilder.Entity<Season>()
            .HasOne(s => s.Series)
            .WithMany(se => se.Seasons)
            .HasForeignKey(s => s.SeriesId)
            .OnDelete(DeleteBehavior.Cascade);
        
        // Season -> Episodes (1:N with Cascade Delete)
        modelBuilder.Entity<Episode>()
            .HasOne(e => e.Season)
            .WithMany(s => s.Episodes)
            .HasForeignKey(e => e.SeasonId)
            .OnDelete(DeleteBehavior.Cascade);
        
        // User -> Reviews (1:N with Cascade Delete)
        modelBuilder.Entity<Review>()
            .HasOne(r => r.User)
            .WithMany(u => u.Reviews)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        // Similar configurations for other relationships...
    }
}
```

---

## 5. API Design

### 5.1 REST Endpoint Structure

**Base URL:** `http://localhost:5000/api` (local) or `http://localhost:8080/api` (Docker)

### 5.2 Controller Design Pattern

Toate controllerele urmează aceeași structură:

```csharp
[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    
    public UsersController(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
    {
        var users = await _userService.GetAllUsersAsync();
        return Ok(users);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetUser(int id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        if (user == null) return NotFound();
        return Ok(user);
    }
    
    [HttpPost]
    public async Task<ActionResult<UserDto>> CreateUser(CreateUserDto createDto)
    {
        var user = await _userService.CreateUserAsync(createDto);
        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
    }
    
    [HttpPut("{id}")]
    public async Task<ActionResult<UserDto>> UpdateUser(int id, UpdateUserDto updateDto)
    {
        var user = await _userService.UpdateUserAsync(id, updateDto);
        if (user == null) return NotFound();
        return Ok(user);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var success = await _userService.DeleteUserAsync(id);
        if (!success) return NotFound();
        return NoContent();
    }
}
```

### 5.3 Complete API Endpoints

#### Users Endpoints
- `GET    /api/users` - Lista utilizatori
- `GET    /api/users/{id}` - Detalii utilizator
- `POST   /api/users` - Creare utilizator
- `PUT    /api/users/{id}` - Actualizare utilizator
- `DELETE /api/users/{id}` - Ștergere utilizator

#### Movies Endpoints
- `GET    /api/movies` - Lista filme
- `GET    /api/movies/{id}` - Detalii film
- `POST   /api/movies` - Creare film
- `PUT    /api/movies/{id}` - Actualizare film
- `DELETE /api/movies/{id}` - Ștergere film

#### Series Endpoints
- `GET    /api/series` - Lista seriale
- `GET    /api/series/{id}` - Detalii serial
- `POST   /api/series` - Creare serial
- `PUT    /api/series/{id}` - Actualizare serial
- `DELETE /api/series/{id}` - Ștergere serial

#### Seasons Endpoints
- `GET    /api/seasons` - Lista sezoane
- `GET    /api/seasons/{id}` - Detalii sezon
- `POST   /api/seasons` - Creare sezon
- `PUT    /api/seasons/{id}` - Actualizare sezon
- `DELETE /api/seasons/{id}` - Ștergere sezon

#### Episodes Endpoints
- `GET    /api/episodes` - Lista episoade
- `GET    /api/episodes/{id}` - Detalii episod
- `POST   /api/episodes` - Creare episod
- `PUT    /api/episodes/{id}` - Actualizare episod
- `DELETE /api/episodes/{id}` - Ștergere episod

#### Reviews Endpoints
- `GET    /api/reviews` - Lista review-uri
- `GET    /api/reviews/{id}` - Detalii review
- `POST   /api/reviews` - Creare review
- `PUT    /api/reviews/{id}` - Actualizare review
- `DELETE /api/reviews/{id}` - Ștergere review

#### WatchHistory Endpoints
- `GET    /api/watchhistory` - Lista istoric
- `GET    /api/watchhistory/{id}` - Detalii istoric
- `POST   /api/watchhistory` - Creare înregistrare
- `PUT    /api/watchhistory/{id}` - Actualizare istoric
- `DELETE /api/watchhistory/{id}` - Ștergere din istoric

#### Watchlist Endpoints
- `GET    /api/watchlist` - Lista watchlist
- `GET    /api/watchlist/{id}` - Detalii watchlist
- `POST   /api/watchlist` - Adăugare în watchlist
- `PUT    /api/watchlist/{id}` - Actualizare watchlist
- `DELETE /api/watchlist/{id}` - Ștergere din watchlist

### 5.4 HTTP Status Codes

| Code | Usage |
|------|-------|
| 200 OK | GET, PUT success |
| 201 Created | POST success |
| 204 No Content | DELETE success |
| 400 Bad Request | Validation error |
| 404 Not Found | Resource not found |
| 500 Internal Server Error | Server error |

---

## 6. Service Layer Design

### 6.1 Service Interface Pattern

```csharp
public interface IUserService
{
    Task<IEnumerable<UserDto>> GetAllUsersAsync();
    Task<UserDto?> GetUserByIdAsync(int id);
    Task<UserDto> CreateUserAsync(CreateUserDto createDto);
    Task<UserDto?> UpdateUserAsync(int id, UpdateUserDto updateDto);
    Task<bool> DeleteUserAsync(int id);
}
```

### 6.2 Service Implementation Pattern

```csharp
public class UserService : IUserService
{
    private readonly WatchTrackDbContext _context;
    
    public UserService(WatchTrackDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
    {
        return await _context.Users
            .Select(u => new UserDto
            {
                Id = u.Id,
                Username = u.Username,
                Email = u.Email,
                CreatedAt = u.CreatedAt
            })
            .ToListAsync();
    }
    
    public async Task<UserDto?> GetUserByIdAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return null;
        
        return new UserDto
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            CreatedAt = user.CreatedAt
        };
    }
    
    public async Task<UserDto> CreateUserAsync(CreateUserDto createDto)
    {
        var user = new User
        {
            Username = createDto.Username,
            Email = createDto.Email
        };
        
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        
        return new UserDto
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            CreatedAt = user.CreatedAt
        };
    }
    
    public async Task<UserDto?> UpdateUserAsync(int id, UpdateUserDto updateDto)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return null;
        
        user.Username = updateDto.Username;
        user.Email = updateDto.Email;
        
        await _context.SaveChangesAsync();
        
        return new UserDto
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            CreatedAt = user.CreatedAt
        };
    }
    
    public async Task<bool> DeleteUserAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return false;
        
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return true;
    }
}
```

---

## 7. DTO Design

### 7.1 DTO Naming Convention

- **ReadDto**: Pentru returnat date (GET)
- **CreateDto**: Pentru creare (POST)
- **UpdateDto**: Pentru actualizare (PUT)

### 7.2 Example: User DTOs

```csharp
// Read DTO
public class UserDto
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public DateTime CreatedAt { get; set; }
}

// Create DTO
public class CreateUserDto
{
    [Required]
    [MinLength(3)]
    [MaxLength(100)]
    public string Username { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}

// Update DTO
public class UpdateUserDto
{
    [Required]
    [MinLength(3)]
    [MaxLength(100)]
    public string Username { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}
```

### 7.3 DTO List (24 Total)

1. UserDto, CreateUserDto, UpdateUserDto
2. MovieDto, CreateMovieDto, UpdateMovieDto
3. SeriesDto, CreateSeriesDto, UpdateSeriesDto
4. SeasonDto, CreateSeasonDto, UpdateSeasonDto
5. EpisodeDto, CreateEpisodeDto, UpdateEpisodeDto
6. ReviewDto, CreateReviewDto, UpdateReviewDto
7. WatchHistoryDto, CreateWatchHistoryDto, UpdateWatchHistoryDto
8. WatchlistDto, CreateWatchlistDto, UpdateWatchlistDto

---

## 8. Deployment Architecture

### 8.1 Docker Configuration

#### 8.1.1 Dockerfile (Multi-Stage Build)

```dockerfile
# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY WatchTrack.csproj .
RUN dotnet restore
COPY . .
RUN dotnet publish -c Release -o /app/publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "WatchTrack.dll"]
```

**Benefits:**
- Smaller final image (only runtime dependencies)
- Build cache optimization
- Security (no SDK in production image)

#### 8.1.2 docker-compose.yml

```yaml
services:
  # PostgreSQL Database
  db:
    image: postgres:16-alpine
    container_name: watchtrack-db
    environment:
      POSTGRES_DB: watchtrack
      POSTGRES_USER: watchtrack_user
      POSTGRES_PASSWORD: watchtrack_password
    ports:
      - "5432:5432"
    volumes:
      - postgres_data:/var/lib/postgresql/data
    healthcheck:
      test: ["CMD-SHELL", "pg_isready -U watchtrack_user -d watchtrack"]
      interval: 10s
      timeout: 5s
      retries: 5
    networks:
      - watchtrack-network

  # WatchTrack API
  api:
    build:
      context: .
      dockerfile: Dockerfile
    container_name: watchtrack-api
    environment:
      - ASPNETCORE_ENVIRONMENT=Docker
    ports:
      - "8080:8080"
    depends_on:
      db:
        condition: service_healthy
    networks:
      - watchtrack-network

volumes:
  postgres_data:

networks:
  watchtrack-network:
    driver: bridge
```

**Key Features:**
- Health check pentru PostgreSQL
- Dependency management (API așteaptă DB)
- Persistent volumes pentru date
- Network isolation

### 8.2 Configuration Management

#### 8.2.1 appsettings.json (Local - SQLite)

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=watchtrack.db"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

#### 8.2.2 appsettings.Docker.json (Docker - PostgreSQL)

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=db;Database=watchtrack;Username=watchtrack_user;Password=watchtrack_password"
  }
}
```

---

## 9. Frontend Architecture

### 9.1 Technology Stack
- **HTML5**: Structure
- **CSS3**: Styling (Glassmorphism, Gradients, Animations)
- **Vanilla JavaScript**: Logic (No frameworks)
- **Google Fonts**: Poppins font family

### 9.2 Component Structure

```
Frontend Components:
│
├── Header Section
│   ├── Title: "WatchTrack Pro"
│   └── Subtitle
│
├── Status Bar
│   ├── Connection Status
│   └── API Health
│
├── Statistics Dashboard
│   ├── User Count Card
│   ├── Movie Count Card
│   ├── Series Count Card
│   ├── Season Count Card
│   ├── Episode Count Card
│   └── Review Count Card
│
├── Navigation Tabs
│   ├── Dashboard Tab
│   ├── Users Tab
│   ├── Movies Tab
│   ├── Series Tab
│   ├── Reviews Tab
│   └── Watchlist Tab
│
└── Content Sections (Dynamic)
    ├── Dashboard View
    ├── Users Grid
    ├── Movies Grid
    ├── Series Grid (with drill-down)
    │   ├── Series List
    │   ├── Seasons View
    │   └── Episodes View
    ├── Reviews Grid
    └── Watchlist Grid
```

### 9.3 Data Flow

```
Page Load
    │
    ├──> Fetch /api/users
    ├──> Fetch /api/movies
    ├──> Fetch /api/series
    ├──> Fetch /api/seasons
    ├──> Fetch /api/episodes
    ├──> Fetch /api/reviews
    └──> Fetch /api/watchlist
    │
    ├──> Store in allData object
    │
    └──> Render UI Components
         │
         ├──> Update Statistics
         ├──> Populate Grids
         └──> Enable Navigation
```

### 9.4 Design System

**Color Palette:**
- Primary: `#667eea` → `#764ba2` (Gradient)
- Background: `#1e3c72` → `#2a5298` → `#7e22ce` (Gradient)
- Text: White with opacity variations
- Accent: `#8b5cf6`, `#a78bfa` (Purple shades)

**Typography:**
- Font: Poppins (Google Fonts)
- Weights: 300, 400, 500, 600, 700

**Effects:**
- Glassmorphism: `backdrop-filter: blur(20px)`
- Shadows: `box-shadow: 0 8px 32px rgba(0,0,0,0.2)`
- Animations: fadeIn, fadeInDown, fadeInUp, hover effects

---

## 10. Security Considerations

### 10.1 Input Validation
- **Server-side validation**: Data Annotations în DTOs
- **Client-side validation**: HTML5 validation attributes
- **Entity validation**: Required, MaxLength, Range attributes

### 10.2 SQL Injection Prevention
- **Entity Framework Core**: Parametrizare automată
- **No raw SQL**: Toate query-urile prin LINQ

### 10.3 CORS Configuration
```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

app.UseCors("AllowAll");
```

### 10.4 Error Handling
- Try-catch blocks în servicii
- Status codes HTTP corecte
- Logging pentru debugging

---

## 11. Performance Optimization

### 11.1 Database Optimization
- **Indexes**: Username, Email (unique indexes)
- **Eager Loading**: Include() pentru relații când e necesar
- **Async Operations**: Toate operațiile DB sunt async

### 11.2 API Optimization
- **DTO Usage**: Proiecție în LINQ pentru a returna doar datele necesare
- **Pagination**: Poate fi adăugată pentru liste mari
- **Caching**: Poate fi implementat pentru date frecvent accesate

### 11.3 Frontend Optimization
- **Single Page Application**: Fără reload între secțiuni
- **Data Caching**: allData object pentru navigare rapidă
- **Lazy Rendering**: Doar secțiunea activă e vizibilă

---

## 12. Testing Strategy

### 12.1 Unit Testing
- **Target**: Services Layer
- **Framework**: xUnit sau NUnit
- **Mocking**: Moq pentru DbContext

### 12.2 Integration Testing
- **Target**: Controllers + Services + Database
- **Framework**: ASP.NET Core TestHost
- **Database**: In-Memory SQLite pentru teste

### 12.3 API Testing
- **Tool**: Swagger UI, Postman
- **Coverage**: Toate endpoint-urile (40+)
- **Scenarios**: CRUD complet, cascade deletes, validări

---

## 13. Scalability Considerations

### 13.1 Horizontal Scaling
- **Stateless API**: Nu păstrează session state
- **Load Balancer Ready**: Pot rula multiple instanțe
- **Database Connection Pooling**: EF Core default behavior

### 13.2 Vertical Scaling
- **Database**: PostgreSQL scalează bine
- **Memory**: Async operations pentru memory efficiency
- **CPU**: Multi-threading support în .NET

---

## 14. Maintenance și Extensibilitate

### 14.1 Adding New Entity
1. Create Entity class în Entities/
2. Add DbSet în WatchTrackDbContext
3. Create DTOs (Read, Create, Update)
4. Create Service Interface și Implementation
5. Create Controller
6. Register Service în Program.cs
7. Run migration: `dotnet ef migrations add NewEntity`

### 14.2 Code Organization Principles
- **SOLID Principles**: Respectate în design
- **DRY**: Service pattern reduce duplicarea
- **Separation of Concerns**: Layered architecture
- **Dependency Injection**: Loose coupling

---

## 15. Technical Specifications

### 15.1 Development Environment
- **.NET SDK**: 8.0
- **IDE**: Visual Studio, VS Code, Rider
- **Database Tools**: pgAdmin (PostgreSQL), DB Browser (SQLite)

### 15.2 Production Environment
- **OS**: Linux (Docker containers)
- **Web Server**: Kestrel (built-in ASP.NET Core)
- **Database**: PostgreSQL 16
- **Container Runtime**: Docker Engine

### 15.3 Dependencies (NuGet Packages)
```xml
<PackageReference Include="Microsoft.EntityFrameworkCore" Version="8.0.0" />
<PackageReference Include="Microsoft.EntityFrameworkCore.Sqlite" Version="8.0.0" />
<PackageReference Include="Npgsql.EntityFrameworkCore.PostgreSQL" Version="8.0.0" />
<PackageReference Include="Swashbuckle.AspNetCore" Version="6.5.0" />
```

---

## 16. Deployment Workflow

### 16.1 Local Development
1. `dotnet restore` - Restore dependencies
2. `dotnet ef database update` - Apply migrations
3. `dotnet run` - Start application
4. Navigate to http://localhost:5000

### 16.2 Docker Deployment
1. `docker-compose build` - Build images
2. `docker-compose up -d` - Start containers
3. Wait for health checks
4. Navigate to http://localhost:8080

---

## 17. Monitoring și Logging

### 17.1 Application Logging
- **Framework**: Microsoft.Extensions.Logging
- **Levels**: Information, Warning, Error
- **Output**: Console, File (configurable)

### 17.2 Database Logging
- **EF Core Logging**: SQL query logging în Development
- **Performance**: Slow query detection

### 17.3 Docker Logging
```bash
docker logs watchtrack-api        # View logs
docker logs -f watchtrack-api     # Follow logs
```

---

## 18. Future Enhancements

### 18.1 Potential Features
- **Authentication**: JWT-based authentication
- **Authorization**: Role-based access control
- **Search**: Full-text search pentru filme/seriale
- **Recommendations**: Algorithm pentru recomandări
- **Social Features**: Follow users, share watchlists
- **Notifications**: Email notifications pentru episoade noi

### 18.2 Technical Improvements
- **Caching**: Redis pentru performance
- **Message Queue**: RabbitMQ pentru async processing
- **CDN**: Pentru poster images
- **API Versioning**: Support pentru multiple API versions
- **GraphQL**: Alternative query interface

---

## 19. Diagrame Tehnice

### 19.1 Sequence Diagram: Create Movie

```
Client          Controller        Service          DbContext        Database
  │                 │                │                 │                │
  │──POST /movies──>│                │                 │                │
  │                 │                │                 │                │
  │                 │─CreateMovie()─>│                 │                │
  │                 │                │                 │                │
  │                 │                │───Add(movie)───>│                │
  │                 │                │                 │                │
  │                 │                │─SaveChangesAsync()─>             │
  │                 │                │                 │                │
  │                 │                │                 │───INSERT───────>│
  │                 │                │                 │                │
  │                 │                │                 │<───ID, Status──│
  │                 │                │                 │                │
  │                 │                │<───movie────────│                │
  │                 │                │                 │                │
  │                 │<───MovieDto────│                 │                │
  │                 │                │                 │                │
  │<─201 Created────│                │                 │                │
  │                 │                │                 │                │
```

### 19.2 Class Diagram: Core Entities

```
┌────────────────────┐
│      User          │
├────────────────────┤
│ - Id: int          │
│ - Username: string │
│ - Email: string    │
│ - CreatedAt: Date  │
└────────────────────┘
         △
         │ 1
         │
         │ *
    ┌────────┐
    │ Review │
    └────────┘
         │ *
         │
         │ 1
         ▽
┌────────────────────┐
│      Movie         │
├────────────────────┤
│ - Id: int          │
│ - Title: string    │
│ - Description      │
│ - ReleaseYear: int │
│ - Genre: string    │
│ - Duration: int    │
└────────────────────┘
```

---

## 20. Glosar Tehnic

**Entity Framework Core (EF Core)**: ORM pentru .NET care mapează obiecte la tabele de bază de date

**DTO (Data Transfer Object)**: Obiect folosit pentru transfer de date între layere, fără logică de business

**Dependency Injection (DI)**: Pattern de design care injectează dependențele în loc să le creeze intern

**CORS (Cross-Origin Resource Sharing)**: Mechanism de securitate pentru requests între domenii diferite

**Swagger/OpenAPI**: Standard pentru documentarea API-urilor RESTful

**Cascade Delete**: Ștergere automată a entităților copil când părintele e șters

**Docker Compose**: Tool pentru definirea și rularea aplicațiilor multi-container

**Health Check**: Verificare automată a stării serviciilor (PostgreSQL în cazul nostru)

**Middleware**: Componente software care procesează requests și responses în pipeline-ul ASP.NET Core

---

**Document Control:**
- **Autor:** Sofron Alexandru, Necula Valentin, Margarit Petrut
- **Revizie:** 1.0
- **Data:** 15 Ianuarie 2026
- **Status:** Final
