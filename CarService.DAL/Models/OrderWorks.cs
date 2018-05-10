using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarService.DAL.Models
{
    [Table("OrderWorks")]
    public class OrderWorks
    {
        [ForeignKey("Work")]
        public int WorkId { get; set; }
        public virtual Work Work { get; set; }

        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }

        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
    }
}
