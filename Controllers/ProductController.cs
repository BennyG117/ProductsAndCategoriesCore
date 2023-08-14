using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ProductsAndCategoriesCore.Models;
using System.ComponentModel;
//Added for Include:
using Microsoft.EntityFrameworkCore;

//ADDED for session check
using Microsoft.AspNetCore.Mvc.Filters;

namespace ProductsAndCategoriesCore.Controllers;




public class ProductController : Controller
{
    private readonly ILogger<ProductController> _logger;

    // Add field - adding context into our class // "db" can be any name
    private MyContext db;

    // update below adding context, etc
    public ProductController(ILogger<ProductController> logger, MyContext context)
    {
        _logger = logger;
        db = context;
    }


//! =======================================================================
//! Products - MyViewModel for Produtcs (Combines get all products & New Product)
//Goal: to use two partials to create a secondary home page that displays a form to add a new product to the db and a form to view all products in the list. This list of all products will be a list of clickable links

[HttpGet("/combined/product/home")]    
public IActionResult CombinedProductHome()    
{   
    MyViewModel MyModels = new MyViewModel
    {
        AllProducts = MyContext.AllProducts()
        CreateProduct = MyContext.CreateProduct()
    };     
    return View("CombinedProductHome",MyModels);    
}
//! =======================================================================
//! Target Product - MyViewModel for target product (Combines basic categories list that connects to target product & Add a Category form)
//Goal:to use two partials to create a view page called "TargetProduct.cshtml". This page will have the target Product listed at the top, then two partials below it. One part will have a list of all the categories that pertain to that target Product, while the other will have dropdown select form to add other categories to the list of linked products to the Product at the top of the page.


[HttpGet("/target/{productId}")]    
public IActionResult TargetProduct()    
{   
    MyViewModel MyModels = new MyViewModel
    {
        ViewProduct = MyContext.ViewProduct()
        AllCategories = MyContext.AllCategories()
        AddCategoryToProduct = MyContext.AddCategoryToProduct()
    };     
    return View("TargetProduct",MyModels);    
}
//! =======================================================================

    //  Products/gets all products * ============================================
    [HttpGet("/products")]
    public IActionResult AllProducts()
    {
        List<Product> allProducts = db.Products.Include(p => p.AssociationLinksCategories).OrderByDescending(d => d.CreatedAt).ToList();
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
            return View("_NewProduct");
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

