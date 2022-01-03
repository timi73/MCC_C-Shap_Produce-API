using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.ViewModel
{
    public class GetRegisterVM
    {
        public string FullName { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }
        public int Salary { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Degree { get; set; }
        public float GPA { get; set; }
        public string UniversityName { get; set; }
    }
}
