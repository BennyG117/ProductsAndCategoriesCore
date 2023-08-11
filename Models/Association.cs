#pragma warning disable CS8618

// using System.
using System.ComponentModel.DataAnnotations;
namespace ProductsAndCategoriesCore.Models;


//!association model or association table // connecting our user and post model togethere*
public class Association 
{
[Key]
    //int AssociationId
    public int AssociationId {get; set;}

    //int ProductId
    public int ProductId {get; set;}
    
    //linking Product
    public Product? Product {get; set;}

    //linking CategoryId
    public int CategoryId {get; set;}

    // linking Category
    public Category? Category {get; set;}
    

        // CreatedAt ======================== 
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    


    // UpdatedAt ======================== 
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

}