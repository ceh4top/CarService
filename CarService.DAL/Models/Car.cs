using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarService.DAL.Models
{
    [Table("Cars")]
    public class Car
    {
        [Key]
        public int Id { get; set; }

        public string StateNumber { get; set; }

        public CarBrand Brand => this.Model?.Brand;

        [ForeignKey("Model")]
        public int ModelId { get; set; }
        public virtual CarModel Model { get; set; }

        public DateTime YearOfManufacture { get; set; }

        [ForeignKey("TransmissionType")]
        public int TransmissionTypeId { get; set; }
        public virtual CarTransmissionType TransmissionType { get; set; }

        public int EnginePower { get; set; }

        [ForeignKey("CarOwner")]
        public int CarOwnerId { get; set; }
        public virtual CarOwner CarOwner { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
