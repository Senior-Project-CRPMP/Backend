﻿namespace Backend.Models.FileUpload
{
    public class FileUpload
    {
        public IFormFile File { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
