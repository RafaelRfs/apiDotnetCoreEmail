
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[ApiController]
[Route("[controller]")]
public class SendEmailController: ControllerBase
{
    private readonly IEmailSender emailSender;

    public SendEmailController(IEmailSender emailSender){
        this.emailSender = emailSender;
    }

    [HttpPost]
    public async Task<ActionResult>  SendEmail(AddEmailDto emailDto) {
          return  Ok(this.emailSender.SendEmailAsync(emailDto));
    }

}
