using ChefsDishes.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChefsDishes.Controllers;

public class DishController : Controller
{
    private int? uid 
    {
        get 
        {
            return HttpContext.Session.GetInt32("UUID");
        }
    }
    private CNDContext db;
    public DishController(CNDContext context)
    {
        db = context;
    }

    [HttpGet("/dishes/new")]
    public IActionResult New()
    {
        ViewBag.allChefs = db.Chefs.ToList();
        return View("NewDish");
    }

    [HttpPost("/dishes/create")]
    public IActionResult Create(Dish newDish)
    {
        if(ModelState.IsValid)
        {
        // newDish.ChefId = (int)uid;
        db.Add(newDish);
        db.SaveChanges();

        return RedirectToAction("All");
        }
        ViewBag.allChefs = db.Chefs.ToList();
        return New();
    }

    [HttpGet("/dishes")]
    public IActionResult All()
    {
        List<Dish> allDishes = db.Dishes.Include(p => p.Author).ToList();

        return View("Dishes", allDishes);
    }

    // [HttpGet("/dishes/{oneDishId}")]
    // public IActionResult GetOneDish(int oneDishId)
    // {
    //     Dish? dish = db.Dishes.FirstOrDefault(d => d.DishId == oneDishId); 

    //     if (dish == null)
    //     {
    //         return RedirectToAction("Index");
    //     }

    //     return View("OneDish", dish);
    // }

    // [HttpPost("/dishes/{deletedDishId}/delete")]
    // public IActionResult DeleteDish(int deletedDishId)
    // {
    //     Dish? dish = db.Dishes.FirstOrDefault(d => d.DishId == deletedDishId); 

    //     if (dish != null)
    //     {
    //         db.Dishes.Remove(dish);
    //         db.SaveChanges();
    //     }

    //     return Redirect("/");
    // }

    // [HttpGet("/dishes/{dishId}/edit")]
    // public IActionResult Edit(int dishId)
    // {
    //     Dish? dish = db.Dishes.FirstOrDefault(d => d.DishId == dishId); 

    //     if(dish == null)
    //     {
    //         return Redirect("/");
    //     }
    //     return View("Edit", dish);
    // }

    // [HttpPost("/dishes/{dishId}/update")]
    // public IActionResult Update(Dish editedDish, int dishId)
    // {
    //     if (ModelState.IsValid == false)
    //     {
    //         return Edit(dishId);
    //     }

    //     Dish? dbDish = db.Dishes.FirstOrDefault(d => d.DishId == dishId); 

    //     if (dbDish == null)
    //     {
    //         return Redirect("/");
    //     }

    //     dbDish.Chef = editedDish.Chef;
    //     dbDish.Name = editedDish.Name;
    //     dbDish.Calories = editedDish.Calories;
    //     dbDish.Tastiness = editedDish.Tastiness;
    //     dbDish.Description = editedDish.Description;
    //     dbDish.UpdatedAt = DateTime.Now;

    //     db.Dishes.Update(dbDish);
    //     db.SaveChanges();

    //     return RedirectToAction("GetOneDish", new { dishId = dbDish.DishId });

    // }
}