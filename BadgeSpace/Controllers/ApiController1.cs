using Microsoft.AspNetCore.Mvc;

namespace BadgeSpace.Controllers
{
    public class ApiController1 : Controller
    {
        [Controller]
        [Route("[Controller]")]
        public class ApiController
        {
            public string oi()
            {
                return "Hello World";
            }
        }
    }
}
