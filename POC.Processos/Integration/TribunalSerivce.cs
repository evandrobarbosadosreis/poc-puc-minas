using AutoBogus;

namespace POC.Processos.Integration;

public class TribunalSerivce : ITribunalApi
{
    public Task<ProcessDTO> GetProcessData(string number)
    {
        Thread.Sleep(10000);
        
        if (number is "0")
        {
            throw new Exception("Falha de conexão com o tribunal");
        }

        var partes = new AutoFaker<PartDTO>()
            .RuleFor(x => x.Name, x => x.Person.FullName)
            .Generate(5);

        var process = new AutoFaker<ProcessDTO>()
            .RuleFor(x => x.Number, number)
            .RuleFor(x => x.Parts, partes)
            .Generate();
        
        return Task.FromResult(process);
    }
}