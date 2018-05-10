using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarService.DAL.Models
{
    [Table("Humans")]
    public class Human
    {
        [Key]
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }

        public DateTime YearOfBirth { get; set; }

        public string PhoneNumber { get; set; }
    }
}
