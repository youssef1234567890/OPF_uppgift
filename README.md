# OPF_uppgift — Forumprojekt

## Projektbeskrivning

Ett forumliknande system byggt med Blazor Server och ASP.NET Core 8. Användare kan registrera sig, logga in, skapa trådar i olika kategorier och skriva meddelanden. Systemet har tre rollnivåer: vanlig användare, admin och mainadmin.

## Teknisk plattform

- ASP.NET Core 8
- Blazor Server
- Entity Framework Core med SQLite
- ASP.NET Core Identity (autentisering och roller)
- Bootstrap 5

## Arkitektur

Projektet följer Blazor Servers komponentmodell:

- `Components/Pages/` — sidkomponenter (Home, CategoryList, Category, ThreadView, Log, Admin, Mainadmin)
- `Components/Layout/` — NavMenu och MainLayout
- `Components/Account/` — registrering, inloggning och kontoinställningar
- `Data/ApplicationDbContext.cs` — databaskontext med Identity och forumtabeller
- `Models/` — entiteter (Thread, Message) och RoleInitializer
- `Migrations/` — Entity Framework-migrationer
- `Services/LogService.cs` — service för meddelandelogg

## Kom igång

1. Klona repot
2. Öppna `BlazorApp.sln` i Visual Studio
3. Kör projektet — databasen skapas automatiskt och roller/mainadmin seedas vid uppstart

## Roller och åtkomst

Systemet har tre roller med olika behörigheter:

| Roll | Åtkomst |
|------|---------|
| Användare | Skapa trådar, skriva och svara på meddelanden, redigera/radera egna meddelanden |
| Admin | Allt ovan + redigera/radera alla meddelanders, se alla användares meddelanden i loggen |
| Mainadmin | Allt ovan + hantera roller, promota/demota användare, radera användare |

## Testa administratörsrollen

Mainadmin-kontot skapas automatiskt vid uppstart via `RoleInitializer.cs`.

**Inloggningsuppgifter:**
- E-post: `mainadmin@example.com`
- Lösenord: `YourSecurePassword123!`

För att testa admin-rollen: logga in som mainadmin, navigera till `/mainadmin` och promota en annan användare till admin. Den användaren får då tillgång till `/admin`.

## Användarscenarion

### 1. Inloggning

**Scenario:** En användare vill logga in för att delta i forumet.

**Teststeg:**
1. Navigera till `/Account/Login`
2. Ange e-postadress och lösenord
3. Klicka på "Log in"

**Förväntat resultat:** Användaren omdirigeras till startsidan och hälsas med sitt användarnamn.

---

### 2. Skapa en tråd

**Scenario:** En inloggad användare vill starta en diskussion i en kategori.

**Teststeg:**
1. Navigera till "Categories" i navmenyn
2. Välj en kategori (t.ex. Games)
3. Klicka på "Create New Thread"
4. Fyll i titel och beskrivning, klicka på "Create"

**Förväntat resultat:** Tråden visas i kategorilistan.

---

### 3. Svara på ett meddelande

**Scenario:** En användare vill svara på ett befintligt meddelande i en tråd.

**Teststeg:**
1. Navigera till en tråd
2. Klicka på "Reply" på ett meddelande
3. Skriv ett svar och skicka

**Förväntat resultat:** Svaret visas indraget under originalmeddelandet.

---

### 4. Adminmoderation

**Scenario:** En admin vill radera ett olämpligt meddelande.

**Teststeg:**
1. Logga in som admin eller mainadmin
2. Navigera till `/log`
3. Hitta meddelandet och klicka på "Delete"

**Förväntat resultat:** Meddelandet tas bort för alla användare.

---

### 5. Hantera användarroller (mainadmin)

**Scenario:** Mainadmin vill ge en användare adminrättigheter.

**Teststeg:**
1. Logga in med mainadmin-kontot
2. Navigera till `/mainadmin`
3. Hitta användaren i listan och klicka på "Make Admin"

**Förväntat resultat:** Användaren får admin-rollen och tillgång till `/admin`.