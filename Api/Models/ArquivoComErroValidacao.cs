namespace Api.Models;

public class ArquivoComErroValidacao : Entity
{
    public int ArquivoId { get; set; }
    public Arquivo Arquivo { get; set; }
    public int NumeroLinha { get; set; }
    public string TextoLinha { get; set; }
    public string Erro { get; set; }
}