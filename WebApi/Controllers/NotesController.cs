using DatabaseModels;
using WebApi.Data;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class NotesController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public NotesController(ApplicationDbContext db) =>
            this.dbContext = db ?? throw new ArgumentNullException(nameof(db));

        [HttpGet]
        public async Task<ActionResult<List<NoteItem>>> ListNotes()
        {
            try
            {
                List<NoteItem> notes = await this.dbContext.Notes.ToListAsync();
                if (notes.Count == 0)
                    return NotFound("No notes were found.");
                return new OkObjectResult(notes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult<NoteItem>> AddNote(NoteItem note)
        {
            NoteItem newNote = new NoteItem()
            {
                Title = note.Title,
                Note = note.Note,
                Author = note.Author,
                Date = note.Date,
                TagList = note.TagList
            };

            await this.dbContext.Notes.AddAsync(newNote);
            await this.dbContext.SaveChangesAsync();

            return new OkObjectResult(newNote);
        }
    }
}
