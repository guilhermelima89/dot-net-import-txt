using Api.Data;
using Api.Interfaces;
using Api.Models;

namespace Api.Repositories;

public class ArquivoRepository : Repository<Arquivo>, IArquivoRepository
{
    public ArquivoRepository(ApplicationDbContext context) : base(context)
    {
    }
}