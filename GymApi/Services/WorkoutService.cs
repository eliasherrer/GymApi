using AutoMapper;
using FluentValidation;
using GymApi.DTOs;
using GymApi.Models;
using GymApi.Repository;
using GymApi.Validations;

namespace GymApi.Services
{
    public class WorkoutService : ICommonService<WorkoutDto, WorkoutInsertDto, WorkoutUpdateDto>
    {
        private IMapper _mapper;
        private IRepository<Workout> _commonRepository;
        private IValidator<WorkoutInsertDto> _workoutInsertValidator;

        public List<string> Errors { get; }

        public WorkoutService(IMapper mapper, IRepository<Workout> commonRepository,
            IValidator<WorkoutInsertDto> workoutInsertValidator) 
        {
            _mapper = mapper;
            _commonRepository = commonRepository;
            _workoutInsertValidator = workoutInsertValidator;
            //_workoutUpdateValidator = workoutUpdatevalidator;
            Errors = new List<string>();
        }

        public async Task<IEnumerable<WorkoutDto>> Get()
        {
            var workout = await _commonRepository.Get();
            return workout.Select(w => _mapper.Map<WorkoutDto>(w));
            
        }

        public async Task<WorkoutDto> GetById(int id)
        {
            var workout = await _commonRepository.GetById(id);
            if (workout != null)
            {
                var workoutDto = _mapper.Map<WorkoutDto>(workout);
                return workoutDto;
            }
            return null;
           

        }
        public async Task<WorkoutDto> Create(WorkoutInsertDto workoutInsertDto)
        {
            if (!Validate(workoutInsertDto)) return null;
            var workout = _mapper.Map<Workout>(workoutInsertDto);
            await _commonRepository.Create(workout);
            await _commonRepository.Save();
            var workoutDto = _mapper.Map<WorkoutDto>(workout);
            return workoutDto;
        }

        public async Task<WorkoutDto> Update(WorkoutUpdateDto workoutUpdateDto, int id)
        {
            var workout = await _commonRepository.GetById(id);

            if (workout != null)
            {
                workout = _mapper.Map<WorkoutUpdateDto,Workout>(workoutUpdateDto,workout);
                _commonRepository.Update(workout);
                await _commonRepository.Save();

                var workoutDto = _mapper.Map<WorkoutDto>(workout);
                return workoutDto;
            }
            return null;
        }


        public async Task<WorkoutDto> Delete(int id)
        {
            var workout = await _commonRepository.GetById(id);

            if (workout != null)
            {
                var workoutDto = _mapper.Map<WorkoutDto>(workout);
                _commonRepository.Delete(workout);
                return workoutDto;
            }

            return null;

        }
       
        public bool Validate(WorkoutUpdateDto dto)
        {
            return true;
        }

        public bool Validate(WorkoutInsertDto dto)
        {
            Errors.Clear();
            var validationResult = _workoutInsertValidator.Validate(dto);

            if (!validationResult.IsValid)
            {
                foreach (var failure in validationResult.Errors)
                {
                    Errors.Add(failure.ErrorMessage);
                }
                return false;
            }

            return true;
        }
    }
}
