using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace iomhiagh_api.Model
{
    public class SkillEvent
    {
        public enum InteractionEventType
        {
            CREATED,
            UPDATED,
            REQUESTED
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SkillEventID { get; set; }
        public Skill AffectedSkill { get; set; }
        public DateTime EventTime { get; set; }
        public InteractionEventType EventType { get; set; }
    }
}
