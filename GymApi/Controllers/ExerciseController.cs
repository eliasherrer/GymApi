using GymApi.DTOs;
using GymApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace GymApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExerciseController : ControllerBase
    {
        private ICommonService<ExerciseDto, ExerciseInsertDto, ExerciseUpdateDto> _exerciseService;

        public ExerciseController([FromKeyedServices("ExerciseService")]
        ICommonService<ExerciseDto, ExerciseInsertDto, ExerciseUpdateDto> exerciseService)
        {
            _exerciseService = exerciseService;
        }

        [HttpGet]
        public async Task<IEnumerable<ExerciseDto>> Get()
            => await _exerciseService.Get();

        [HttpGet("{id}")]
        public async Task<ActionResult<ExerciseDto>> GetById (int id)
        {
            var exerciseDto = await _exerciseService.GetById(id);
            return exerciseDto == null ? NotFound() : Ok(exerciseDto);
        }

        [HttpPost]
        public async Task<ActionResult<ExerciseDto>> Create(ExerciseInsertDto exerciseInsertDto)
        {
            if (!_exerciseService.Validate(exerciseInsertDto))
            {
                return BadRequest(_exerciseService.Errors);
            }
            var exerciseDto = await _exerciseService.Create(exerciseInsertDto);
            return CreatedAtAction(nameof(GetById), new { id = exerciseDto.Id }, exerciseDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ExerciseDto>> Update (ExerciseUpdateDto exerciseUpdateDto, int id)
        {
            if (!_exerciseService.Validate(exerciseUpdateDto))
            {
                return BadRequest(_exerciseService.Errors);
            }
            var exerciseDto = await _exerciseService.Update(exerciseUpdateDto, id);
            return exerciseUpdateDto == null ? NotFound() : Ok(exerciseDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ExerciseDto>> Delete(int id)
        {
            var exerciseDto = await _exerciseService.Delete(id);
            return exerciseDto == null ? NotFound() : Ok(exerciseDto);
        }

    }


}
