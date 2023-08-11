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

    // Add field - adding context into our class // "db" can eb any name
    private MyContext db;

    // update below adding context, etc
    public ProductController(ILogger<ProductController> logger, MyContext context)
    {
        _logger = logger;
        db = context;
    }


//! =======================================================================
//! Products - MyViewModel for Produtcs (Combines get all products & New Product)

[HttpGet("/combined/product/home")]    
public IActionResult CombinedProductHome()    
{   
    MyViewModel MyModels = new MyViewModel
    {
        AllProducts = _context.Products.ToList()
    };     
    return View("CombinedProductHome",MyModels);    
}
//! =======================================================================
//! Target Product - MyViewModel for target product (Combines basic categories list that connects to target product & Add a Category form)
//TODO: nested loop needed

[HttpGet("/target/{productId}")]    
public IActionResult TargetProduct()    
{   
    MyViewModel MyModels = new MyViewModel
    {
        AllProducts = _context.Products.ToList()
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

