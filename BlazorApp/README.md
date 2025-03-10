\# Vårt Projekt

\#\# Beskrivning  
Detta projekt är en **forumapplikation byggd med Blazor Server**. Applikationen ger användare möjlighet att delta i diskussioner genom att skapa och hantera trådar samt kommentera andras inlägg. Forumet är uppdelat i olika kategorier, och användare kan organisera och filtrera innehåll baserat på deras intressen.  

\#\# Teknologi  
Projektet använder följande tekniker:  
**Frontend:** Blazor Server (C\# och .NET)  
**Backend:** ASP.NET Core  
**Databas:** SQLite / Entity Framework Core  
**Autentisering & Auktorisering:** Identity Framework (för hantering av användare och roller)
**How to use Main Admin role:** Navigera till Models/RoleInitializer.cs, på rad 30 finner du användarnamn med lösenord 
på rad 43. Använd dom när du loggar in på Applikationen i syfte av full funktionalitetstest.

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

\-Användare kan svara på andra kommentarer (nested comments).

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

