namespace POC.Processos.Entities;

public class Process
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Number { get; private set; }
    public DateTime Date { get; private set; }
    public List<Part> Parts { get; private set; } = new();

    public Process(string number, DateTime date)
    {
        Number = number;
        Date   = date;
    }

    public void Add(params Part[] parts)
    {
        Parts.AddRange(parts);
    }
}