using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ogretmen_Ozel.Models
{
    [Table("Students")]
    public class Student
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudentID { get; set; }

        public User User { get; set; }
        public virtual List<Reservation> reservation { get; set; }
    }
}