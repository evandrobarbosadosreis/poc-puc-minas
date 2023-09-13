using System.Text.Json.Serialization;

namespace POC.Integracao.Entities;

public class Integration
{
    public int Id { get; private set; }
    public DateTime Date { get; private set; } = DateTime.Now;
    public string Number { get; private set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Status Status { get; private set; } = Status.Pending;

    public Integration(string number)
    {
        Number = number;
    }
}