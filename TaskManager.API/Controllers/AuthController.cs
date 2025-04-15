using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.API.Services;
using TaskManagerApi.Data;

[Route("api/[controller]")]
[ApiController]
public class AuthController : Controller
{
    private readonly AppDbContext _context;
    private readonly JwtService _jwt;

    public AuthController(AppDbContext context, JwtService jwt)
    {
        _context = context;
        _jwt = jwt;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        var user = _context.Users.FirstOrDefault(u => u.userName == request.userName && u.PasswordHash == request.Password);
        if (user == null) return Unauthorized("Invalid credentials");

        var token = _jwt.GenerateToken(user);
        var isSuccess = true;
        return Ok(new { token, isSuccess });
    }

    [HttpGet("users")]
    public async Task<ActionResult> GetUsers()
    {
        var users = await _context.Users
             .Select(u => new
             {
                 u.id,
                 u.userName
             }).ToListAsync();
        return Ok(users);
    }
}

public class LoginRequest
{
    public string userName { get; set; }
    public string Password { get; set; }
}
