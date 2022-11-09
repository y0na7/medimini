namespace shared.Model;

// DTO: Data Transfer Object

public record PN_DTO(int patientId, int laegemiddelId, double antal, DateTime startDato, DateTime slutDato);
public record DagligFastDTO(int patientId, int laegemiddelId,
        double antalMorgen, double antalMiddag, double antalAften, double antalNat,
        DateTime startDato, DateTime slutDato);

public record DagligSkaevDTO(int patientId, int laegemiddelId, Dosis[] doser,
        DateTime startDato, DateTime slutDato);
public record DateTimeDTO(DateTime date);
public record MsgRecord (string msg);
public record AnbefaletDosisDTO(int laegemiddelId, double anbefaletDosis);