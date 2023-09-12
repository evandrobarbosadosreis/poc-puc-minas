namespace POC.Integracao.Entities;

public class Integration
{
    public int Id { get; private set; }
    public DateTime Date { get; private set; } = DateTime.Now;
    public string Number { get; private set; }
    public Status Status { get; private set; } = Status.Pending;

    public Integration(string number)
    {
        Number = number;
    }
}