KingdomHospital - Back-End

KingdomHospital est un système de gestion médicale développé avec ASP.NET Core et Entity Framework Core.
Ce projet gère les entités médicales, leurs relations, les prescriptions et les consultations. Ce README sert à expliquer chaque étape réalisée, le raisonnement derrière chaque choix et la manière dont cela a été implémenté.

1. Entités (Domain Models)
Pourquoi ?
Les entités représentent les tables de notre base de données. Elles permettent de structurer les données de manière logique et de gérer les relations entre elles. Chaque entité correspond à un concept du domaine médical : médecins, patients, consultations, médicaments, ordonnances, etc.

Comment ?
Chaque entité a été créée avec :
Des propriétés simples (string, int, DateTime) avec [Required] pour garantir la présence de données et [MaxLength] pour limiter la taille des chaînes.

Des propriétés nullables (string?, int?) pour gérer les relations optionnelles, comme ConsultationId dans Ordonnance.

Des collections initialisées (new List<T>()) pour éviter les erreurs null (NullReferenceException).

Des relations navigationnelles pour EF Core, permettant de naviguer entre entités (Doctor.Consultations, Consultation.Ordonnances, etc.).

Exemple :
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

2. Fluent API (Configurations)
Pourquoi ?
Même si EF Core peut générer automatiquement les tables et les relations à partir des entités, la Fluent API permet un contrôle fin : gérer les contraintes, renommer les tables, définir les comportements de suppression (DeleteBehavior) et sécuriser l’intégrité des relations.

Comment ?
Chaque entité possède sa propre classe de configuration.
Les relations One-to-Many et Many-to-One sont définies explicitement.

Les comportements de suppression sont adaptés : Restrict pour empêcher la suppression si une relation existe, SetNull pour mettre à null certaines clés étrangères.

Les longueurs maximales, les champs requis et les unicités sont appliqués.

Exemple :
builder.HasMany(d => d.Consultations)
       .WithOne(c => c.Doctor)
       .HasForeignKey(c => c.DoctorId)
       .OnDelete(DeleteBehavior.Restrict);

3. DbContext
Pourquoi ?
Le DbContext est le cœur d’EF Core : il représente la base de données dans le code, permet de faire des opérations CRUD et gère l’application des configurations.

Comment ?
Tous les DbSet représentant les tables sont déclarés.
OnModelCreating applique automatiquement toutes les configurations Fluent API.
Prêt pour les requêtes, insertions, mises à jour et suppressions sécurisées.

4. Migrations
Pourquoi ?
Les migrations permettent de synchroniser le code C# et la base SQL. Elles génèrent la structure des tables et appliquent les contraintes définies dans les entités et configurations.

Comment ?
Une migration initiale InitialCreate a été créée.
La base KindomHospitalDb a été générée automatiquement dans SQL Server.
Toutes les tables, clés primaires, clés étrangères et contraintes ont été vérifiées.

Commandes EF Core :

Add-Migration InitialCreate
Update-Database

5. Vérification SQL
Pourquoi ?
Vérifier que la base de données correspond aux attentes du modèle permet de détecter les erreurs avant de développer l’API.
Comment ?
Ouverture de SQL Server Management Studio pour vérifier l’existence des tables et des colonnes.
Test des clés primaires et étrangères, des contraintes NOT NULL, MAX LENGTH et UNIQUE.
Vérification que les relations navigationnelles sont correctes.

Exemple de vérification :

USE KindomHospitalDb;
GO
SELECT * FROM Doctors;
SELECT * FROM Patients;
SELECT * FROM Consultations;
SELECT * FROM Specialties;
SELECT * FROM Medicaments;
SELECT * FROM Ordonnances;
SELECT * FROM OrdonnanceLignes;

6. DTOs (Data Transfer Objects)
Pourquoi ?
Les DTOs permettent de protéger les entités et contrôler les échanges avec l’API. Cela évite de renvoyer directement les entités complètes, ce qui pourrait exposer des données sensibles ou des relations complexes.

Comment ?
Chaque entité possède trois DTOs : CreateDTO, UpdateDTO, ReadDTO.
Les DTOs appliquent des validations [Required], [MaxLength] et [Range] pour assurer l’intégrité côté API.
Les relations sont simplifiées dans les DTO de lecture, par exemple avec DoctorName ou PatientName.

Exemple pour Specialty :

Créer une spécialité avec SpecialtyCreateDTO

Modifier le nom avec SpecialtyUpdateDTO

Lire l’ID et le nom avec SpecialtyReadDTO

Exemple DTO :

public class SpecialtyReadDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}


💡 Avec cette structure, ton binôme peut comprendre à quoi sert chaque étape, pourquoi on le fait, et comment ça a été implémenté, sans avoir besoin de poser de questions.
