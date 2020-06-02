
using System.Threading.Tasks;

public interface IEmailSender{
    Task SendEmailAsync(AddEmailDto emailDto );
    
}