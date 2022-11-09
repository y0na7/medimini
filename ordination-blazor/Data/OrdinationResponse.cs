using shared.Model;

namespace ordinationsapp.Model;
public record OrdinationResponse(PN[] pn, DagligFast[] dagligFast, DagligSkæv[] dagligSkaev);