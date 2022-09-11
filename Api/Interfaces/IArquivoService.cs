using Api.Models;

namespace Api.Interfaces;

public interface IArquivoService
{
    Task Adicionar(IFormFileCollection arquivos);
    Task<bool> Processar(int idArquivo);
}