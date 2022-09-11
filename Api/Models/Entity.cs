using System.ComponentModel.DataAnnotations;

namespace Api.Models;

public abstract class Entity
{
    [Key]
    public int Id { get; set; }
    public DateTime DataCadastro { get; set; }
    public DateTime? DataAlteracao { get; set; }
}