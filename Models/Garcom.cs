using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aula03.Models
{
    public class Garcom
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]


        public int? ID { get; set; }


        [Required(ErrorMessage = "Nome é obrigatório")]
        public string? Name { get; set; }


        [Required(ErrorMessage = "Gênero é obrigatório")]
        public string? Gender { get; set; }


        [Required(ErrorMessage = "Data é obrigatória")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime? BirthDate { get; set; }
    }
}
