using System.Text.Json.Serialization;

namespace POC.Processos.Entities;

public class Part
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; private set; }
    public bool Active { get; private set; } = true;
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public PartKind Kind { get; private set; }

    public Part(string name, PartKind kind)
    {
        Name = name;
        Kind = kind;
    }
}