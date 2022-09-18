namespace Api.Models;

public class ConfirmacaoCadastralHeader
{
    public string Tipo { get; set; }
    public string Versao { get; set; }
    public int NumeroLinhaArquivoOriginal { get; set; }
    public string TextoLinhaArquivoOriginal { get; set; }
    public List<ConfirmacaoCadastralOperacao> ConfirmacaoCadastral { get; set; }
}

public class ConfirmacaoCadastralOperacao
{
    public string RazaoSocial { get; set; }
    public string NomeAcesso { get; set; }
    public string ContaPrincipal { get; set; }
    public string Confirmacao { get; set; }
    public int NumeroLinhaArquivoOriginal { get; set; }
    public string TextoLinhaArquivoOriginal { get; set; }
}
