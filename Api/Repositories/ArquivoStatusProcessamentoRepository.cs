using Api.Data;
using Api.Interfaces;
using Api.Models;

namespace Api.Repositories;

public class ArquivoStatusProcessamentoRepository : Repository<ArquivoStatusProcessamento>, IArquivoStatusProcessamentoRepository
{
    public ArquivoStatusProcessamentoRepository(ApplicationDbContext context) : base(context)
    {
    }
}