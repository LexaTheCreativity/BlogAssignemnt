using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using assignemnt_4.Models;
using assignment_4.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;


namespace assignment_4.Controllers;

public class BlogController : Controller
{
     private ApplicationDbContext _db;
     private UserManager<ApplicationUser> _um;
     private RoleManager<IdentityRole> _rm;


    
    // Constructor with db context as a parameter
    public BlogController(ApplicationDbContext db, UserManager<ApplicationUser> um, RoleManager<IdentityRole> rm)
    {
        _db = db;
        _um = um;
        _rm = rm;
        
    }
    
    
    
    // GET
    public async Task<IActionResult> Index()
    {
        var vm = new blogviewmodel
        {
            BlogPosts = _db.Blogs.OrderByDescending(a => a.Id).ToList(),
            ApplicationUsers = _um.Users.OrderByDescending(a => a.Id).ToList()
        };
        return View(vm);

        
        //var blogs = _db.Blog.ToList();
        //return View(blogs);
        //var posts = await _db.Blogs.OrderByDescending(p => p.Time).ToListAsync();
        //return View(posts);
    }
    
    //add
    [Authorize]
    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add([Bind("Id,Title,Summary,Content")] Blog posts)
    {
        var user = _um.GetUserId(User);
        posts.Time = DateTime.Now;

        if (user == null)
        {
            return BadRequest();
        }
        posts.UserId = user;
        
        _db.Add(posts);
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
     
    
    //edit
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        
        if (id == null)
        {
            return NotFound();
        }

        // Hent den innloggede brukerens identitet
        //var user = await _um.GetUserAsync(User);

        // Hent den eksisterende bloggposten basert på ID og brukerens ID
        var editPost = await _db.Blogs.FindAsync(id);

        if (editPost == null)
        {
            return NotFound();
        }

        var userId = _um.GetUserId(User);
        //ViewBag.UserId = userId;

        if (userId != editPost.UserId)
        {
            return RedirectToAction(nameof(Index));
        }

        // Fyll ut edit-skjemaet med eksisterende postens verdier
        return View(editPost);
    }
    
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Blog editPosts)
    {
        
        if (id != editPosts.Id)
        {
            return NotFound();
        }
        
        // Ensure that the UserId is set to the current user's ID
        var user = _um.GetUserAsync(User).Result;
        editPosts.UserId = user.Id;

        
        if (ModelState.IsValid)
        {
            try
            {
                
                _db.Update(editPosts);
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BlogModelExists(editPosts.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        else
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
            return BadRequest(new { Errors = errors });
        }
        
        return View(editPosts);
    }
    
    private bool BlogModelExists(int id)
    {
        return (_db.Blogs?.Any(e => e.Id == id)).GetValueOrDefault();
    }
    
} 



/*// GET: Authors/Create
         public IActionResult Add()
         {
             return View();
         }
 
         // POST: Authors/Create
         // To protect from overposting attacks, enable the specific properties you want to bind to.
         // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
         [HttpPost]
         [ValidateAntiForgeryToken]
         public async Task<IActionResult> Add([Bind("Title,Summary,Content")] Blog posts)
         {
             if (ModelState.IsValid )
             {
                 _db.Add(posts);
                 await _db.SaveChangesAsync();
                 return RedirectToAction(nameof(Index));
             }
             return View(posts);
         }
 
         // GET: Authors/Edit/5
         public async Task<IActionResult> Edit(int? id)
         {
             if (id == null || _db.Blogs == null)
             {
                 return NotFound();
             }
 
             var editposts = await _db.Blogs.FindAsync(id);
             if (editposts == null)
             {
                 return NotFound();
             }
             return View(editposts);
         }
 
         // POST: Authors/Edit/5
         // To protect from overposting attacks, enable the specific properties you want to bind to.
         // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
         [HttpPost]
         [ValidateAntiForgeryToken]
         public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Birthdate")] Blog editPosts)
         {
             if (id != editPosts.Id)
             {
                 return NotFound();
             }
 
             if (ModelState.IsValid)
             {
                 try
                 {
                     _db.Update(editPosts);
                     await _db.SaveChangesAsync();
                 }
                 catch (DbUpdateConcurrencyException)
                 {
                     if (!BlogExists(editPosts.Id))
                     {
                         return NotFound();
                     }
                     else
                     {
                         throw;
                     }
                 }
                 return RedirectToAction(nameof(Index));
             }
             return View(editPosts);
         }
       
 
         private bool BlogExists(int id)
         {
           return (_db.Blogs?.Any(e => e.Id == id)).GetValueOrDefault();
         }
     
     */
     
     