using System.Collections.Generic;

namespace Ogretmen_Ozel.Models.AccountModels
{
    public class TeacherSignUp
    {
        public User User { get; set; }

        public int Id { get; set; }

        public virtual Address Address { get; set; }
        public List<Subject> Subject { get; internal set; }
    }
}