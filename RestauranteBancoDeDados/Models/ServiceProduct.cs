using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aula03.Models
{
    public class ServiceProduct
    {
        public int? ServiceProductID { get; set; }
        public Product Product { get; set; }
        public int? Amount { get; set; }
    }
}
