using GymApi.DTOs;

namespace GymApi.Services
{
    //T -> UserDto, ExerciseDto
    //TI -> Insert
    //TU -> Update
    public interface ICommonService <T, TI, TU>
    {
        public List<string> Errors { get; }
        Task<IEnumerable<T>> Get();

        Task<T> GetById(int id);
        Task<T> Create(TI insertDto);
        Task<T> Update(TU updateDto, int id);
        
        Task<T> Delete(int id);

        bool Validate(TU dto);
        bool Validate(TI dto);


    }

}
