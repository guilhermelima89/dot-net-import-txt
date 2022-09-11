namespace Api.Models;

public class ArquivoSemErroValidacao : Entity
{
    public int ArquivoId { get; set; }
    public Arquivo Arquivo { get; set; }
    public string Identificador { get; set; }
    public string RazaoSocial { get; set; }
    public string NomeAcesso { get; set; }
    public string ContaPrincipal { get; set; }
    public string Confirmacao { get; set; }
}