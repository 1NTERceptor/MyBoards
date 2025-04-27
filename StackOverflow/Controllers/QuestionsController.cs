using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StackOverflow.DTO;
using StackOverflow.Entities;

namespace StackOverflow.Controllers;

[ApiController]
[Route("[controller]")]
public class QuestionsController : ControllerBase
{
    private readonly StackOverflowContext _dBContext;
    private readonly IMapper _mapper;

    public QuestionsController(StackOverflowContext dBContext, IMapper mapper)
    {
        _dBContext = dBContext;
        _mapper = mapper;
    }

    [HttpGet]
    public IEnumerable<QuestionModel> Get()
    {
        var list = _dBContext.Questions.ToList();

        return _mapper.Map<IEnumerable<QuestionModel>>(list);
    }

    [HttpGet("{id}")]
    public ActionResult<QuestionModel> GetById(int id)
    {
        var question = _dBContext.Questions.Find(id);

        if (question == null)
        {
            return NotFound(new ProblemDetails()
            {
                Title = "Question not found",
                Detail = $"Question with ID {id} does not exist.",
                Status = StatusCodes.Status404NotFound
            });
        }

        return _mapper.Map<QuestionModel>(question);
    }

    [HttpPost]
    public ActionResult<QuestionModel> Create(QuestionModel questionModel)
    {
        var user = _dBContext.Users.Where(u => u.UserName == questionModel.UserName).FirstOrDefault();

        if (user == null)
        {
            return NotFound(new ProblemDetails()
            {
                Title = "User not found",
                Detail = $"User with username {questionModel.UserName} does not exist.",
                Status = StatusCodes.Status404NotFound
            });
        }

        var question = new Question
        {
            Title = questionModel.Title,
            Content = questionModel.Content,
            Author = user,
            Tags = new List<Tag>()
        };

        _dBContext.Questions.Add(question);
        _dBContext.SaveChanges();
        return CreatedAtAction(nameof(GetById), new { id = question.Id }, _mapper.Map<QuestionModel>(question));
    }

    [HttpPut("{questionId}/AddTag/{tagId}")]
    public ActionResult<QuestionModel> Update(Guid questionId, Guid tagId)
    {
        var question = _dBContext.Questions.Find(questionId);
        var tag = _dBContext.Tags.Find(tagId);

        if (question == null)
        {
            return NotFound(new ProblemDetails()
            {
                Title = "Question not found",
                Detail = $"Question with ID {questionId} does not exist.",
                Status = StatusCodes.Status404NotFound
            });
        }

        if (tag == null)
        {
            return NotFound(new ProblemDetails()
            {
                Title = "Tag not found",
                Detail = $"Tag with ID {tagId} does not exist.",
                Status = StatusCodes.Status404NotFound
            });
        }

        question.Tags.Add(tag);

        _dBContext.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(Guid id)
    {
        var question = _dBContext.Questions.Find(id);
        if (question == null)
        {
            return NotFound();
        }
        _dBContext.Questions.Remove(question);
        _dBContext.SaveChanges();
        return NoContent();
    }
}
