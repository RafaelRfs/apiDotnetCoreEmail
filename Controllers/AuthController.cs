
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[ApiController]
[Route("[controller]")]
public class AuthController: ControllerBase
{
    private readonly IAuthRepository authRepository;

    public AuthController(IAuthRepository authRepository)
    {
        this.authRepository = authRepository;
    }
    [HttpPost]
    public async Task<IActionResult> Register(UserRegisterDto request)
    {
        ServiceResponse<int> response = await this.authRepository
        .Register(
            new User{
                Username = request.Username}, request.Password);
        if(!response.Success){
            return BadRequest(response);
        }
        return Ok(response);
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(UserLoginDto request)
    {
        ServiceResponse<string> response = await this.authRepository.Login(
            request.Username, request.Password
        );
        if(!response.Success){
            return BadRequest(response);
        }
        return Ok(response);
    }


    [HttpPost("Token")]
    public async Task<IActionResult> verifyToken(){
        string token = "";
        return Ok(await LoginService.ValidateToken(token));
    }
  
}