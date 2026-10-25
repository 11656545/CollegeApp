namespace CollegeApp.MyLogging
{
    public class LogToDB : IMyLogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine("Indexing the log message to the database");
        }

    }
}
