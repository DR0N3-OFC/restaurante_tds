using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aula03.Models
{
    public class Garcom
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]


        public int? GarcomID { get; set; }


        [Required(ErrorMessage = "Nome é obrigatório")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        public string? SecondName { get; set; }

        [Required(ErrorMessage = "Telefone é obrigatório")]
        public string? Telefone { get; set; }


        [Required(ErrorMessage = "Data é obrigatória")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime? BirthDate { get; set; }
        [Required(ErrorMessage = "Estatística é obrigatória")]
        public Double? Estatistica { get; set; }
    }
}
