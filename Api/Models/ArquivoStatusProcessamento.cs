namespace Api.Models;

public class ArquivoStatusProcessamento : Entity
{
    public int QtLinhas { get; set; }
    public int QtLinhasErros { get; set; }
    public int QtLinhasProcessadas { get; set; }
    public int ArquivoId { get; set; }
    public Arquivo Arquivo { get; set; }
}