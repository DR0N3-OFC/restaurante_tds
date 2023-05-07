using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aula03.Models
{
    public class Product
    {
        public int? ProductID { get; set; }


        [Required(ErrorMessage = "Nome é obrigatório")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Descrição é obrigatória")]
        public string? Description { get; set; }


        [DisplayFormat(DataFormatString = "R${0:N2}")]
        [Required(ErrorMessage = "Preço é obrigatório")]
        public double? Price { get; set; }

        [Required(ErrorMessage = "Categoria é obrigatória")]
        public Category? Category { get; set; }
    }
}
