namespace CadastroApi.Models
{
    public class User
    {
        public long Id { get; set; }
        public string? FirstName { get; set; }
        public string? Surname { get; set; }
        public int Age { get; set; } 
        public DateTime CreationDate { get; set; }
    }
}