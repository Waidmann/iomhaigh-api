using iomhiagh_api.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static iomhiagh_api.Model.SkillEvent;

namespace iomhiagh_api.mediator
{
    public class SkillInteractiondNotificationMessage : INotification
    {
        public InteractionEventType EventType { get; set; }
        public Skill AffectedSkill { get; set; }
        public DateTime UpdateTime { get; set; }

        public override string ToString()
        {
            return String.Format("[{0}] {1} on skill with name {2}", UpdateTime, EventType, AffectedSkill.Name);
        }
    }
}
