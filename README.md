# KingdomHospital - Back-End

KingdomHospital est un système de gestion médicale développé avec **ASP.NET Core** et **Entity Framework Core**. Ce dépôt contient toutes les entités, la configuration Fluent API, le DbContext, les migrations et la génération de la base SQL Server.

## Statut actuel

* **Entités (Models)** : toutes les classes représentant les tables ✅ Terminé
* **Fluent API** : relations et contraintes ✅ Terminé
* **DbContext** : configuration complète ✅ Terminé
* **Migration initiale** : création de la base ✅ Terminé
* **Vérification SQL** : tables et relations vérifiées ✅ Terminé
* **Seed Data** : ajout des données initiales ⏳ En cours
* **API (Controllers)** : endpoints REST ⏳ Pas commencé
* **DTOs + AutoMapper** : sécurisation et structuration des échanges ⏳ En cours
* **Validation** : FluentValidation ⏳ Pas commencé

---

## 1. Entités (Domain Models)

### Pourquoi

Les entités représentent les **tables de la base de données**. Elles permettent de structurer les données de manière logique et de gérer les relations entre elles. Chaque entité correspond à un concept du domaine médical : médecins, patients, consultations, médicaments, ordonnances, etc.

### Comment

* Propriétés simples avec `[Required]` et `[MaxLength]`
* Propriétés nullables (`string?`, `int?`) pour champs optionnels
* Collections initialisées (`new List<T>()`) pour éviter les `NullReferenceException`
* Relations navigationnelles pour EF Core

**Exemple**:

```csharp
public class Consultation
{
    public int Id { get; set; }
    public int DoctorId { get; set; }
    public Doctor Doctor { get; set; } = null!;
    public int PatientId { get; set; }
    public Patient Patient { get; set; } = null!;
    public DateTime Date { get; set; }
    public TimeSpan Time { get; set; }
    public string? Reason { get; set; }
    public ICollection<Ordonnance> Ordonnances { get; set; } = new List<Ordonnance>();
}
```

---

## 2. Fluent API (Configurations)

### Pourquoi

La **Fluent API** permet un **contrôle fin** des relations et contraintes, pour assurer l'intégrité de la base.

### Comment

* Chaque entité possède sa propre classe de configuration
* Relations One-to-Many et Many-to-One définies explicitement
* Comportements de suppression adaptés (`Restrict`, `SetNull`)
* Longueurs maximales, champs requis et unicités appliqués

**Exemple**:

```csharp
builder.HasMany(d => d.Consultations)
       .WithOne(c => c.Doctor)
       .HasForeignKey(c => c.DoctorId)
       .OnDelete(DeleteBehavior.Restrict);
```

---

## 3. DbContext

### Pourquoi

Le **DbContext** représente la base de données dans le code, permettant les opérations CRUD et l'application des configurations.

### Comment

* Tous les DbSet pour les entités sont déclarés
* `OnModelCreating` applique automatiquement toutes les configurations Fluent API
* Prêt pour les requêtes et insertions sécurisées

---

## 4. Migrations

### Pourquoi

Les migrations synchronisent le code C# et la base SQL. Elles génèrent la structure des tables et appliquent les contraintes.

### Comment

* Migration initiale `InitialCreate` créée
* Base **KindomHospitalDb** générée automatiquement
* Structure confirmée (tables, colonnes, clés primaires/étrangères)

**Commandes EF Core**:

```bash
Add-Migration InitialCreate
Update-Database
```

---

## 5. Vérification SQL

### Pourquoi

Pour s'assurer que la base correspond au modèle avant de développer l’API.

### Comment

* Ouverture de SQL Server Management Studio
* Vérification des tables, colonnes, clés primaires/étrangères, contraintes

**Exemple**:

```sql
USE KindomHospitalDb;
GO
SELECT * FROM Doctors;
SELECT * FROM Patients;
SELECT * FROM Consultations;
SELECT * FROM Specialties;
SELECT * FROM Medicaments;
SELECT * FROM Ordonnances;
SELECT * FROM OrdonnanceLignes;
```

---

## 6. DTOs (Data Transfer Objects)

### Pourquoi

Les DTOs permettent de **protéger les entités et contrôler les échanges avec l’API**, évitant d’exposer des données sensibles ou des relations complexes.

### Comment

* Chaque entité possède `CreateDTO`, `UpdateDTO` et `ReadDTO`
* Validations `[Required]`, `[MaxLength]` et `[Range]` appliquées
* Relations simplifiées pour la lecture (`DoctorName`, `PatientName`)

**Exemple Specialty**:

```csharp
public class SpecialtyReadDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
```

* Créer avec `SpecialtyCreateDTO`
* Modifier avec `SpecialtyUpdateDTO`
* Lire ID et nom avec `SpecialtyReadDTO`

---

💡 Cette structure détaillée permet à ton binôme de comprendre **le pourquoi et le comment** de chaque étape sans poser de questions.

## 6. DTOs et AutoMapper

### Pourquoi AutoMapper

**AutoMapper** permet de **mapper automatiquement les entités vers les DTO et vice versa**. Cela évite d'écrire du code répétitif pour copier les valeurs des propriétés. Avec AutoMapper, quand on reçoit ou renvoie un objet via l'API, on utilise un DTO, mais l'entité originale reste protégée.

### Comment

* Créer un fichier `MappingProfile.cs` dans le dossier `Mapper`
* Définir les mappings pour chaque entité :

  * `CreateMap<Entity, ReadDTO>()` pour les retours GET
  * `CreateMap<CreateDTO, Entity>()` pour les créations POST
  * `CreateMap<UpdateDTO, Entity>()` pour les modifications PUT
* Injecter AutoMapper dans les contrôleurs et l'utiliser pour convertir entre DTO et entités

**Exemple MappingProfile**:

```csharp
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Specialty, SpecialtyReadDTO>();
        CreateMap<SpecialtyCreateDTO, Specialty>();
        CreateMap<SpecialtyUpdateDTO, Specialty>();

        CreateMap<Doctor, DoctorReadDTO>();
        CreateMap<DoctorCreateDTO, Doctor>();
        CreateMap<DoctorUpdateDTO, Doctor>();

        // Idem pour les autres entités...
    }
}
```

### Exemple dans un contrôleur

```csharp
[HttpGet]
public async Task<ActionResult<IEnumerable<SpecialtyReadDTO>>> GetAll()
{
    var specialties = await _context.Specialties.ToListAsync();
    var specialtiesDTO = _mapper.Map<List<SpecialtyReadDTO>>(specialties);
    return Ok(specialtiesDTO);
}
```

💡 Avec AutoMapper et les DTO, on **évite d'exposer directement les entités** et on **simplifie le code** des contrôleurs, tout en gardant la structure des données sécurisée et cohérente.

