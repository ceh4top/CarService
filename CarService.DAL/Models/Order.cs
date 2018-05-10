using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarService.DAL.Models
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Car")]
        public int CarId { get; set; }
        public virtual Car Car { get; set; }

        public CarOwner CarOwner => Car.CarOwner;

        public virtual ICollection<OrderWorks> OrderWorks { get; set; }

        public List<Work> WorksList => OrderWorks.Select(x => x.Work).ToList();

        public string Works => this.WorksList.Select(x => x.Name).Aggregate((x, y) => x + ", " + y);
        public DateTime? Start => OrderWorks.Min(x => x.Start);
        public DateTime? End => (OrderWorks.Any(x => x.End == null)) ? null : OrderWorks.Max(x => x.End);
        public double Cost => OrderWorks.ToList().Sum(x => x.Work.Price);
    }
}
