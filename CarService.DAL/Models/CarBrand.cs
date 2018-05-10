using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarService.DAL.Models
{
    [Table("CarBrands")]
    public class CarBrand
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CarModel> Models { get; set; }
    }
}
