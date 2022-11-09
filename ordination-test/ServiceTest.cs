namespace ordination_test;

using Microsoft.EntityFrameworkCore;
using shared.Model;
using Service;
using Data;

public class ServiceTest
{
    private readonly DataService service;

    public ServiceTest()
    {
        var optionsBuilder = new DbContextOptionsBuilder<OrdinationContext>();
        optionsBuilder.UseInMemoryDatabase(databaseName: "test-database"); 
        var context = new OrdinationContext(optionsBuilder.Options);
        service = new DataService(context);
        service.SeedData();
    }

    [Fact]
    public void PatientsExist()
    {
        Assert.NotNull(service.GetPatienter());
    }
}