using Microsoft.AspNetCore.Mvc;
using StackOverflow.Entities;

namespace StackOverflow.Controllers;

[ApiController]
[Route("[controller]")]
public class CommentsController : ControllerBase
{
    private readonly StackOverflowContext _dBContext;

    public CommentsController(StackOverflowContext dBContext)
    {
        _dBContext = dBContext;
    }

    [HttpGet]
    public IEnumerable<Comment> Get()
    {
        return _dBContext.Comments.ToList();
    }

    [HttpGet("{id}")]
    public ActionResult<Comment> GetById(int id)
    {
        var comment = _dBContext.Comments.Find(id);
        if (comment == null)
        {
            return NotFound();
        }
        return comment;
    }

    [HttpPost]
    public ActionResult<Comment> Create(Comment comment)
    {
        _dBContext.Comments.Add(comment);
        _dBContext.SaveChanges();
        return CreatedAtAction(nameof(GetById), new { id = comment.Id }, comment);
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(Guid id)
    {
        var comment = _dBContext.Comments.Find(id);
        if (comment == null)
        {
            return NotFound();
        }
        _dBContext.Comments.Remove(comment);
        _dBContext.SaveChanges();
        return NoContent();
    }
}
