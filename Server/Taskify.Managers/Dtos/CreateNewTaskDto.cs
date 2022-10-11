namespace TaskifyAPI.Dtos
{
    public record CreateNewTaskDto(string Title, string Description, Guid? ParentId)
    {
        public string? CreatedBy { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime CreationDate { get; set; }
    };
}
