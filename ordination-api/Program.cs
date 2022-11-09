using Microsoft.EntityFrameworkCore;

using Service;
using Data;
using shared.Model;

var builder = WebApplication.CreateBuilder(args);

// Sætter CORS så API'en kan bruges fra andre domæner
var AllowCors = "_AllowCors";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowCors, builder => {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

// Tilføj DbContext factory som service.
builder.Services.AddDbContext<OrdinationContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("ContextSQLite")));

// Tilføj DataService så den kan bruges i endpoints
builder.Services.AddScoped<DataService>();

var app = builder.Build();

// Seed data hvis nødvendigt.
using (var scope = app.Services.CreateScope())
{
    var dataService = scope.ServiceProvider.GetRequiredService<DataService>();
    dataService.SeedData();
}

app.UseHttpsRedirection();
app.UseCors(AllowCors);

// Middlware der kører før hver request. Sætter ContentType for alle responses til "JSON".
app.Use(async (context, next) =>
{
    context.Response.ContentType = "application/json; charset=utf-8";
    await next(context);
});

app.MapGet("/", (DataService service) =>
{
    return Results.Ok("API is running");
});

app.MapGet("/api/ordinationer", (DataService service) =>
{
    List<PN> pn = service.GetPNs();
    List<DagligFast> dagligFast = service.GetDagligFaste();
    List<DagligSkæv> dagligSkaev = service.GetDagligSkæve();
    
    return Results.Ok(new {
        pn,
        dagligFast,
        dagligSkaev
    });
});

app.MapGet("/api/patienter", (DataService service) =>
{
    return service.GetPatienter().Select(p => new
    {
        id = p.PatientId,
        cprnr = p.cprnr,
        navn = p.navn,
        vaegt = p.vaegt,
        ordinationer = p.ordinationer.Select(o => o.OrdinationId)
    });
});

app.MapGet("/api/laegemidler", (DataService service) =>
{
    return service.GetLaegemidler();
});

app.MapPost("/api/ordinationer/pn/", (DataService service, PN_DTO dto) =>
{
    return service.OpretPN(dto.patientId, dto.laegemiddelId, dto.antal, dto.startDato, dto.slutDato);
});

app.MapPost("/api/ordinationer/dagligfast/", (DataService service, DagligFastDTO dto) =>
{
    return service.OpretDagligFast(dto.patientId, dto.laegemiddelId, dto.antalMorgen, dto.antalMiddag, dto.antalAften, dto.antalNat, dto.startDato, dto.slutDato);
});

app.MapPost("/api/ordinationer/dagligskaev/", (DataService service, DagligSkaevDTO dto) =>
{
    return service.OpretDagligSkaev(dto.patientId, dto.laegemiddelId, dto.doser, dto.startDato, dto.slutDato);
});

app.MapPut("/api/ordinationer/pn/{id}/anvend", (DataService service, int id, DateTimeDTO dto) =>
{
    return Results.Ok(new {msg = service.AnvendOrdination(id, new Dato{dato = dto.date})});
});

app.MapPost("/api/patienter/{id}/beregnAnbefaletDosisPerDøgn", (DataService service, int id, AnbefaletDosisDTO dto) =>
{
    double dosisStørrelse =  service.GetAnbefaletDosisPerDøgn(id, dto.laegemiddelId);
    AnbefaletDosisDTO response = new AnbefaletDosisDTO(dto.laegemiddelId, dosisStørrelse);
    return Results.Ok(response);
});

app.Run();