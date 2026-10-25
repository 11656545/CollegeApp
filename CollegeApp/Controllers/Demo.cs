using CollegeApp.MyLogging;
using Microsoft.AspNetCore.Mvc;

namespace CollegeApp.Controllers
{
    [Route("Demo")]
    [ApiController]
    public class Demo : Controller
    {
        private readonly IMyLogger _mylogger;

        public Demo()
        {
            //Strongly coupled mechanism, we are creating the object of LogToDB class here, so if we want to change the logging mechanism, we have to change the code here.
            _mylogger = new LogToDB();
            //If I want to change the logging mechanism to LogToFile, I have to change the code here as below commented, which is not a good practice.
            // _mylogger = new LogToFile();
            //_mylogger = new LogToServerMemory();
            //_mylogger = new LogToConsole();
        }

        [HttpGet]
        public IActionResult Index()
        {
            _mylogger.Log("Demo index page accessed from logtoDB");
            return Ok();
        }
    }
}
