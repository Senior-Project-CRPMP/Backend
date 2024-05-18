using AutoMapper;
using Backend.Dto.Document;
using Backend.Interfaces.Document;
using Backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers.Document
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IMapper _mapper;

        public DocumentController(IDocumentRepository documentRepository, IMapper mapper)
        {
            _documentRepository = documentRepository;
            _mapper = mapper;
        }

        [HttpGet("EveryDocument")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Models.Document.Document>))]
        public IActionResult GetDocuments()
        {
            var documents = _mapper.Map<List<DocumentDto>>(_documentRepository.GetDocuments());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(documents);
        }

        [HttpGet("SingleDocument/{documentId}")]
        [ProducesResponseType(200, Type = typeof(Models.Document.Document))]
        [ProducesResponseType(400)]
        public IActionResult GetDocument(int documentId)
        {
            if (!_documentRepository.DocumentExists(documentId))
                return NotFound();

            var document = _mapper.Map<DocumentDto>(_documentRepository.GetDocument(documentId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(document);
        }

        [HttpGet("SingleDocumentT/{documentTitle}")]
        [ProducesResponseType(200, Type = typeof(Models.Document.Document))]
        [ProducesResponseType(400)]
        public IActionResult GetDocument(string documentTitle)
        {
            if (!_documentRepository.DocumentExists(documentTitle))
                return NotFound();

            var document = _documentRepository.GetDocument(documentTitle);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(document);
        }

        [HttpPost("CreateDocument")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult CreateDocument([FromBody] DocumentDto documentCreate)
        {
            if (documentCreate == null)
                return BadRequest(ModelState);

            var document = _documentRepository.GetDocuments()
                .Where(p => p.Title.Trim().ToUpper() == documentCreate.Title.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (document != null)
            {
                ModelState.AddModelError("", "Models.Document.Document already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var documentMap = _mapper.Map<Models.Document.Document>(documentCreate);

            if (!_documentRepository.CreateDocument(documentMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Created");
        }

        [HttpPut("UpdateDocument/{documentId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateDocument(int documentId, [FromBody] DocumentDto updatedDocument)
        {
            if (updatedDocument == null)
                return BadRequest(ModelState);

            if (documentId != updatedDocument.Id)
                return BadRequest(ModelState);

            if (!_documentRepository.DocumentExists(documentId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var documentMap = _mapper.Map<Models.Document.Document>(updatedDocument);

            if (!_documentRepository.UpdateDocument(documentMap))
            {
                ModelState.AddModelError("", "Something went wrong updating the category");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("DeleteDocument/{documentId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteDocument(int documentId)
        {
            if (!_documentRepository.DocumentExists(documentId))
                return NotFound();

            var documentToDelete = _documentRepository.GetDocument(documentId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_documentRepository.DeleteDocument(documentToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting document");

            }

            return NoContent();



        }
    }
}
