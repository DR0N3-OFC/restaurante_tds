using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aula03.Models
{
    public class Categoria
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]


        public int? CategoriaID { get; set; }


        [Required(ErrorMessage = "Nome é obrigatório")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Estatística é obrigatória")]
        public Double? Estatistica { get; set; }
    }
}
