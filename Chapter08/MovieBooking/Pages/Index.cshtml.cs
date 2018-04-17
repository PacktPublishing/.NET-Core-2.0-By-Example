using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MovieBooking.Pages
{
    public class IndexModel : PageModel
    {
        private readonly DataStore store;

        public List<MovieDetailModel> Movies { get; set; }


        [BindProperty]
        public int SelectedMovieId { get; set; }


        public IndexModel()
        {
            this.store = new DataStore();
        }



        public void OnGet()
        {
            this.Movies = this.store.GetAllMovies();
        }

        public ActionResult OnPost()
        {
            if(!this.ModelState.IsValid)
            {
                return this.Page();
            }

            return this.RedirectToPage("./BookTicket", new { id = this.SelectedMovieId });
        }
    }
}
