using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace Ogretmen_Ozel.Models
{
    [Table("Subjects")]
    public class Subject
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string SubjectName { get; set; }
        [MaxLength(300)]
        public string SubjectDescription { get; set; }

        public string Image { get; set; }
        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }
    }
}