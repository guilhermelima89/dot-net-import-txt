namespace Api.Models;

public class ArquivoComErroValidacao : Entity
{
    public int NumeroLinhaArquivoOriginal { get; set; }
    public string TextoLinhaArquivoOriginal { get; set; }
    public string Erro { get; set; }
    public int ArquivoId { get; set; }
    public Arquivo Arquivo { get; set; }
}