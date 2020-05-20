using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

public class AuthMessageSender : IEmailSender
{
    private GetEmailDto emailDto;
    private string emailSite;
    private string passSite;
    private string primaryDomain;
    private int primaryPort;

    public AuthMessageSender( ){
        this.emailSite = Environment.GetEnvironmentVariable("APP_SITE_EMAIL");
        this.passSite = Environment.GetEnvironmentVariable("APP_SITE_PASS");
        this.primaryDomain = Environment.GetEnvironmentVariable("PRIMARY_DOMAIN");
        this.primaryPort = Int32.Parse(Environment.GetEnvironmentVariable("PRIMARY_PORT"));
    }

    public Task SendEmailAsync(GetEmailDto emailDto )
    {
       this.emailDto = emailDto;
       this.Execute();
       return  Task.FromResult(0);
    }

    public async Task Execute(){
        try{

            MailMessage mail = new MailMessage(){
                From = new MailAddress(this.emailSite, "Site Test RFS ")
            };

            mail.To.Add(new MailAddress(emailDto.to));
            
            if(!string.IsNullOrEmpty(emailDto.ccEmail)){
            mail.CC.Add(new MailAddress(emailDto.ccEmail));
            }

            mail.Subject = emailDto.subject;
            mail.Body = emailDto.msg;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;

            if(!string.IsNullOrEmpty(emailDto.attachment)){
                mail.Attachments.Add(new Attachment(emailDto.attachment));
            }

            Console.Write("Sending new Email to >>> "+emailDto.to);

            using(SmtpClient smtp = new SmtpClient(this.primaryDomain,this.primaryPort)){
                smtp.Credentials = new NetworkCredential(this.emailSite, this.passSite);
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(mail);
            }

        }catch(Exception ex){
            throw ex;
        }
    }

}