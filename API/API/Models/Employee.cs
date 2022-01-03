using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{

    [Table("tb_m_employees")]
    public class Employee
    {
        [Key]
        public string NIK { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        //[StringLength(maximumLength: 12, MinimumLength =10, ErrorMessage ="Phone Number Maximal 12 Karakter dan Minimal 10 Karakter")]
        [Phone, Required]
        public string Phone { get; set; }

        [Required, DataType(DataType.Date)]
        public DateTime Birthdate { get; set; }

        public int Slary { get; set; }

        [EmailAddress, Required]
        public string Email { get; set; }

        public Gender Gender { get; set; }

        [JsonIgnore]
        public virtual Account Account { get; set; }
    }

    public enum Gender {Male,Female}
}
