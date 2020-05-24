using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace apiEmail.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService emailService;

        public EmailController(IEmailService emailService)
        {
            this.emailService = emailService;
        }
  
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id){
           return Ok(await this.emailService.GetEmailDataById(id));
        }
        public async Task<ActionResult> GetAll(){
            return Ok(await this.emailService.GetAllEmails());
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> AddEmail(AddEmailDto newEmail) {
            newEmail.id = 0;
           return Ok(await this.emailService.AddEmailData(newEmail));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEmail(UpdateEmailDto updateEmail)
        {
            ServiceResponse<GetEmailDto> serviceResponse = 
            await this.emailService.UpdateEmail(updateEmail);
            if(serviceResponse.Data == null){
                return NotFound(serviceResponse);
            }
            return Ok(serviceResponse);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmail(int id){
            ServiceResponse<List<GetEmailDto>> serviceResponse = await this.emailService.DeleteEmailData(id);
             if (serviceResponse.Data == null) {
                    return NotFound(serviceResponse);
            }
            return Ok(serviceResponse);
        }
        
    }
}