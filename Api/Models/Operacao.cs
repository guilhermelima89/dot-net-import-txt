namespace Api.Models;

public class Operacao
{
    public int NumeroLinhaArquivoOriginal { get; set; }
    public string TextoLinhaArquivoOriginal { get; set; }
    public string MensagemErro { get; set; }
    public Arquivo Arquivo { get; set; }
}