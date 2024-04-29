using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ogretmen_Ozel.Models
{
    [Table("Teachers")]
    public class Teacher
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Description { get; set; }
        public virtual User User { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual List<Reservation> Reservations { get; set; }


    }
}