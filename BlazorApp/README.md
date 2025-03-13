\# Vårt Projekt

## Projektöversikt

Detta Blazor-projekt implementerar ett forumliknande system där användare kan skapa trådar, skicka meddelanden och interagera med varandra. Projektet använder ASP.NET Core Identity för användarhantering och Entity Framework Core för databasinteraktioner.

## Huvudfunktionalitet

### Meddelandehantering

Projektet innehåller följande meddelandefunktioner:

- **Loggöversikt**: Visar alla meddelanden grupperade efter tråd.
- **Roller baserad åtkomst**: Administratörer kan se alla meddelanden medan vanliga användare endast kan se sina egna.
- **Redigering av meddelanden**: Användare kan redigera sina egna meddelanden, administratörer kan redigera alla.
- **Radering av meddelanden**: Användare kan radera sina egna meddelanden, administratörer kan radera alla.
- **Svara på meddelanden**: Användare kan svara på befintliga meddelanden med visuell indentering för svar.

### Trådhantering

- **Trådöversikt**: Visar alla trådar i systemet.
- **Trådvy**: Visar alla meddelanden i en specifik tråd med svarsfunktionalitet.
- **Radering av trådar**: Trådägare och administratörer kan radera trådar.

### Kontohantering

- **Profilhantering**: Användare kan uppdatera e-post, telefonnummer och lösenord.
- **Kontoradering**: Två metoder för att radera konton:
  - **Snabbradering**: Direkt radering från profilsidan med bekräftelsedialog.
  - **Avancerad radering**: Dedikerad sida med ytterligare bekräftelsessteg och transaktionshantering.

## Implementationsdetaljer

### Loggmeddelandehantering

Meddelandehanteringen inkluderar:

- Visuell indentering för svar med blå vänsterkant
- Visning av tråddata för varje meddelande
- Direktredigering i gränssnittet
- Bekräftelsedialoger för radering
- Visuell återkoppling under bearbetning
- Felhantering med användarvänliga meddelanden

### Kontoradering

Kontoraderingsfunktionaliteten tillämpar:

- Databasrelationer hanteras korrekt
- Transaktioner används för att säkerställa dataintegritet
- Beroende data (meddelanden, trådar) raderas i rätt ordning
- Visuell feedback under raderingsprocessen
- Omfattande felhantering

## Säkerhetsöverväganden

- Användarroller respekteras i alla vyer
- Lösenordsverifiering används för känsliga operationer
- Transaktionshantering förhindrar delvis raderad data
- Bekräftelsedialog förhindrar oavsiktliga raderingar

**How to use Main Admin role:** Navigera till Models/RoleInitializer.cs, på rad 30 finner du användarnamn med lösenord 
på rad 43. Använd dom när du loggar in på Applikationen i syfte av full funktionalitetstest.

## Teknisk plattform

- ASP.NET Core 8
- Entity Framework Core
- Blazor Server
- SQLite (för utveckling)
- Bootstrap 5 (UI-ramverk)

1.---

\#\# Användarscenarion  

**Kategori:**

\-Inloggning

**Scenario:**

\-En användare vill logga in på sitt konto för att få tillgång till forumets funktioner.

**Teststeg:**

\-Navigera till inloggningssidan.

\-Ange registrerad e-postadress och lösenord.

\-Klicka på knappen "Logga in".

**Förväntat resultat:**

\-Om inloggningsuppgifterna är korrekta, omdirigeras användaren till sin profil eller forumets startsida.

\-Om uppgifterna är felaktiga, visas ett tydligt felmeddelande.

**Åtgärdsförslag:**

\-Implementera hantering av felaktiga inloggningsförsök, inklusive tydliga felmeddelanden.

\-Lägg till en "Glömt lösenord?"-funktion för att hjälpa användare att återställa sina uppgifter.

\-Säkerställ att konton med för många misslyckade inloggningsförsök tillfälligt spärras av säkerhetsskäl.

 2.---

**Kategori:**

\-Kommentarer

**Scenario:**

\-En användare vill kommentera på en befintlig tråd för att delta i diskussionen.

**Teststeg:**

1. Logga in på forumet.  
2. Navigera till en kategori och välj en tråd att kommentera.  
3. Skriv din kommentar i textfältet.  
4. Klicka på "Send".

**Förväntat resultat:**

\-Kommentaren visas direkt under tråden tillsammans med användarens namn och datum.

\-Användare kan svara på andra kommentarer.

**Åtgärdsförslag:**

\-Implementera en bekräftelse för att undvika oavsiktliga kommentarer.

3.---

**Kategori:**

\-Användarprofil

**Scenario:**

\-En användare vill uppdatera sin profilinformation.

**Teststeg:**

\-Logga in på forumet.

\-Navigera till "Manage Account"-sidan.

\-Klicka på "Redigera profil".

\-Uppdatera önskade fält (t.ex. användarnamn, telefonnummer).

\-Klicka på "Spara ändringar".

**Förväntat resultat:**

\-De uppdaterade uppgifterna sparas och visas korrekt på användarens profil.

\-Ett bekräftelsemeddelande visas, t.ex. "Profil uppdaterad framgångsrikt".

**Åtgärdsförslag:**

\-Validera inmatade uppgifter (t.ex. e-postformat, begränsningar för användarnamn).

\-Säkerställ att ändringar sparas korrekt i databasen och uppdateras direkt i UI.

