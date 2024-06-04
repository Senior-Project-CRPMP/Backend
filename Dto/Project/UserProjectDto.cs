namespace Backend.Dto.Project
{
    public class UserProjectDto
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string? UserId { get; set; }
        public string? Role { get; set; }
    }
}
