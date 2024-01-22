using assignemnt_4.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Build.Framework;

public class ApplicationUser : IdentityUser
{
    public string Nickname { get; set; } = string.Empty;

    public ICollection<Blog> posts { get; set; } = new List<Blog>();
}