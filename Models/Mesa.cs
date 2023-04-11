using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aula03.Models
{
    public class Mesa
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int? MesaID { get; set; }

        public bool? Status { get; set; }

        public DateTime? HoraLiberacao { get; set; }
        [Required(ErrorMessage = "Estatística é obrigatória")]
        public Double? Estatistica { get; set; }

    }
}
