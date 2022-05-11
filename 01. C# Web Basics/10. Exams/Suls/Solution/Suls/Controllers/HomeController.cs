using Suls.Services;
using SUS.HTTP;
using SUS.MvcFramework;

namespace Suls.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProblemsService problemsService;

        public HomeController(IProblemsService problemsService)
        {
            this.problemsService = problemsService;
        }

        [HttpGet("/")]
        public HttpResponse Index()
        {
            if (IsUserSignedIn())
            {
                return this.Redirect("/Home/IndexLoggedIn");
            }

            return View();
        }

        public HttpResponse IndexLoggedIn()
        {
            var models = problemsService.GetAll();

            return View(models);
        }
    }
}
