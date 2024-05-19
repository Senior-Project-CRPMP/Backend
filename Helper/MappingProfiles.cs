using AutoMapper;
using Backend.Dto;
using Backend.Models;
using Backend.Models.Form;
using Backend.Dto.Form;
using Backend.Models.Document;
using Backend.Dto.Document;

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

            CreateMap<FormOption, FormOptionDto>();
            CreateMap<FormOptionDto, FormOption>();

            CreateMap<FormFileStorage, FormFileStorageDto>();
            CreateMap<FormFileStorageDto, FormFileStorage>();

            CreateMap<FormResponse, FormResponseDto>();
            CreateMap<FormResponseDto, FormResponse>();

            CreateMap<FormAnswer, FormAnswerDto>();
            CreateMap<FormAnswerDto, FormAnswer>();

            CreateMap<Models.Document.Document, DocumentDto>();
            CreateMap<DocumentDto, Models.Document.Document>();
        }
    }
}
