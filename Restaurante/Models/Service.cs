using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aula03.Models
{
    public class Service
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? ServiceID { get; set; }

        public Waiter? Waiter { get; set; }

        [Required(ErrorMessage = "Mesa é obrigatória")]
        public Table Table { get; set; }

        public List<ServiceProduct>? ServiceProducts { get; set; } = new List<ServiceProduct>();

        [Required(ErrorMessage = "Data de início é obrigatória")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime? InitDate { get; set; }
        public DateTime? FinishDate { get; set; }
    }
}
