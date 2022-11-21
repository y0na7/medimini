# Ordinationsprojektet

Projektet indeholder en prototype på et ordinationssystem, der kan bruges til at ordinere medicin til borgere. 

Der ligger en dotnet solution-fil som kan åbnes i Visual Studio. Disse projekter indgår:
- **ordination-api**: En ASP.NET web api der implementere funktionaliteten til at ordinere medicin og hente / gemme data i databasen.
- **ordination-blazor**: En .NET Blazor frontend der benytter sig af "ordination-api".
- **shared**: Fælles C# model-klasser for de to .NET-projekter.
- **ordination-test**: Projekt der demonstrerer hvordan man kan teste med MStest. 

## Afprøv projektet fra Visual Studio 2022
Der er to måder at testet projektet på lokalt på sin egen computer.

Den nemme løsning er at bruge Visual Studio 2022:
1. Åben solution-filen.
2. Byg hele projektet
3. Find en terminal og kør kommandoen `dotnet ef database update --project ordination-api/` for at bygge database (kun første gang).
4. Start API-projektet (run)
5. Start Blazor-programmet (run)
6. Brug blazor-app'en fra din favorit-browser.

## Afprøv projektet fra terminalen
Hvis man vil køre projektet uden visual studio, kræver det blot  man har installeret [**.NET 6 SDK**](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) på sin PC.

Vejledningen herunder tager udgangspunkt i en bash- eller zsh-terminal. På Windows anbefales [**git bash**](https://git-scm.com/downloads) som følger med git. 

Brug følgende kommandoer i terminalen (fra projektets rodfolder):

```sh
$ dotnet build
$ dotnet ef database update --project ordination-api/
$ dotnet run --project ordination-api & dotnet run --project ordination-blazor && fg
```

Nu burde du kunne benytte Blazor-app'en på https://localhost:7428 
