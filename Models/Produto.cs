using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aula03.Models
{
    public class Produto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? ProdutoID { get; set; }


        [Required(ErrorMessage = "Nome é obrigatório")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Descrição é obrigatória")]
        public string? Desc { get; set; }

        [Required(ErrorMessage = "Preço é obrigatório")]
        public double? Price { get; set; }

        [Required(ErrorMessage = "Categoria é obrigatória")]
        public Categoria? Categoria { get; set; }
    }
}
