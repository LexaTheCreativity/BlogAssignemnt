using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace assignemnt_4.Models
{
    
    public class Blog
    {
        
        public Blog(){}

        /*public Blog(string nickname, string title, string summary, string content, DateTime time, ApplicationUser user)
        {
            Nickname = nickname;
            Title = title;
            Summary = summary;
            Content = content;
            Time = time;
            User = user;
        }*/
        
        public int Id { get; set; }
        
        public string UserId { get; set; }
        
        public ApplicationUser? User { get; set; }
        [Required] [DisplayName("Title")] public string Title { get; set; } = string.Empty;
        [Required] [DisplayName("Summary")] public string Summary { get; set; } = string.Empty;
        
        [Required] [DisplayName("Content")] public string Content { get; set; } = string.Empty;
        
        [Required] [DisplayName("Date")] public DateTime Time { get; set; } 
        
       
        // Define a foreign key relationship to ApplicationUser
        
        


    }

}

/*
public class Author
{
    public Author() {}
        
    public Author(string firstName, string lastName, DateTime birthdate)
    {
        FirstName = firstName;
        LastName = lastName;
        Birthdate = birthdate;
    }

    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    [DisplayName("First name")]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    [DisplayName("Last name")]
    public string LastName { get; set; } = string.Empty;

    [DataType(DataType.Date)]
    [DisplayName("Birthdate")]
    public DateTime Birthdate { get; set; }

    public ICollection<Book> Books { get; set; } = new List<Book>();
}*/