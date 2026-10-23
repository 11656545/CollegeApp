namespace CollegeApp.Models
{
    public static class CollegeRepository
    {
        public static List<Student> Students { get; set;  } = new List<Student>
            {
                new Student {
            Id = 1,
                Name = "John Doe",
                Email = "studnentemail1@gmail.com",
                Address = "123 Main St, City, Country"},
                new Student {
            Id = 2,
                Name = "Jane Smith",
                Email = "studentemail2@gmail.com",
                Address = "456 Oak Ave, City, Country"}
    };
}
}
