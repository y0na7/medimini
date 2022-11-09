namespace ordination_test;

using shared.Model;

public class PatientTest
{
    [Fact]
    public void PatientHasName()
    {
        string cpr = "160563-1234";
        string navn = "John";
        double vægt = 83;
        
        Patient patient = new Patient(cpr, navn, vægt);
        Assert.Equal(navn, patient.navn);
    }

    [Fact]
    public void TestDerAltidFejler()
    {
        string cpr = "160563-1234";
        string navn = "John";
        double vægt = 83;

        Patient patient = new Patient(cpr, navn, vægt);
        Assert.Equal("Egon", patient.navn);
    }
}