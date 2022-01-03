using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace API.Models
{
    [Table("tb_m_profiling")]
    public class Profiling
    {
        [Key]
        public string NIK { get; set; }
        [JsonIgnore]
        public virtual Education Education { get; set; }
        public int EducationId { get; set; }
        [JsonIgnore]
        public virtual Account Account { get; set; }
    }
}
