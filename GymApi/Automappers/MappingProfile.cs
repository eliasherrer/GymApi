using AutoMapper;
using GymApi.DTOs;
using GymApi.Models;

namespace GymApi.Automappers
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            CreateMap<UserInsertDto, User>();
            CreateMap<User, UserDto>();
            CreateMap<UserUpdateDto, User>();

            CreateMap<ExerciseInsertDto, Exercise>();
            CreateMap<Exercise, ExerciseDto>();
            CreateMap<ExerciseUpdateDto, Exercise>();

            CreateMap<WorkoutInsertDto,Workout>();
            CreateMap<Workout,WorkoutDto>();
            CreateMap<WorkoutUpdateDto,Workout>();

            CreateMap<WorkoutSetInsertDto, WorkoutSet>();
            CreateMap<WorkoutSet, WorkoutSetDto>();
            CreateMap<WorkoutSetUpdateDto, WorkoutSet>();

        }
    }
}
