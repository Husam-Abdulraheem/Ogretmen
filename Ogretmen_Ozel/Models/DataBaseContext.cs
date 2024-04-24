using System.Data.Entity;

namespace Ogretmen_Ozel.Models
{
    public class DataBaseContext : DbContext
    {
        public DbSet<User> UserTable { get; set; }
        public DbSet<Address> AddressTable { get; set; }
        public DbSet<Teacher> TeachersTable { get; set; }
        public DbSet<Student> StudentTable { get; set; }
        public DbSet<Subject> SubjectTable { get; set; }
        public DbSet<Reservation> ReservationsTable { get; set; }
    }
}