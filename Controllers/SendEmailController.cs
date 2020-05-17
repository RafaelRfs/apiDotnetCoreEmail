
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
          return  Ok(this.emailSender.SendEmailAsync(emailDto));
    }


}