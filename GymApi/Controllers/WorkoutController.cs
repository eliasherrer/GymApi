using AutoMapper;
using GymApi.DTOs;
using GymApi.Repository;
using GymApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GymApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutController : ControllerBase
    {

        private ICommonService<WorkoutDto, WorkoutInsertDto, WorkoutUpdateDto> _commonService;

        public WorkoutController([FromKeyedServices("WorkoutService")]
        ICommonService<WorkoutDto, WorkoutInsertDto, WorkoutUpdateDto> commonService)
        {
            _commonService = commonService;
        }

        [HttpGet]
        public async Task <IEnumerable<WorkoutDto>> Get() 
            => await _commonService.Get();

        [HttpGet("{id}")]
        public async Task <ActionResult<WorkoutDto>> GetById(int id)
        {
            var workoutDto = await _commonService.GetById(id);
            return workoutDto == null ? NotFound() : Ok(workoutDto);
        }

        [HttpPost]
        public async Task <ActionResult<WorkoutDto>> Create (WorkoutInsertDto workoutInsertDto)
        {
            var workoutDto = await _commonService.Create(workoutInsertDto);

            if (workoutDto == null)
            {
               return BadRequest(_commonService.Errors);
            }
            return CreatedAtAction(nameof(GetById), new { id = workoutDto.Id }, workoutDto);
        }

        [HttpPut("{id}")]
        public async Task <ActionResult<WorkoutDto>> Update(WorkoutUpdateDto workoutUpdateDto, int id)
        {
            var workoutDto = await _commonService.Update(workoutUpdateDto, id);
            return workoutDto == null ? NotFound() : Ok(workoutDto);
        }

        [HttpDelete("{id}")]
        public async Task <ActionResult<WorkoutDto>> Delete (int id)
        {
            var workoutDto = await _commonService.Delete(id);
            return workoutDto == null ? NotFound() : Ok(workoutDto);
        }
        
    }
}
