
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class SendEmailController: ControllerBase
{
    private readonly IEmailSender emailSender;

    public SendEmailController(IEmailSender emailSender){
        this.emailSender = emailSender;
    }

    [HttpPost]
    public async Task<ActionResult>  SendEmail(GetEmailDto emailDto) {
        string header = Request.Headers["Authorization"];
        string token = null;
        
        if(header != null && header.Contains("Bearer")){
            string [] aux = header.Split(" ");
            token = aux.Length > 1 ? aux[1].Trim() : token;
        }

        if(token != null && await LoginService.ValidateToken(token) != null){
          return  Ok(this.emailSender.SendEmailAsync(emailDto));
        } else {
          return BadRequest("Email not sended >> ");
        }
    }


}
