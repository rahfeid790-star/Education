 using global::Education.Models;
    using Microsoft.EntityFrameworkCore;

    namespace Education.Data
    {
        public class ApplicationDbContext : DbContext
        {
            public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
            {
            }

            public DbSet<School> Schools { get; set; }
            public DbSet<Teacher> Teachers { get; set; }
            public DbSet<Student> Students { get; set; }
            public DbSet<Subject> Subjects { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);

                // Seeding Data (3 Items per Table minimum)
                modelBuilder.Entity<School>().HasData(
                    new School { Id = 1, Name = "Al-Najah School", Address = "Cairo", Phone = "01000000001" },
                    new School { Id = 2, Name = "Al-Amal School", Address = "Giza", Phone = "01000000002" },
                    new School { Id = 3, Name = "Future Leaders School", Address = "Alexandria", Phone = "01000000003" }
                );

                modelBuilder.Entity<Teacher>().HasData(
                    new Teacher { Id = 1, Name = "Ahmed Ali", Email = "ahmed@mail.com", Salary = 8000, SchoolId = 1 },
                    new Teacher { Id = 2, Name = "Mona Hassan", Email = "mona@mail.com", Salary = 8500, SchoolId = 1 },
                    new Teacher { Id = 3, Name = "Khaled Omar", Email = "khaled@mail.com", Salary = 9000, SchoolId = 2 }
                );

                modelBuilder.Entity<Student>().HasData(
                    new Student { Id = 1, Name = "Omar Mohamed", Age = 15, Grade = "10th", SchoolId = 1 },
                    new Student { Id = 2, Name = "Sara Ibrahim", Age = 16, Grade = "11th", SchoolId = 1 },
                    new Student { Id = 3, Name = "Youssef Mahmoud", Age = 15, Grade = "10th", SchoolId = 2 }
                );

                modelBuilder.Entity<Subject>().HasData(
                    new Subject { Id = 1, Title = "Mathematics", Code = "MATH101", CreditHours = 3, TeacherId = 1 },
                    new Subject { Id = 2, Title = "Physics", Code = "PHYS101", CreditHours = 4, TeacherId = 2 },
                    new Subject { Id = 3, Title = "Computer Science", Code = "CS101", CreditHours = 3, TeacherId = 3 }
                );
            }
        }
    }

