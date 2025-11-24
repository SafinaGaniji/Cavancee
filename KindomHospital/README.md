📌 Présentation du projet

KingdomHospital est un système de gestion médicale développé avec ASP.NET Core et Entity Framework Core.
Ce dépôt contient toute la partie Back-End, incluant :

Les entités (modèles)

La configuration Fluent API

Le DbContext

Les migrations EF Core

La génération de la base SQL Server

Ce README sert aussi de suivi d’avancement pour le développement.

📊 Statut d’avancement global
Étape	Description	Statut
1️⃣ Entités (Models)	Création de toutes les classes représentant les tables	✅ Terminé
2️⃣ Fluent API	Relations, contraintes, comportements Delete	✅ Terminé
3️⃣ DbContext	Configuration complète	✅ Terminé
4️⃣ Migration initiale	Add-Migration InitialCreate	✅ Terminé
5️⃣ Update Database	Base KindomHospitalDb créée	✅ Terminé
6️⃣ Vérification SQL	Toutes les tables présentes	✅ Terminé
7️⃣ Seed Data	Ajouter données initiales	⏳ En cours
8️⃣ API (Controllers)	Endpoints REST	⏳ Pas commencé
9️⃣ Swagger / Documentation API	Interface & tests	⏳ Pas commencé
🔟 Frontend	Interface utilisateur	⏳ Pas commencé
🧱 Architecture du projet
KingdomHospital/
│
├── Models/
│   ├── Consultation.cs
│   ├── Doctor.cs
│   ├── Patient.cs
│   ├── Specialty.cs
│   ├── Medicament.cs
│   ├── Ordonnance.cs
│   └── OrdonnanceLigne.cs
│
├── Configurations/
│   ├── ConsultationConfiguration.cs
│   ├── DoctorConfiguration.cs
│   ├── PatientConfiguration.cs
│   ├── SpecialtyConfiguration.cs
│   ├── MedicamentConfiguration.cs
│   ├── OrdonnanceConfiguration.cs
│   └── OrdonnanceLigneConfiguration.cs
│
├── Data/
│   └── AppDbContext.cs
│
└── Migrations/
    ├── InitialCreate.cs
    ├── InitialCreate.Designer.cs
    └── AppDbContextModelSnapshot.cs

🧠 Ce qui a été implémenté (techniquement)
✔ Entités (Domain Models)

Propriétés avec [Required], [MaxLength], relations navigationnelles…

Nullabilité respectée (string?, initialisation = string.Empty)

✔ Fluent API

Configuration fine des relations :

One-to-Many

Many-to-One

DeleteBehavior : Restrict, SetNull

Renommage des tables

Contraintes supplémentaires

✔ DbContext

Tous les DbSet configurés

OnModelCreating : application automatique de toutes les configurations

✔ Migrations

Migration initiale créée

Base SQL Server générée automatiquement

Structure confirmée côté SSMS

🗺️ Roadmap (visuelle)
📦 Version 1.0 – Base de données (OK ✔)
│
├── ✔ Création des entités
├── ✔ Configurations Fluent API
├── ✔ Mise en place du DbContext
└── ✔ Migration + génération de la base SQL

🚧 Version 1.1 – API (en cours)
│
├── ⏳ Controllers (Patients, Doctors, Consultations…)
├── ⏳ Services (business logic)
├── ⏳ DTOs + Automapper
└── ⏳ Validation (FluentValidation)

✨ Version 2.0 – Documentation & outils
│
├── ⏳ Intégration Swagger
├── ⏳ Documentation des endpoints
└── ⏳ Tests Postman automatisés

🎨 Version 3.0 – Frontend
│
├── ⏳ Choix du framework (Blazor / MVC / MAUI)
├── ⏳ Connexion API
└── ⏳ UI complète

🛠️ Commandes importantes EF Core
➤ Créer une migration
Add-Migration NomDeMigration

➤ Appliquer les migrations
Update-Database

➤ Supprimer la dernière migration
Remove-Migration

🧪 Vérification SQL

Dans SQL Server Management Studio, vérifier la base :

USE KindomHospitalDb;
GO

SELECT * FROM Doctors;
SELECT * FROM Patients;
SELECT * FROM Consultations;