using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MovieBooking.Pages
{
    public class BookTicketModel : PageModel
    {
        private readonly DataStore dataStore;

        public BookTicketModel()
        {
            this.dataStore = new DataStore();
        }

        [BindProperty]
        public int NumberOfSeats { get; set; }

        [BindProperty]
        public int SelectedMovieId { get; set; }

        public MovieDetailModel SelectedMovie { get; set; }

        public void OnGet(int id)
        {
            //// HACK: There is a bug in preview that if id is not nullable, it gives an error, hence marking it as nullable, though it is always required.
            this.SelectedMovieId = id;
            this.SelectedMovie = this.dataStore.GetMovieById(this.SelectedMovieId);
        }

        public List<SelectListItem> Timings => this.GetMovieTimings();

        public List<SelectListItem> GetMovieTimings()
        {
            //// We have only one timing, but the code is written to demonstrate how multiple times could have been supported.
            var list = new List<SelectListItem>();
            list.Add(new SelectListItem() { Text = this.SelectedMovie.ShowTime, Value = this.SelectedMovie.ShowTime });
            return list;
        }
        
        public ActionResult OnPost()
        {            
            if (!ModelState.IsValid)
            {
                return Page();
            }
            return RedirectToPage("/BookingSummary", new { id=this.SelectedMovieId, seats = this.NumberOfSeats});
        }
    }
}