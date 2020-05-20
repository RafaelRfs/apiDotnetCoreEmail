using AutoMapper;

namespace apiEmail
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<EmailData, GetEmailDto>();
            CreateMap<AddEmailDto, EmailData>();  
            CreateMap<UpdateEmailDto, EmailData>(); 
            CreateMap<EmailData,UpdateEmailDto>();       
        }        
    }
}