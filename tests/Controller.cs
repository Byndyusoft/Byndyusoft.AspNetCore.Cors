using Microsoft.AspNetCore.Mvc;

namespace Byndyusoft.AspNetCore.Cors
{
    [Controller]
    [Route("values")]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public IActionResult Post()
        {
            return Ok();
        }
    }
}