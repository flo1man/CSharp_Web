namespace Andreys.App.Controllers
{
    using Andreys.Services;
    using SIS.HTTP;
    using SIS.MvcFramework;

    public class HomeController : Controller
    {
        private readonly IProductsService productsService;

        public HomeController(IProductsService productsService)
        {
            this.productsService = productsService;
        }


        [HttpGet("/")]
        public HttpResponse Index()
        {
            if (IsUserLoggedIn())
            {
                return this.Home();
            }

            return this.View();
        }

        public HttpResponse Home()
        {
            if (IsUserLoggedIn())
            {
                var viewModel = this.productsService.GetAll();
                return this.View(viewModel);
            }

            return this.Index();
        }
    }
}
