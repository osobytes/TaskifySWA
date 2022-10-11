namespace Taskify.AzureTables
{
    using AutoMapper;
    using Taskify.AzureTables.Entities;
    using Taskify.Data.Models;

    internal class EntityMapperProfile : Profile
    {
        public EntityMapperProfile()
        {
            CreateMap<TaskEntity, TaskModel>()
                .ForMember(nameof(TaskModel.ParentTask), opt => 
                opt.MapFrom(s => s.PartitionKey != "root" ? s.PartitionKey : null))
                .ForMember(nameof(TaskModel.Id), opt => opt.MapFrom(s => s.RowKey))
                .ReverseMap()
                .ForMember(nameof(TaskEntity.PartitionKey), 
                opt => opt.MapFrom(s => s.ParentTask.HasValue ? s.ParentTask.Value.ToString() : "root"))
                .ForMember(nameof(TaskEntity.RowKey), opt => opt.MapFrom(s => s.Id.ToString()));
        }
    }
}
