using iomhiagh_api.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static iomhiagh_api.Model.SkillEvent;

namespace iomhiagh_api.mediator
{
    public interface ISkillInteractionNotifierMediator
    {
        void Notify(InteractionEventType eventType, Skill skill);
    }

    public class SkillInteractionNotifierMediator : ISkillInteractionNotifierMediator
    {
        private readonly IMediator _mediator;

        public SkillInteractionNotifierMediator(IMediator mediator)
        {
            _mediator = mediator;
        }

        public void Notify(InteractionEventType eventType, Skill skill)
        {
            _mediator.Publish(new SkillInteractiondNotificationMessage
            {
                AffectedSkill = skill,
                EventType = eventType,
                UpdateTime = DateTime.Now
            }) ;
        }
    }
}
