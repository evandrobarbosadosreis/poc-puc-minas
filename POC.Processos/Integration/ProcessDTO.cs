namespace POC.Processos.Integration;

public class ProcessDTO
{
    public string Number { get; set; }
    public DateTime Date { get; set; }
    public List<PartDTO> Parts { get; set; } = new();
}