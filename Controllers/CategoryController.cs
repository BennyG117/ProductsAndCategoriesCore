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
        AllCategories = _context.Categories.ToList()
    };     
    return View("CombinedCategoryHome",MyModels);    
}
//! =======================================================================
//! Target Category - MyViewModel for target category (Combines basic Products list that connects to target category & Add a Product form)
//TODO: nested loop needed

[HttpGet("/target/{categoryId}")]    
public IActionResult TargetCetegory()    
{   
    MyViewModel MyModels = new MyViewModel
    {
        AllCategories = _context.Categories.ToList()
    };     
    return View("TargetCategory",MyModels);    
}
//! =======================================================================

    //  Categories/gets all categories * ============================================
    [HttpGet("/categories")]
    public IActionResult AllCategories()
    {
        List<Category> allCategories = db.Categories.Include(p => p.AssociationLinksProducts).OrderByDescending(d => d.CreatedAt).ToList();
        return View("CombinedProductHome", allProducts);
    }


    // New Product  ============================================
    [HttpGet("products/new")]
    public IActionResult NewProduct()
    {
        return View("CombinedProductHome");
    }



    // CreateProduct method ============================================
    [HttpPost("product/create")]
    public IActionResult CreateProduct(Product newProduct)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.allProducts = db.Products.ToList();
            return View("NewProduct");
        }
        // newDish.DishId = (int) HttpContext.Session.GetInt32("UUID");
        // using db table name "Dishes"
        db.Products.Add(newProduct);
        db.SaveChanges();
        return RedirectToAction("CombinedProductHome");
    }


    // view one Product method ============================================
    [HttpGet("product/viewone/{productId}")]
    public IActionResult ViewProduct(int productId)
    {
        //Query below:
        Product? product = db.Products.FirstOrDefault(product => product.ProductId == productId);

        if(product == null) 
        {
            return RedirectToAction("TargetProduct");
        }
        return View("TargetProduct", product );
    }


}

