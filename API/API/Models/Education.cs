using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("tb_m_educations")]
    public class Education
    {
        public int Id { get; set; }
        public string Degree { get; set; }
        public float GPA { get; set; }
        [JsonIgnore]
        public virtual University University { get; set; }
        public int UniversityId { get; set; }
        [JsonIgnore]
        public virtual ICollection<Profiling> Profilings { get; set; }

    }
}
