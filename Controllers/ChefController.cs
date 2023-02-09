using ChefsDishes.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace ChefsDishes.Controllers;

public class ChefController : Controller
{
    private CNDContext db;
    public ChefController(CNDContext context)
    {
        db = context;
    }

    [HttpGet("/chefs/new")]
    public IActionResult New()
    {
        return View("NewChef");
    }

    [HttpPost("/chefs/create")]
    public IActionResult Create(Chef newChef)
    {
        if(!ModelState.IsValid)
        {
            return New();
        }
        db.Chefs.Add(newChef);
        db.SaveChanges();

        return Redirect("/");
    }

    [HttpGet("")]
    public IActionResult All()
    {
        List<Chef> allChefs = db.Chefs.Include(d => d.ChefDishes).ToList();

        return View("Index", allChefs);
    }

}