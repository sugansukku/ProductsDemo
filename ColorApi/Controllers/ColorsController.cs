using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ColorApi.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class ColorsController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Color> Get()
        {
            return new List<Color> { new Color {
                ColourCode = "FFF",
                ColourName = "White",
                ColourId = new Guid("c01941ef-08e6-479e-ae19-ff95d770022c")
            } };
        }
    }
}
