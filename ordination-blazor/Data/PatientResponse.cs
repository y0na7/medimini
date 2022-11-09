using shared.Model;

namespace ordinationsapp.Model;
public record PatientResponse(int id, string cprnr, string navn, double vaegt, int[] ordinationer);