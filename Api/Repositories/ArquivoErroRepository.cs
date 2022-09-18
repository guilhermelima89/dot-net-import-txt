using Api.Data;
using Api.Interfaces;
using Api.Models;

namespace Api.Repositories;

public class ArquivoErroRepository : Repository<ArquivoErro>, IArquivoErroRepository
{
    public ArquivoErroRepository(ApplicationDbContext context) : base(context)
    {
    }
}