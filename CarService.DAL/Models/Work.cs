using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarService.DAL.Models
{
    [Table("Works")]
    public class Work
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        public virtual ICollection<OrderWorks> OrderWorks { get; set; }
    }
}
