using AutoMapper;
using TaskManager.Contracts;
using Task = TaskManager.Models.Task;

namespace TaskManager.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region Tasks

            CreateMap<CreateTaskRequest, Task>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

            CreateMap<UpdateTaskRequest, Task>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

            #endregion

            #region TaskTypes

            CreateMap<CreateTaskTypeRequest, Task>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<UpdateTaskTypeRequest, Task>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            #endregion
        }
    }
}
