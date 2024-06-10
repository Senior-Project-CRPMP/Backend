using System.Collections.Generic;

namespace Backend.Dto.Form
{
    public class FormWithResponsesDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int ProjectId { get; set; }
        public List<FormResponseDto>? FormResponses { get; set; }
    }
}
