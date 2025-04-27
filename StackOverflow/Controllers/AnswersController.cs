using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StackOverflow.DTO;
using StackOverflow.Entities;

namespace StackOverflow.Controllers;

[ApiController]
[Route("[controller]")]
public class AnswersController : ControllerBase
{
    private readonly StackOverflowContext _dBContext;
    private readonly IMapper _mapper;

    public AnswersController(StackOverflowContext dBContext, IMapper mapper)
    {
        _dBContext = dBContext;
        _mapper = mapper;
    }

    [HttpGet]
    public IEnumerable<AnswerModel> Get()
    {
        var answers = _dBContext.Answers.ToList();
        return _mapper.Map<IEnumerable<AnswerModel>>(answers);
    }

    [HttpGet("{id}")]
    public ActionResult<AnswerModel> GetById(Guid id)
    {
        var answer = _dBContext.Answers.Find(id);
        if (answer == null)
        {
            return NotFound();
        }
        return _mapper.Map<AnswerModel>(answer);
    }

    [HttpPost]
    public ActionResult<AnswerModel> Create(AnswerModel answerModel)
    {        
        var question = _dBContext.Questions.Find(answerModel.QuestionId);
        if (question == null)
        {
            return NotFound(new ProblemDetails()
            {
                Title = "Question not found",
                Detail = $"Question with ID {answerModel.QuestionId} does not exist.",
                Status = StatusCodes.Status404NotFound
            });
        }

        var user = _dBContext.Users.Find(answerModel.AuthorId);
        if (user == null)
        {
            return NotFound(new ProblemDetails()
            {
                Title = "User not found",
                Detail = $"User with ID {answerModel.AuthorId} does not exist.",
                Status = StatusCodes.Status404NotFound
            });
        }

        var answer = new Answer
        {
            QuestionId = answerModel.QuestionId,
            Content = answerModel.Content,
            AuthorId = answerModel.AuthorId,
            Rate = answerModel.Rate
        };

        _dBContext.Answers.Add(answer);
        _dBContext.SaveChanges();
        return CreatedAtAction(nameof(GetById), new { id = answer.Id }, _mapper.Map<AnswerModel>(answer));
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(Guid id)
    {
        var answer = _dBContext.Answers.Find(id);
        if (answer == null)
        {
            return NotFound();
        }
        _dBContext.Answers.Remove(answer);
        _dBContext.SaveChanges();
        return NoContent();
    }

    [HttpPost("{id}/like")]
    public ActionResult<AnswerModel> Like(Guid id)
    {
        var answer = _dBContext.Answers.Find(id);
        if (answer == null)
        {
            return NotFound();
        }
        answer.Rate += 1;
        _dBContext.SaveChanges();
        return NoContent();
    }

    [HttpPost("{id}/dislike")]
    public ActionResult<AnswerModel> Dislike(Guid id)
    {
        var answer = _dBContext.Answers.Find(id);
        if (answer == null)
        {
            return NotFound();
        }
        answer.Rate -= 1;
        _dBContext.SaveChanges();
        return NoContent();
    }
}
