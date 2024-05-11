using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ogretmen_Ozel.Models
{
    [Table("Reservations")]
    public class Reservation
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        //public Date Date { get; set; }
        public DateTime Time { get; set; }
        public virtual Teacher Teacher { get; set; }
        public virtual Student Student { get; set; }
        public virtual Subject Subject { get; set; }
        public string Status { get; set; }
        //public static implicit operator List<object>(Reservation v)
        //{
        //    throw new NotImplementedException();
        //}
    }
}