namespace Backend.Models.FileUpload
{
    public class ProfilePicUpload
    {
        public int Id { get; set; }
        public string FilePath { get; set; }
        public int UserId { get; set; }
    }
}
