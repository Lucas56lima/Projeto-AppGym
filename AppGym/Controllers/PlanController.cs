using Domain.Entities;
using Domain.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppGym.Controllers
{
    [Route("api/[controller]")]
    [ApiController]    
    public class PlanController : ControllerBase
    {
        private readonly IPlanService _planService;
        public PlanController (IPlanService planService)
        {
            _planService = planService;
        }
        [HttpPost("RegisterPlan")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles="super,admin")]
        public async Task<IActionResult> PostPlanAsync([FromBody]Plan plan)
        {
            return Ok(await _planService.PostPlanAsync(plan));
        }
        [HttpGet("ViewPlanById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "super,admin")]
        public async Task<IActionResult> GetPlanByIdAsync(int id)
        {
            return Ok(await _planService.GetPlanByIdAsync(id));
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "super,admin")]
        [HttpGet("ViewPlanByName")]
        public async Task<IActionResult> GetPlanByNameAsync(string name)
        {
            return Ok(await _planService.GetPlanByNameAsync(name));
        }
        [HttpGet("ViewAllPlans")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]        
        public async Task<IActionResult> GetAllPlansAsync()
        {
            return Ok(await _planService.GetAllPlansAsync());
        }

    }
}
