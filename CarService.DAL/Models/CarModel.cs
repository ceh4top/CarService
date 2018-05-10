using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarService.DAL.Models
{
    [Table("CarModels")]
    public class CarModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        [ForeignKey("Brand")]
        public int BrandId { get; set; }
        public virtual CarBrand Brand { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
    }
}
