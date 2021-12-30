using System;

namespace AppCadastroX.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
