namespace TaskifyAPI.Managers
{
    using Taskify.Data.Models;
    using TaskifyAPI.Dtos;

    public interface ITaskifyManager
    {
        Task<TaskModel> CreateNewTaskAsync(CreateNewTaskDto dto);
        Task<TaskModel> UpdateTaskAsync(UpdateTaskDto dto);
        Task<TaskModel[]> GetRootTasksAsync();
        Task<TaskModel> SetParentAsync(SetParentTaskDto dto);
        Task<TaskModel> Fetch(TaskKey key);
        Task<TaskDetailsDto> GetTaskDetailsAsync(TaskKey key);
    }
}
