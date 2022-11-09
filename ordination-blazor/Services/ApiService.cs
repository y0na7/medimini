using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

using shared.Model;
using ordinationsapp.Model;

namespace ordinationsapp.Data;

public class ApiService
{
    private readonly HttpClient http;
    private readonly IConfiguration configuration;
    private readonly string baseAPI = "";
    public event Action? RefreshRequired;

    public ApiService(HttpClient http, IConfiguration configuration)
    {
        this.http = http;
        this.configuration = configuration;
        this.baseAPI = configuration["base_api"];
    }

    public void CallRequestRefresh()
    {
         RefreshRequired?.Invoke();
    }

    public async Task<OrdinationResponse?> GetOrdinationer()
    {
        string url = $"{baseAPI}ordinationer/";
        return await http.GetFromJsonAsync<OrdinationResponse>(url);
    }

    public async Task<PatientResponse[]?> GetPatienter()
    {
        string url = $"{baseAPI}patienter/";
        return await http.GetFromJsonAsync<PatientResponse[]>(url);

    }

    public async Task<Laegemiddel[]?> GetLaegemidler()
    {
        string url = $"{baseAPI}laegemidler";
        return await http.GetFromJsonAsync<Laegemiddel[]>(url);
    }

    public async Task<PN> OpretPN(int patientId, int laegemiddelId, double antal, DateTime startDato, DateTime slutDato)
    {
        string url = $"{baseAPI}ordinationer/pn/";
        PN_DTO opret = new(patientId, laegemiddelId, antal, startDato, slutDato);
        HttpResponseMessage res = await http.PostAsJsonAsync<PN_DTO>(url, opret);
        string json = res.Content.ReadAsStringAsync().Result;
        PN newPN = JsonSerializer.Deserialize<PN>(json)!;
        return newPN;
    }

    public async Task<DagligFast> OpretDagligFast(int patientId, int laegemiddelId, 
        double antalMorgen, double antalMiddag, double antalAften, double antalNat, 
        DateTime startDato, DateTime slutDato) {

        string url = $"{baseAPI}ordinationer/dagligfast/";
        DagligFastDTO opret = new(patientId, laegemiddelId, antalMorgen, antalMiddag, antalAften, antalNat, startDato, slutDato);
        HttpResponseMessage res = await http.PostAsJsonAsync<DagligFastDTO>(url, opret);
        string json = res.Content.ReadAsStringAsync().Result;
        DagligFast newDagligFast = JsonSerializer.Deserialize<DagligFast>(json)!;
        return newDagligFast;
    }

    public async Task<DagligSkæv> OpretDagligSkaev(int patientId, int laegemiddelId,
        Dosis[] doser, DateTime startDato, DateTime slutDato) {

        string url = $"{baseAPI}ordinationer/dagligskaev/";
        DagligSkaevDTO opret = new(patientId, laegemiddelId, doser, startDato, slutDato);
        HttpResponseMessage res = await http.PostAsJsonAsync<DagligSkaevDTO>(url, opret);
        string json = res.Content.ReadAsStringAsync().Result;
        DagligSkæv newDagligSkaev = JsonSerializer.Deserialize<DagligSkæv>(json)!;
        return newDagligSkaev;
    }

    public async Task<string> GivDosisPN(PN pn, DateTime date)
    {
        string url = $"{baseAPI}ordinationer/pn/{pn.OrdinationId}/anvend";
        HttpResponseMessage res = await http.PutAsJsonAsync<DateTimeDTO>(url, new DateTimeDTO(date));
        CallRequestRefresh();
        string json = res.Content.ReadAsStringAsync().Result;
        MsgRecord record = JsonSerializer.Deserialize<MsgRecord>(json)!;
        return record.msg;
    }

    public async Task<AnbefaletDosisDTO> GetAnbefaletDosisPerDøgn(int patientId, Laegemiddel lm) {
        string url = $"{baseAPI}patienter/{patientId}/beregnAnbefaletDosisPerDøgn";
        HttpResponseMessage res = await http.PostAsJsonAsync<AnbefaletDosisDTO>(url, new AnbefaletDosisDTO(lm.LaegemiddelId, -1));
        string json = res.Content.ReadAsStringAsync().Result;
        AnbefaletDosisDTO record = JsonSerializer.Deserialize<AnbefaletDosisDTO>(json)!;
        return record;
    }
}
