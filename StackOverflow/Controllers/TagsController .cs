using Microsoft.AspNetCore.Mvc;
using StackOverflow.Entities;

namespace StackOverflow.Controllers;

[ApiController]
[Route("[controller]")]
public class TagsController : ControllerBase
{
    private readonly StackOverflowContext _dBContext;

    public TagsController(StackOverflowContext dBContext)
    {
        _dBContext = dBContext;
    }

    [HttpGet]
    public IEnumerable<Tag> Get()
    {
        return _dBContext.Tags.ToList();
    }

    [HttpGet("{id}")]
    public ActionResult<Tag> GetById(int id)
    {
        var tag = _dBContext.Tags.Find(id);
        if (tag == null)
        {
            return NotFound();
        }
        return tag;
    }

    [HttpPost]
    public ActionResult<Tag> Create(Tag tag)
    {
        _dBContext.Tags.Add(tag);
        _dBContext.SaveChanges();
        return CreatedAtAction(nameof(GetById), new { id = tag.Id }, tag);
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(Guid id)
    {
        var tag = _dBContext.Tags.Find(id);
        if (tag == null)
        {
            return NotFound();
        }
        _dBContext.Tags.Remove(tag);
        _dBContext.SaveChanges();
        return NoContent();
    }
}
