using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aula03.Models
{
    public class Produto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]


        public int? ID { get; set; }


        [Required(ErrorMessage = "Nome é obrigatório")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Preço é obrigatório")]
        public double? Price { get; set; }
    }
}
