using Bogus;
using BookDataGeneratorApp.Models;
using Microsoft.AspNetCore.Mvc;
using Task_5.Models;


public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Generate([FromBody] BookRequest request)
    {
        var rng = new Random(request.Seed + request.StartIndex);
        Randomizer.Seed = new Random(request.Seed + request.StartIndex); 

        var faker = new Faker(request.Region);

        var books = new List<BookViewModel>();
        for (int i = 0; i < request.Count; i++)
        {
            int idx = request.StartIndex + i;

            int likes = (int)Math.Floor(request.AvgLikes);
            if (rng.NextDouble() < (request.AvgLikes - likes))
                likes += 1;

            int reviews = (int)Math.Floor(request.AvgReviews);
            if (rng.NextDouble() < (request.AvgReviews - reviews))
                reviews += 1;

            var book = new BookViewModel
            {
                Index = idx,
                ISBN = faker.Random.Replace("###-#-##-######-#"),
                Title = faker.Lorem.Sentence(),
                Authors = new List<string> { faker.Name.FullName() },
                Publisher = faker.Company.CompanyName(),
                Likes = likes,
                Reviews = reviews
            };

            books.Add(book);
        }

        return PartialView("_BookRowPartial", books);
    }


}
