using CollegeApp.MyLogging;
using Microsoft.AspNetCore.Mvc;

namespace CollegeApp.Controllers
{
    [Route("Demo")]
    [ApiController]
    public class Demo : Controller
    {
        private readonly IMyLogger _mylogger;

        //Loosely coupled mechanism/ Dependency Injection (DI) is used here to inject the logger dependency into the controller.
        //Whenever we need an object for different class we will get it object from program.cs file and inject it into the controller class. This is called dependency injection.
        public Demo(IMyLogger myLogger)
        {
            _mylogger = myLogger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            _mylogger.Log("Demo index page accessed from logtoDB");
            return Ok();
        }
    }
}
