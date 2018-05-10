using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarService.DAL.Models
{
    [Table("CarOwners")]
    public class CarOwner : Human
    {
        public virtual ICollection<Car> Cars { get; set; }
    }
}
