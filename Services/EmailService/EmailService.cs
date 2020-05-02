using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

public class EmailService : IEmailService
{
    private readonly IMapper mapper;
      private static List<EmailData> emails = new List<EmailData> {
                new EmailData(),
                new EmailData { id=1, msg =" teste lambda 123"},
                new EmailData { to = "1", msg = "ola mundo Dotnet Core!"}
         };
    

    public EmailService(IMapper mapper)
    {
        this.mapper = mapper;
    }

    public  async Task<ServiceResponse<List<GetEmailDto>>> AddEmailData(AddEmailDto newEmail)
    {
        ServiceResponse<List<GetEmailDto>> serviceResponse = new ServiceResponse<List<GetEmailDto>>();
        EmailData email = mapper.Map<EmailData>(newEmail);
        email.id = emails.Max(c => c.id) + 1;
        emails.Add(email);
        serviceResponse.Data = (emails.Select(c => mapper.Map<GetEmailDto>(c))).ToList();
        //serviceResponse.Data = mapper.Map<List<GetEmailDto>>(emails);
        return  serviceResponse;
    }

    public async Task<ServiceResponse<List<GetEmailDto>>> GetAllEmails()
    {
        ServiceResponse<List<GetEmailDto>> serviceResponse = new ServiceResponse<List<GetEmailDto>>();
        serviceResponse.Data = mapper.Map<List<GetEmailDto>>(emails);
        return serviceResponse;
    }

    public async Task<ServiceResponse<GetEmailDto>> GetEmailDataById(int id)
    {
      ServiceResponse<GetEmailDto> serviceResponse = new ServiceResponse<GetEmailDto>();   
      serviceResponse.Data = mapper.Map<GetEmailDto>(emails.FirstOrDefault(e => e.id == id));
      //serviceResponse.Data = (emails.Select(c => mapper.Map<GetEmailDto>(c))).ToList();
      return serviceResponse;
    }

}