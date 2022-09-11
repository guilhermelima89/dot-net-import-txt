using Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Mappings
{
    public class ArquivoStatusMapping : IEntityTypeConfiguration<ArquivoStatus>
    {
        public void Configure(EntityTypeBuilder<ArquivoStatus> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Descricao)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.HasData(
                new ArquivoStatus
                {
                    Id = 1,
                    Descricao = "Aguardando processamento",
                    DataCadastro = DateTime.Now
                },
                new ArquivoStatus
                {
                    Id = 2,
                    Descricao = "Processado sem erros",
                    DataCadastro = DateTime.Now
                },
                new ArquivoStatus
                {
                    Id = 3,
                    Descricao = "Processado parcialmente com erros",
                    DataCadastro = DateTime.Now
                },
                new ArquivoStatus
                {
                    Id = 4,
                    Descricao = "Não processado",
                    DataCadastro = DateTime.Now
                },
                new ArquivoStatus
                {
                    Id = 5,
                    Descricao = "Processando",
                    DataCadastro = DateTime.Now
                }
            );

            /* ENTITY */
            builder.Property(x => x.DataCadastro)
                .HasColumnType("datetime");

            builder.Property(x => x.DataAlteracao)
                .HasColumnType("datetime");

            builder.ToTable("ArquivoStatus");
        }
    }
}
