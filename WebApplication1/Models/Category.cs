using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aula03.Models
{
    public class Category
    {
        public int? CategoryID { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        public string? Name { get; set; }
    }
}
