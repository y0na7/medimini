namespace shared.Model;

public class Laegemiddel
{
    public int LaegemiddelId { get; set; }
    public string navn { get; set; }
    public double enhedPrKgPrDoegnLet { get; set; }    // faktor der anvendes hvis patient vejer < 25 kg
    public double enhedPrKgPrDoegnNormal { get; set; } // faktor der anvendes hvis 25 kg <= patient vægt <= 120 kg
    public double enhedPrKgPrDoegnTung { get; set; }   // faktor der anvendes hvis patient vægt > 120 kg 
    public String enhed { get; set; }

    public Laegemiddel(String navn, double enhedPrKgPrDoegnLet, double enhedPrKgPrDoegnNormal,
            double enhedPrKgPrDoegnTung, String enhed)
    {
        this.navn = navn;
        this.enhedPrKgPrDoegnLet = enhedPrKgPrDoegnLet;
        this.enhedPrKgPrDoegnNormal = enhedPrKgPrDoegnNormal;
        this.enhedPrKgPrDoegnTung = enhedPrKgPrDoegnTung;
        this.enhed = enhed;
    }

    public Laegemiddel() {
        this.navn = "";
        this.enhed = "";
    }

    public override String ToString()
    {
        return navn;
    }
}
