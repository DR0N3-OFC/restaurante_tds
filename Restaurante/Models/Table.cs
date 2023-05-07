using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aula03.Models
{
    public class Table
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int? TableID { get; set; }

        public string? Name { get; set; }
        public bool? Status { get; set; }

        public DateTime? LiberationTime { get; set; }

    }
}
