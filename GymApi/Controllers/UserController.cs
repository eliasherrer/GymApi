using GymApi.Context;
using GymApi.DTOs;
using GymApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GymApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private ICommonService<UserDto, UserInsertDto, UserUpdateDto> _userService;
        

        public UserController(
            [FromKeyedServices("UserService")]ICommonService<UserDto, UserInsertDto, UserUpdateDto>userService) {
            
            _userService = userService;
        }

        [HttpGet]
        public async Task<IEnumerable<UserDto>> Get() =>
            await _userService.Get();

        [HttpGet("{id}")]

        public async Task<ActionResult<UserDto>> GetById(int id)
        {
            var userDto = await _userService.GetById(id);

            return userDto == null ? NotFound() : Ok(userDto);
        }

        [HttpPost]
        public async Task <ActionResult<UserDto>> Create(UserInsertDto userInsertDto)
        {
            if (!_userService.Validate(userInsertDto))
            {
                return BadRequest(_userService.Errors);
            }
            var userDto = await _userService.Create(userInsertDto);
            return CreatedAtAction(nameof(GetById), new { id = userDto.Id}, userDto);
        }

        [HttpPut("{id}")]

        public async Task <ActionResult<UserDto>> Update (UserUpdateDto userUpdateDto, int id)
        {
            if (!_userService.Validate(userUpdateDto))
            {
                return BadRequest(_userService.Errors);
            }
            var userDto = await _userService.Update(userUpdateDto, id);
            return userDto == null ? NotFound() : Ok(userDto);
        }

        [HttpDelete("{id}")]

        public async Task <ActionResult<UserDto>> Delete (int id)
        {
            var userDto = await _userService.Delete(id);
            return userDto == null ? NotFound() : Ok(userDto);
        }
    }
}
