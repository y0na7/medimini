namespace shared.Model;

public abstract class Ordination {
    public int OrdinationId { get; set; }
    public DateTime startDen { get; set; }
    public DateTime slutDen { get; set; }
    public Laegemiddel laegemiddel { get; set; }
    
    public Ordination(Laegemiddel laegemiddel, DateTime startDen = new DateTime(), DateTime slutDen = new DateTime()) {
    	this.startDen = startDen;
    	this.slutDen = slutDen;
        this.laegemiddel = laegemiddel;
    }

    public Ordination()
    {
        this.laegemiddel = null!;
    }

    /**
     * Antal hele dage mellem startdato og slutdato. Begge dage inklusive.
     * @return antal dage ordinationen gælder for
     */
    public int antalDage() {
        // TODO: Implement!
        return -1;
    }

    public override String ToString() {
        return startDen.ToString();
    }

    /**
     * Returnerer den totale dosis der er givet i den periode ordinationen er gyldig
     * @return
     */
    public abstract double samletDosis();

    /**
     * Returnerer den gennemsnitlige dosis givet pr dag i den periode ordinationen er gyldig
     * @return
     */
    public abstract double doegnDosis();

    /**
     * Returnerer ordinationstypen som en String
     * @return
     */
    public abstract String getType(); // TODO: Omdøb? Minder for meget om den indbyggede i C#.
}
