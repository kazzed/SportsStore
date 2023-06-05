using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Controllers;

public class HomeController : Controller
{
    public int PageSize = 4;
    
    private IStoreRepository repository;

    public HomeController(IStoreRepository repository)
        => this.repository = repository;

    public ViewResult Index(int productPage = 1)
        => View(repository.Products
        .OrderBy(p => p.ProductID)
        .Skip((productPage - 1) * PageSize)
        .Take(PageSize));
}
