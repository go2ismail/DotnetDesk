using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using src.Services;

namespace src.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/UploadFile")]
    public class UploadFileController : Controller
    {
        private readonly Dotnetdesk _dotnetdesk;
        private readonly IHostingEnvironment _env;

        public UploadFileController(Dotnetdesk dotnetdesk,
            IHostingEnvironment env)
        {
            _dotnetdesk = dotnetdesk;
            _env = env;
        }

        [HttpPost]
        [RequestSizeLimit(5000000)]
        public async Task<IActionResult> PostUploadFile(List<IFormFile> files)
        {
            var fileName = await _dotnetdesk.UploadFile(files, _env);
            return Ok(fileName);
        }
    }
}