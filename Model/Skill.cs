using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace iomhiagh_api.Model
{
    public class Skill
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SkillID { get; set; }
        public string Name { get; set; }
        public int Proficiency { get; set; }
        public string BarColor { get; set; }
        public string Description { get; set; }
        public virtual Skill Parent { get; set; }
        public virtual HashSet<Skill> Children { get; set; }
    }
}
