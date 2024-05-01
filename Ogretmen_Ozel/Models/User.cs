using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ogretmen_Ozel.Models
{
    [Table("Users")]
    public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(30), Required]
        public string Name { get; set; }

        [StringLength(40), Required]
        public string LastName { get; set; }

        [StringLength(30), Required, Index(IsUnique = true)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [StringLength(30), Required]
        public string Phone { get; set; }

        [Required]
        public bool IsTeacher { get; set; }

        public virtual Address Address { get; set; }


    }
}