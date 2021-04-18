using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecordsAPI.Models;
using RecordsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace RecordsAPI.Controllers
{
    [ApiController]
    [Route("")]
    public class RecordController : ControllerBase
    {

        private readonly ILogger<RecordController> _logger;
        private readonly IRecordsModel model;

        public RecordController(ILogger<RecordController> logger, IRecordsModel model)
        {
            _logger = logger;
            this.model = model;
        }

        [HttpGet("records/gender")]
        public IEnumerable<IRecord> GetByGender()
        {
            return model.RecordSet.ByGender();
        }        
        
        [HttpGet("records/birthday")]
        public IEnumerable<IRecord> GetByBirthdays()
        {
            return model.RecordSet.ByBirthDate();
        }  
        
        [HttpGet("records/name")]
        public IEnumerable<IRecord> GetByName()
        {
            return model.RecordSet.ByLastName();
        }

        [HttpPost("records")]
        public void PostRecord([FromBody] string str)
        {
            
            var added = this.model.AddRecordFromString(str);
            if(added)
            {
                this.HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
            }
            else
            {
                this.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
        }
    }
}
