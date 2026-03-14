using AutoMapper;
using FluentValidation;
using GymApi.DTOs;
using GymApi.Models;
using GymApi.Repository;
using GymApi.Validations;
using Microsoft.AspNetCore.Http.HttpResults;

namespace GymApi.Services
{
    public class ExerciseService :ICommonService<ExerciseDto, ExerciseInsertDto, ExerciseUpdateDto>
    {
        public List<string> Errors { get; }
        private IRepository<Exercise> _commonRepository;
        private IMapper _mapper;
        private IValidator<ExerciseInsertDto> _exerciseInsertValidator;

        public ExerciseService(IRepository<Exercise> exerciseReposritory, IMapper mapper, 
            IValidator<ExerciseInsertDto> exerciseInsertValidator)
        {
            _commonRepository = exerciseReposritory;
            _mapper = mapper;
            _exerciseInsertValidator = exerciseInsertValidator;
        }
        public async Task<IEnumerable<ExerciseDto>> Get()
        {
            var exercise = await _commonRepository.Get();
            return exercise.Select(e => _mapper.Map<ExerciseDto>(e));
        }

        public async Task<ExerciseDto> GetById(int id)
        {
            var exercise = await _commonRepository.GetById(id);
            if (exercise != null)
            {
                var exerciseDto = _mapper.Map<ExerciseDto>(exercise);
                return exerciseDto;
            }
            return null;

        }
        public async Task<ExerciseDto> Create(ExerciseInsertDto exerciseInsertDto)
        {
            var exercise = _mapper.Map<Exercise>(exerciseInsertDto);
            await _commonRepository.Create(exercise);
            await _commonRepository.Save();
            var exerciseDto = _mapper.Map<ExerciseDto>(exercise);
            return exerciseDto;

        }
        public async Task<ExerciseDto> Update(ExerciseUpdateDto exerciseUpdateDto, int id)
        {
            var exercise = await _commonRepository.GetById(id);
            if (exercise != null)
            {
                exercise = _mapper.Map<ExerciseUpdateDto, Exercise>(exerciseUpdateDto, exercise);
                _commonRepository.Update(exercise);
                await _commonRepository.Save();

                var exerciseDto = _mapper.Map<ExerciseDto>(exerciseUpdateDto);
                return exerciseDto;
            }
            return null;   
        }

        public async Task<ExerciseDto> Delete(int id)
        {
            var exercise = await _commonRepository.GetById(id);
            if (exercise != null)
            {
                var exerciseDto = _mapper.Map<ExerciseDto>(exercise);
                _commonRepository.Delete(exercise);
                await _commonRepository.Save();
            }
            return null;
        }

       

        public bool Validate(ExerciseUpdateDto dto)
        {
            return true;
        }

        public bool Validate(ExerciseInsertDto dto)
        {
            Errors.Clear();
            var validationResult = _exerciseInsertValidator.Validate(dto);

            if (!validationResult.IsValid)
            {
                foreach (var failure in validationResult.Errors)
                {
                    Errors.Add(failure.ErrorMessage);
                }
                return false; // Importante para que no siga
            }

            // Validamos que el nombre sea único
            if (_commonRepository.Search(e => e.Name.ToLower() == dto.Name.ToLower()).Any())
            {
                Errors.Add("Ya existe un ejercicio con ese nombre.");
                return false;
            }

            return true;
        }

        
    }
}
