using iomhiagh_api.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace iomhiagh_api.mediator
{
    public class SkillInteractionDbLogNotificationHandler : INotificationHandler<SkillInteractiondNotificationMessage>
    {
        private readonly IomhaighDbContext _context;

        public SkillInteractionDbLogNotificationHandler(IomhaighDbContext context)
        {
            _context = context;
        }

        public Task Handle(SkillInteractiondNotificationMessage notification, CancellationToken cancellationToken)
        {
            SkillEvent skillEvent = new SkillEvent
            {
                AffectedSkill = notification.AffectedSkill,
                EventType = notification.EventType,
                EventTime = notification.UpdateTime
            };

            _context.SkillEvents.Add(skillEvent);
            _context.SaveChanges();

            return Task.CompletedTask;
        }
    }
}
