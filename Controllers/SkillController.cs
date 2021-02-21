using iomhiagh_api.mediator;
using iomhiagh_api.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iomhiagh_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SkillController : ControllerBase
    {
        private readonly ILogger<SkillController> _logger;
        private readonly IomhaighDbContext _context;
        private readonly ISkillInteractionNotifierMediator _mediator;

        public SkillController(IomhaighDbContext context, ILogger<SkillController> logger, ISkillInteractionNotifierMediator mediator)
        {
            _context = context;
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Skill>>> GetSkills()
        {
            return _context.Skills
                .Where(skill => skill.Parent == null)
                .Include(skill => skill.Children)
                .ToList();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSkill(long id, Skill skill)
        {
            if (id != skill.SkillID)
            {
                return BadRequest();
            }

            _context.Entry(skill).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                _mediator.Notify(SkillEvent.InteractionEventType.UPDATED, skill);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SkillExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Skill>> GetSkill(int id)
        {
            var skill = _context.Skills
                .Where(skill => skill.SkillID == id)
                .Include(skill => skill.Children)
                .First();

            if (skill == null)
            {
                return NotFound();
            }

            _mediator.Notify(SkillEvent.InteractionEventType.REQUESTED, skill);
            return skill;
        }

        [HttpPost]
        public async Task<ActionResult<Skill>> PostSkill(Skill skill)
        {
            _context.Skills.Add(skill);
            await _context.SaveChangesAsync();

            _mediator.Notify(SkillEvent.InteractionEventType.CREATED, skill);
            return CreatedAtAction(nameof(GetSkill), new { id = skill.SkillID }, skill);
        }

        private bool SkillExists(long id)
        {
            return _context.Skills.Any(e => e.SkillID == id);
        }
    }
}
