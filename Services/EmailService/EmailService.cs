using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

public class EmailService : IEmailService
{
    private readonly IMapper mapper;
    private readonly DataContext context;
    private static List<EmailData> emails = new List<EmailData> {
                new EmailData(),
                new EmailData { id=1, msg =" teste lambda 123"},
                new EmailData { to = "1", msg = "ola mundo Dotnet Core!"}
         };
    
    public EmailService(IMapper mapper,
                        DataContext context)
    {
        this.mapper = mapper;
        this.context = context;
        context.Database.Migrate();
    }

    public  async Task<ServiceResponse<List<GetEmailDto>>> AddEmailData(AddEmailDto newEmail)
    {
        ServiceResponse<List<GetEmailDto>> serviceResponse = new ServiceResponse<List<GetEmailDto>>();
        EmailData email = mapper.Map<EmailData>(newEmail);
        email.id = 0;
        await context.Emails.AddAsync(email);
        await context.SaveChangesAsync();
        serviceResponse.Data = context.Emails.Select(e => mapper.Map<GetEmailDto>(e)).ToList();
        return  serviceResponse;
    }

    public async Task<ServiceResponse<List<GetEmailDto>>> GetAllEmails()
    {
        ServiceResponse<List<GetEmailDto>> serviceResponse = new ServiceResponse<List<GetEmailDto>>();
        List<EmailData> dbEmails = await context.Emails.ToListAsync();
        serviceResponse.Data = dbEmails.Select(email => mapper.Map<GetEmailDto>(email)).ToList();
        return serviceResponse;
    }

    public async Task<ServiceResponse<GetEmailDto>> GetEmailDataById(int id)
    {
      ServiceResponse<GetEmailDto> serviceResponse = new ServiceResponse<GetEmailDto>();   
      EmailData email = await context.Emails.FirstOrDefaultAsync(e => e.id == id);
      serviceResponse.Data = mapper.Map<GetEmailDto>(email);
      return serviceResponse;
    }

    public async Task<ServiceResponse<GetEmailDto>> UpdateEmail(UpdateEmailDto updatedEmail)
    {
        ServiceResponse<GetEmailDto> serviceResponse = new ServiceResponse<GetEmailDto>();
     try{
       EmailData updtEml = mapper.Map<EmailData>(updatedEmail);
       EmailData email = await context.Emails.FirstOrDefaultAsync(e => e.id == updatedEmail.id);
       email.id = updtEml.id;
       email.adress = updtEml.adress;
       email.from = updtEml.from;
       email.msg =  updtEml.msg;
       email.options =  updtEml.options;
       email.to = updtEml.to;
       context.Emails.Update(email);
       await context.SaveChangesAsync();
       serviceResponse.Data = mapper.Map<GetEmailDto>(email);
       }catch(Exception ex){
            serviceResponse.Success = false;
            serviceResponse.Message =  ex.Message;
        }
       return serviceResponse;
    }

    public async Task<ServiceResponse<List<GetEmailDto>>> DeleteEmailData(int id)
    {
        ServiceResponse<List<GetEmailDto>> serviceResponse = new ServiceResponse<List<GetEmailDto>>();

        try{
            EmailData email = await context.Emails.FirstAsync(e => e.id == id);
            context.Emails.Remove(email);
            await context.SaveChangesAsync();
            serviceResponse.Data =  serviceResponse.Data = context.Emails.Select(email => mapper.Map<GetEmailDto>(email)).ToList();

        }catch(Exception ex){
            serviceResponse.Success = false;
            serviceResponse.Message =  ex.Message;
        }

       return serviceResponse; 
    }
}