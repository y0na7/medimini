
namespace shared.Model;

public class Patient {
    public int PatientId { get; set; }
    public String cprnr { get; set; }
    public String navn { get; set; }
    public double vaegt { get; set; }
    public List<Ordination> ordinationer { get; set; } = new List<Ordination>();

    public Patient(String cprnr, String navn, double vaegt) {
        this.cprnr = cprnr;
        this.navn = navn;
        this.vaegt = vaegt;
    }

    public Patient() {
        this.cprnr = "";
        this.navn = "";
    }

    public override String ToString() {
        return navn + " " + cprnr;
    }
}
