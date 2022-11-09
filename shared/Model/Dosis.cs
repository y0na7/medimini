namespace shared.Model;

public class Dosis
{
    public int DosisId { get; set; }
    public DateTime tid { get; set; }
    public double antal { get; set; }

    public Dosis(DateTime tid, double antal)
    {
        this.tid = tid;
        this.antal = antal;
    }

    public Dosis()
    {
        this.tid = new DateTime();
        this.antal = 0;
    }

    public override String ToString()
    {
        return "Kl: " + tid.ToLongTimeString() + "    antal:  " + antal;
    }

}
