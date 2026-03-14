using AutoMapper;
using FluentValidation;
using GymApi.DTOs;
using GymApi.Models;
using GymApi.Repository;
using GymApi.Validations;

namespace GymApi.Services
{
    public class WorkoutSetService : ICommonService<WorkoutSetDto, WorkoutSetInsertDto, WorkoutSetUpdateDto>
    {
        private IMapper _mapper;
        private IRepository<WorkoutSet> _commonRepository;
        //private IValidator<WorkoutSetUpdateDto> _workoutSetValidatorUpdate;
        private IValidator<WorkoutSetInsertDto> _workoutSetInsertValidator;

        public WorkoutSetService(IMapper mapper, IRepository<WorkoutSet> repository,
            //IValidator<WorkoutSetUpdateDto> workoutSetValidatorUpdate,
            IValidator<WorkoutSetInsertDto> workoutSetValidatorInsert) 
        { 
            _mapper = mapper;
            _commonRepository = repository;
            //_workoutSetValidatorUpdate = workoutSetValidatorUpdate;
            _workoutSetInsertValidator = workoutSetValidatorInsert;

        }
        public List<string> Errors { get; }

        public async Task<IEnumerable<WorkoutSetDto>> Get()
        {
            var workout = await _commonRepository.Get();
            return workout.Select(w => _mapper.Map<WorkoutSetDto>(w));
        }
            

        public async Task<WorkoutSetDto> GetById(int id)
        {
            var wokoutSet = await _commonRepository.GetById(id);
            if(wokoutSet != null)
            {
                var workoutDto = _mapper.Map<WorkoutSetDto>(wokoutSet);
                return workoutDto;
            }

            return null;
        }
        public async Task<WorkoutSetDto> Create(WorkoutSetInsertDto workoutSetInsertDto)
        {
            if (!Validate(workoutSetInsertDto)) return null;
            var workoutSet = _mapper.Map<WorkoutSet>(workoutSetInsertDto);
            await _commonRepository.Create(workoutSet);
            await _commonRepository.Save();
            var workoutSetDto = _mapper.Map<WorkoutSetDto>(workoutSet);
            return workoutSetDto;
        }
        public async Task<WorkoutSetDto> Update(WorkoutSetUpdateDto workoutSetUpdateDto, int id)
        {
            var workoutSet = await _commonRepository.GetById(id);

            if (workoutSet != null)
            {
                workoutSet = _mapper.Map<WorkoutSetUpdateDto, WorkoutSet>(workoutSetUpdateDto, workoutSet);
                _commonRepository.Update(workoutSet);
                await _commonRepository.Save();

                var workoutSetDto = _mapper.Map<WorkoutSetDto>(workoutSet);
                return workoutSetDto;
            }
            return null;
        }
        public async Task<WorkoutSetDto> Delete(int id)
        {
            var workoutSet = await _commonRepository.GetById(id);

            if (workoutSet != null)
            {
                var workoutSetDto = _mapper.Map<WorkoutSetDto>(workoutSet);
                _commonRepository.Delete(workoutSet);
                return workoutSetDto;
            }

            return null;
        }

        public bool Validate(WorkoutSetUpdateDto dto)
        {
            return true;
        }

        public bool Validate(WorkoutSetInsertDto dto)
        {
            Errors.Clear();
            var validationResult = _workoutSetInsertValidator.Validate(dto);

            if (!validationResult.IsValid)
            {
                foreach (var failure in validationResult.Errors)
                {
                    Errors.Add(failure.ErrorMessage);
                }
                return false;
            }

            // Validación lógica de negocio: No puedes cargar peso negativo
            if (dto.Weight < 0)
            {
                Errors.Add("El peso no puede ser menor a 0.");
                return false;
            }

            return true;
        }
    }
}
