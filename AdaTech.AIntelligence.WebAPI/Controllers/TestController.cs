using AdaTech.AIntelligence.Service.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace AdaTech.AIntelligence.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public ActionResult Get()
        {
           throw new NotReadableImageException("A imagem não pode ser lida");
        }

    }
}
