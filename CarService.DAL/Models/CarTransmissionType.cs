using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarService.DAL.Models
{
    [Table("CarTransmissionTypes")]
    public class CarTransmissionType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
    }
}
