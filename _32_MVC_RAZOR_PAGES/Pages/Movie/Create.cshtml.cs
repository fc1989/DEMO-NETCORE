using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _32_MVC_RAZOR_PAGES.Views.Movie
{
    public class CreateModel : PageModel
    {
        private readonly IMovieService service;

        public CreateModel(IMovieService service)
        {
            this.service = service;
        }

        [BindProperty]
        public MovieInputModel Movie { get; set; }

        public void OnGet()
        {
            this.Movie = new MovieInputModel();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            var model = new _32_MVC_RAZOR_PAGES.Movie
            {
                Id = this.Movie.Id,
                Title = this.Movie.Title,
                ReleaseYear = this.Movie.ReleaseYear,
                Summary = this.Movie.Summary
            };
            service.AddMovie(model);

            return RedirectToPage("./Index");
        }
    }
}