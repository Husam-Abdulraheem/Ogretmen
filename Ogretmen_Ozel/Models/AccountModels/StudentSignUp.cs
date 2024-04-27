namespace Ogretmen_Ozel.Models.AccountModels
{
    public class StudentSignUp
    {
        public User User { get; set; }
        public virtual Address Address { get; set; }
    }
}