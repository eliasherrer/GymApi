using GymApi.DTOs;
using GymApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GymApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutSetController : ControllerBase
    {
        private ICommonService<WorkoutSetDto, WorkoutSetInsertDto, WorkoutSetUpdateDto> _commonService;

        public WorkoutSetController([FromKeyedServices("WorkoutSetService")]
        ICommonService<WorkoutSetDto, WorkoutSetInsertDto, WorkoutSetUpdateDto> commonService)
        {
            _commonService = commonService;
        }

        [HttpGet]
        public async Task<IEnumerable<WorkoutSetDto>> Get()
            => await _commonService.Get();

        [HttpGet("{id}")]
        public async Task<ActionResult<WorkoutDto>> GetById(int id)
        {
            var workoutDto = await _commonService.GetById(id);
            return workoutDto == null ? NotFound() : Ok(workoutDto);
        }

        [HttpPost]
        public async Task<ActionResult<WorkoutDto>> Create(WorkoutSetInsertDto workoutSetInsertDto)
        {

            var workoutSetDto = await _commonService.Create(workoutSetInsertDto);

            
            if (workoutSetDto == null)
            {
                return BadRequest(_commonService.Errors);
            }
            return CreatedAtAction(nameof(GetById), new { id = workoutSetDto.Id }, workoutSetDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<WorkoutDto>> Update(WorkoutSetUpdateDto workoutSetUpdateDto, int id)
        {
            var workoutDto = await _commonService.Update(workoutSetUpdateDto, id);
            return workoutDto == null ? NotFound() : Ok(workoutDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<WorkoutDto>> Delete(int id)
        {
            var workoutDto = await _commonService.Delete(id);
            return workoutDto == null ? NotFound() : Ok(workoutDto);
        }

    }
}
