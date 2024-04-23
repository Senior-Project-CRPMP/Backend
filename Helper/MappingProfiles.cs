using AutoMapper;
using Backend.Dto;
using Backend.Models;

namespace Backend.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        {
            CreateMap<ProjectModel, ProjectDto>();
            CreateMap<ProjectDto, ProjectModel>();
            CreateMap<TaskModel, TaskDto>();
            CreateMap<TaskDto, TaskModel>();
        }
    }
}
