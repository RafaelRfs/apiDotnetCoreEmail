using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace apiEmail.Controllers
{
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

        [HttpPost]
        public async Task<IActionResult> AddCharacter(AddEmailDto newEmail) {
           return Ok(await this.emailService.AddEmailData(newEmail));
        }





    }
}