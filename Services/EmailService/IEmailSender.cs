
using System.Threading.Tasks;

public interface IEmailSender{
    Task SendEmailAsync(GetEmailDto emailDto );
    
}