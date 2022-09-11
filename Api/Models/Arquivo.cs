namespace Api.Models;

public class Arquivo : Entity
{
    public string Nome { get; set; }
    public byte[] ArquivoOriginal { get; set; }
    public byte[] ArquivoRetorno { get; set; }
    public DateTime? DataInicioProcessamento { get; set; }
    public DateTime? DataFimProcessamento { get; set; }
    public int ArquivoStatusId { get; set; }
    public ArquivoStatus ArquivoStatus { get; set; }
}