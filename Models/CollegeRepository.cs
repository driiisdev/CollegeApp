namespace CollegeApp.Models
{
    public static class CollegeRepository
    {
         public static List<Student> Students { get; set; } = new List<Student>() {
            new Student
            {
                Id = 1,
                Name = "idris yakub",
                Age = 26,
                Email = "olayinkayakub@yahoo.com",
                Address = "Yaba, Lagos"
            },
            new Student
            {
                Id = 2,
                Name = "olayinka yakub",
                Age = 24,
                Email = "olayinkayakub01@gmail.com",
                Address = "Eti-Osa, Lagos"
            }
         };
    }
}
