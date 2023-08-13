using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ProductsAndCategoriesCore.Models;
using System.ComponentModel;
//Added for Include:
using Microsoft.EntityFrameworkCore;

//ADDED for session check
using Microsoft.AspNetCore.Mvc.Filters;

namespace ProductsAndCategoriesCore.Controllers;




public class CategoryController : Controller
{
    private readonly ILogger<CategoryController> _logger;

    // Add field - adding context into our class // "db" can eb any name
    private MyContext db;

    // update below adding context, etc
    public CategoryController(ILogger<CategoryController> logger, MyContext context)
    {
        _logger = logger;
        db = context;
    }


//! =======================================================================
//! Categories - MyViewModel for Categories (Combines get all categories & New categories)

[HttpGet("/combined/category/home")]    
public IActionResult CombinedCategoryHome()    
{   
    MyViewModel MyModels = new MyViewModel
    {
        AllCategories = MyContext.AllCategories()
        CreateCategory = MyContext.CreateCategory()
    };     
    return View("CombinedCategoryHome",MyModels);    
}
//! =======================================================================
//! Target Category - MyViewModel for target category (Combines basic Products list that connects to target category & Add a Product form)
//TODO: Need a method that adds a selected product to a category that I'm viewing

[HttpGet("/target/{categoryId}")]    
public IActionResult TargetCetegory()    
{   
    MyViewModel MyModels = new MyViewModel
    {
        ViewCategory = MyContext.ViewCategory()
        AllProducts = MyContext.AllProducts()
        AddProductToCategory = MyContext.AddProductToCategory()
    };     
    return View("TargetCategory",MyModels);    
}
//! =======================================================================
//TODO: REMINDER TO COMPLETE CONTROLLER.....
    //  Categories/gets all categories * ============================================
    [HttpGet("/categories")]
    public IActionResult AllCategories()
    {
        List<Category> allCategories = db.Categories.Include(p => p.AssociationLinksProducts).OrderByDescending(d => d.CreatedAt).ToList();
        return View("CombinedCategoryHome", allCategories);
    }


    // New Category  ============================================
    [HttpGet("categories/new")]
    public IActionResult NewCategory()
    {
        return View("CombinedCategoryHome");
    }



    // CreateCategory method ============================================
    [HttpPost("category/create")]
    public IActionResult CreateCategory(Category newCategory)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.allCategories = db.Categories.ToList();
            return View("_NewCategory");
        }

        db.Categories.Add(newCategory);
        db.SaveChanges();
        return RedirectToAction("CombinedCategoryHome");
    }


    // view one Category method ============================================
    [HttpGet("category/viewone/{categoryId}")]
    public IActionResult ViewCategory(int categoryId)
    {
        //Query below:
        Category? category = db.Categories.FirstOrDefault(category => category.CategoryId == categoryId);

        if(category == null) 
        {
            return RedirectToAction("TargetCategory");
        }
        return View("TargetCategory", category );
    }


}

