
using assignemnt_4.Models;
using Microsoft.AspNetCore.Identity;


namespace assignment_4.Data
{
    public class ApplicationDbInitializer
    {
        public static void Initialize(ApplicationDbContext db, UserManager<ApplicationUser> um, RoleManager<IdentityRole> rm)
        {
            // Recreate DB during development
            db.Database.EnsureDeleted();           
            db.Database.EnsureCreated();           
            
            // Create roles
            var adminRole = new IdentityRole("Admin");           
            rm.CreateAsync(adminRole).Wait();           
            
            // Create users
            var admin = new ApplicationUser { UserName = "admin@uia.no", Email = "admin@uia.no" , Nickname = "AdminUser", EmailConfirmed = true};           
            um.CreateAsync(admin, "Password1.").Wait(); 
            um.AddToRoleAsync(admin, "Admin").Wait();    
            
            
            // Finally save changes to the database
            db.SaveChanges();

    
        }
    }
}

