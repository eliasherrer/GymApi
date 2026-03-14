using AutoMapper;
using FluentValidation;
using GymApi.Context;
using GymApi.DTOs;
using GymApi.Models;
using GymApi.Repository;
using GymApi.Validations;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace GymApi.Services
{
    public class UserService : ICommonService<UserDto, UserInsertDto, UserUpdateDto>
    {
        private IValidator<UserUpdateDto> _userUpdateValidator;
        private IValidator<UserInsertDto> _userInsertValidator;
        private IRepository<User> _commonRepository;
        private IMapper _mapper;

        public List<string> Errors {  get; }

        public UserService( IValidator<UserInsertDto> userInsertvalidator, IRepository<User> userRepository,
            IMapper mapper, IValidator<UserUpdateDto> userUpdateValidator)
        {
            _userUpdateValidator = userUpdateValidator;
            _userInsertValidator = userInsertvalidator;
            _commonRepository = userRepository;
            _mapper = mapper;
            Errors = new List<string>();
        }

        public async Task<IEnumerable<UserDto>> Get()
        {
            var user = await _commonRepository.Get();

            return user.Select(u => _mapper.Map<UserDto>(u));
        }
            

        public async Task<UserDto> GetById(int id)
        {
            var user = await _commonRepository.GetById(id);
            if (user != null) {
                var userDto = _mapper.Map<UserDto>(user);
                return userDto;
            }
            return null;
        }

        public async Task<UserDto> Create (UserInsertDto userInsertDto)
        {
            var user = _mapper.Map<User>(userInsertDto);
            await _commonRepository.Create(user);
            await _commonRepository.Save();
            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }

        public async Task<UserDto> Update (UserUpdateDto userUpdateDto, int id)
        {
            var user = await _commonRepository.GetById(id);
            if (user != null)
            {
                _mapper.Map<UserUpdateDto, User>(userUpdateDto, user);
                _commonRepository.Update(user);
                await _commonRepository.Save();

                var usuaroDto = _mapper.Map<UserDto>(user);

                return usuaroDto;
            }

            return null;
            
        }

        public async Task<UserDto> Delete(int id)
        {
            var user = await _commonRepository.GetById(id);
            if (user != null)
            {
                var userDto = _mapper.Map<UserDto>(user);

                _commonRepository.Delete(user);
                await _commonRepository.Save();
                return userDto;
            }

            return null;
        }

        public bool Validate(UserInsertDto userInsertDto)
        {
            var result = _userInsertValidator.Validate(userInsertDto);
            if (!result.IsValid)
            {
                foreach (var failure in result.Errors)
                {
                    Errors.Add(failure.ErrorMessage);
                }
                return false;
            }
            if(_commonRepository.Search(u => u.Name == userInsertDto.Name).Count() > 0)
            {
                Errors.Add("No puedes tener dos usuarios");
                return false;
            }

            return true;
        }

        public bool Validate(UserUpdateDto userUpdateDto)
        {
            var validationResult = _userUpdateValidator.Validate(userUpdateDto);
            if (!validationResult.IsValid)
            {
                foreach (var failure in validationResult.Errors)
                {
                    Errors.Add(failure.ErrorMessage);
                }
                
            }
            if (_commonRepository.Search(u => u.Name == userUpdateDto.Name 
             && userUpdateDto.Id != u.Id).Count() > 0)
            {
                Errors.Add("No puedes tener dos usuarios");
                return false;
            }
            return true;
        }

    }
}
