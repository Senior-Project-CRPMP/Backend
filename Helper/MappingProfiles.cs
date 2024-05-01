using AutoMapper;
using Backend.Dto;
using Backend.Models;

namespace Backend.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        {
            CreateMap<Project, ProjectDto>();
            CreateMap<ProjectDto, Project>();
            CreateMap<Models.Task, TaskDto>();
            CreateMap<TaskDto, Models.Task>();
            CreateMap<Models.Form.Form, Dto.Form.FormDto>();
            CreateMap<Dto.Form.FormDto, Models.Form.Form>();
            CreateMap<Models.Form.FormQuestion, Dto.Form.FormQuestionDto>();
            CreateMap<Dto.Form.FormQuestionDto, Models.Form.FormQuestion>();
        }
    }
}
