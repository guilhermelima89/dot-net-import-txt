namespace Api.ViewModels;

public class ConfirmacaoCadastralHeader
{
    public string Tipo { get; set; }
    public string Versao { get; set; }
    public List<ConfirmacaoCadastralOperacao> ConfirmacaoCadastral { get; set; }
}

public class ConfirmacaoCadastralOperacao
{
    public string Identificador { get; set; }
    public string RazaoSocial { get; set; }
    public string NomeAcesso { get; set; }
    public string ContaPrincipal { get; set; }
    public string Confirmacao { get; set; }
}
