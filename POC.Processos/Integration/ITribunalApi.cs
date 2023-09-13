namespace POC.Processos.Integration;

public interface ITribunalApi
{
    Task<ProcessDTO> GetProcessData(string number);
}