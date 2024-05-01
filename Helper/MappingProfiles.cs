using AutoMapper;
using Backend.Dto;
using Backend.Models;
using Backend.Models.Form;
using Backend.Dto.Form;

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
            CreateMap<FormQuestion, FormQuestionDto>();
            CreateMap<FormQuestionDto, FormQuestion>();
            CreateMap<FormLinkQuestion, FormLinkQuestionDto>();
            CreateMap<FormLinkQuestionDto, FormLinkQuestion>();
            CreateMap<FormOption, FormOptionDto>();
            CreateMap<FormOptionDto, FormOption>();
        }
    }
}
