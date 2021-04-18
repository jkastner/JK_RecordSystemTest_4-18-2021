using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecordsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecordsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecordController : ControllerBase
    {

        private readonly ILogger<RecordController> _logger;

        public RecordController(ILogger<RecordController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Record> Get()
        {
            return null;
        }
    }
}
