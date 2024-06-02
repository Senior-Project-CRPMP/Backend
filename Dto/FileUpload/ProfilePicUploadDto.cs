namespace Backend.Dto.FileUpload
{
    public class ProfilePicUploadDto
    {
        public IFormFile? File { get; set; }
        public int UserId { get; set; }
    }
}
