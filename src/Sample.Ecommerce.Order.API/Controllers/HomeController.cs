using Microsoft.AspNetCore.Mvc;

namespace Sample.Ecommerce.Order.API.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return Redirect("~/api-docs");
    }
}
