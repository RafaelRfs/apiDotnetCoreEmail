
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IEmailService{
    Task<ServiceResponse<List<GetEmailDto>>> GetAllEmails();
    Task<ServiceResponse<GetEmailDto>> GetEmailDataById(int id);
    Task<ServiceResponse<List<GetEmailDto>>> AddEmailData(AddEmailDto emailData);
    Task<ServiceResponse<GetEmailDto>> UpdateEmail(UpdateEmailDto updatedEmail);
    Task<ServiceResponse<List<GetEmailDto>>> DeleteEmailData(int id);

}