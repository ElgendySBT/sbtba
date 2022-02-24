using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SBTBackEnd.Services.FeedbackService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SBTBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IFeedbackService _feedback;

        public MessagesController(IFeedbackService feedback)
        {
            _feedback = feedback;
        }
        
    }
}
