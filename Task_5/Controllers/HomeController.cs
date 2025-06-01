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
        Randomizer.Seed = rng;

        var faker = new Faker(request.Region);
        var books = new List<BookViewModel>();

        for (int i = 0; i < request.Count; i++)
        {
            int idx = request.StartIndex + i;

            int likes = (int)Math.Floor(request.AvgLikes);
            if (faker.Random.Double() < (request.AvgLikes - likes))
                likes++;

            int reviewsCount = (int)Math.Floor(request.AvgReviews);
            if (faker.Random.Double() < (request.AvgReviews - reviewsCount))
                reviewsCount++;

            var reviewFaker = new Faker<Review>(request.Region)
                .RuleFor(r => r.Text, f => f.Commerce.ProductDescription())
                .RuleFor(r => r.Reviewer, f => f.Name.FullName());

            var generatedReviews = reviewFaker.Generate(reviewsCount);

            var book = new BookViewModel
            {
                Index = idx,
                ISBN = faker.Random.Replace("###-#-##-######-#"),
                Title = faker.Lorem.Sentence(3, 2),
                Authors = new List<string> { faker.Name.FullName() },
                Publisher = faker.Company.CompanyName(),
                Likes = likes,
                ReviewList = generatedReviews,
                Reviews = generatedReviews.Count
            };


            books.Add(book);
        }

        return PartialView("_BookRowPartial", books);
    }
}
