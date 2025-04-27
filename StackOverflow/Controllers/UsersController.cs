using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StackOverflow.DTO;
using StackOverflow.Entities;

namespace StackOverflow.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly StackOverflowContext _dBContext;
    private readonly IMapper _mapper;

    public UsersController(StackOverflowContext dBContext, IMapper mapper)
    {
        _dBContext = dBContext;
        _mapper = mapper;
    }

    [HttpGet]
    public IEnumerable<UserModel> Get()
    {
        var users = _dBContext.Users.ToList();

        return _mapper.Map<IEnumerable<UserModel>>(users);
    }

    [HttpGet("{id}")]
    public ActionResult<UserModel> GetById(int id)
    {
        var user = _dBContext.Users.Find(id);
        if (user == null)
        {
            return NotFound();
        }
        return _mapper.Map<UserModel>(user);
    }

    [HttpPost]
    public ActionResult<UserModel> Create(UserModel userModel)
    {
        var user = new User
        {
            UserName = userModel.UserName,
            Email = userModel.Email,
        };
        _dBContext.Users.Add(user);
        _dBContext.SaveChanges();
        return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(Guid id)
    {
        var user = _dBContext.Users.Find(id);
        if (user == null)
        {
            return NotFound();
        }
        _dBContext.Users.Remove(user);
        _dBContext.SaveChanges();
        return NoContent();
    }
}
