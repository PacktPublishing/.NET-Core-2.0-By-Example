using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MovieBooking.Pages
{
    public class BookingSummaryModel : PageModel
    {
        private readonly DataStore store;

        public BookingSummaryModel()
        {
            this.store = new DataStore();        
        }


        [BindProperty]
        public int MovieId { get; set; }

        public MovieDetailModel Movie { get; set; }

        [BindProperty]
        public int NumberOfSeats { get; set; }
        
        public void OnGet(int id, int seats)
        {
            this.MovieId = id;
            this.NumberOfSeats = seats;
            this.Movie = this.store.GetMovieById(id);
        }

        public ActionResult OnPost()
        {
            if(!this.ModelState.IsValid)
            {
                return Page();
            }


            //// Wire up your payment gateway & ticket issuing code here. We will do a dummy booking and return the congratulations message.

            return this.RedirectToPage("/Congratulations");
        }
    }
}