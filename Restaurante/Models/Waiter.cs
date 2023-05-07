using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aula03.Models
{
    public class Waiter
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]


        public int? WaiterID { get; set; }


        [Required(ErrorMessage = "Nome é obrigatório")]

		[RegularExpression(@"^[a-zA-ZÀ-ÿ\u00f1\u00d1]+$", ErrorMessage = "Somente letras são permitidas.")]
		public string? Name { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]

		[RegularExpression(@"^[a-zA-ZÀ-ÿ\u00f1\u00d1]+$", ErrorMessage = "Somente letras são permitidas.")]
		public string? LastName { get; set; }

        [Required(ErrorMessage = "Telefone é obrigatório")]
        public string? Cellphone { get; set; }


        [Required(ErrorMessage = "Data é obrigatória")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime? BirthDate { get; set; }
    }
}
