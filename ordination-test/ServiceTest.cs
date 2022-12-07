namespace ordination_test;

using Microsoft.EntityFrameworkCore;

using Service;
using Data;
using shared.Model;
using static shared.Util;

[TestClass]
public class ServiceTest
{
    private DataService service;

    [TestInitialize]
    public void SetupBeforeEachTest()
    {
        var optionsBuilder = new DbContextOptionsBuilder<OrdinationContext>();
        optionsBuilder.UseInMemoryDatabase(databaseName: "test-database");
        var context = new OrdinationContext(optionsBuilder.Options);
        service = new DataService(context);
        service.SeedData();
    }

    [TestMethod]
    public void PatientsExist()
    {
        Assert.IsNotNull(service.GetPatienter());
    }

    [TestMethod]
    public void OpretDagligFast()
    {
        Patient patient = service.GetPatienter().First();
        Laegemiddel lm = service.GetLaegemidler().First();

        Assert.AreEqual(1, service.GetDagligFaste().Count());

        DagligFast dagligfast = service.OpretDagligFast(patient.PatientId, lm.LaegemiddelId,
            2, 2, 1, 0, DateTime.Now, DateTime.Now.AddDays(3));

        Assert.AreEqual(2, service.GetDagligFaste().Count());
        Assert.AreEqual(5, dagligfast.doegnDosis());
        Assert.AreEqual(20, dagligfast.samletDosis());
    }

    [TestMethod]
    public void OpretDagligSkaev()
    {
        Patient patient = service.GetPatienter().First();
        Laegemiddel lm = service.GetLaegemidler().First();
        Dosis[] doser = new Dosis[] {
                new Dosis(CreateTimeOnly(12, 0, 0), 0.5),
                new Dosis(CreateTimeOnly(12, 40, 0), 1),
                new Dosis(CreateTimeOnly(16, 0, 0), 2.5),
                new Dosis(CreateTimeOnly(18, 45, 0), 3)
            };

        Assert.AreEqual(1, service.GetDagligSkæve().Count());

        service.OpretDagligSkaev(patient.PatientId, lm.LaegemiddelId, doser, DateTime.Now, DateTime.Now.AddDays(3));

        Assert.AreEqual(2, service.GetDagligSkæve().Count());
    }

    [TestMethod]
    public void OpretPN()
    {
        Patient patient = service.GetPatienter().First();
        Laegemiddel lm = service.GetLaegemidler().First();
       

        Assert.AreEqual(4, service.GetPNs().Count());

        PN pn = service.OpretPN(patient.PatientId, lm.LaegemiddelId, 4, DateTime.Now, DateTime.Now.AddDays(3));

        Dato datoTrue = new Dato();
        datoTrue.dato = DateTime.Now;

        Dato datoFalse = new Dato();
        datoFalse.dato = DateTime.Now.AddDays(4);

        Assert.AreEqual(5, service.GetPNs().Count());
        Assert.IsTrue(pn.givDosis(datoTrue));
        Assert.IsFalse(pn.givDosis(datoFalse));
    }

    [TestMethod]
    public void GetAnbefaletDosisPerDøgn()
    {
        Patient patientLet = service.GetPatienter()[0];
        Patient patientMedium = service.GetPatienter()[1];
        Patient patientTung = service.GetPatienter()[2];
        Laegemiddel lm = service.GetLaegemidler()[2];

        patientLet.vaegt = 24;
        patientMedium.vaegt = 80;
        patientTung.vaegt = 121;

        service.OpretPN(patientLet.PatientId, lm.LaegemiddelId, 4, DateTime.Now, DateTime.Now.AddDays(3));
      
        Assert.AreEqual(0.6, service.GetAnbefaletDosisPerDøgn(patientLet.PatientId,lm.LaegemiddelId), 0.1);
        Assert.AreEqual(2, service.GetAnbefaletDosisPerDøgn(patientMedium.PatientId, lm.LaegemiddelId), 0.1);
        Assert.AreEqual(3.025, service.GetAnbefaletDosisPerDøgn(patientTung.PatientId, lm.LaegemiddelId), 0.1);

    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestAtKodenSmiderEnException()
    {
        // Herunder skal man så kalde noget kode,
        // der smider en exception.
        service.OpretPN(-2, -3, 4, DateTime.Now, DateTime.Now.AddDays(3));

        Patient patientFejl = service.GetPatienter()[3];
        Laegemiddel lm = service.GetLaegemidler()[2];

        patientFejl.vaegt = -1;
        service.GetAnbefaletDosisPerDøgn(patientFejl.PatientId, lm.LaegemiddelId);
        // Hvis koden _ikke_ smider en exception,
        // så fejler testen.


        Console.WriteLine("Her kommer der ikke en exception. Testen fejler.");
    }
}