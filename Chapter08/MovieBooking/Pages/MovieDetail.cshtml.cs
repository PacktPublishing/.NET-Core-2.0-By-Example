using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MovieBooking.Pages
{
    public class MovieDetailModel : PageModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Poster { get; set; }

        public string Description { get; set; }

        public string ReleaseDate { get; set; }

        public string Cast { get; set; }

        public string Director { get; set; }

        public string ShowTime { get; set; }

        private DataStore store;
        
        public void OnGet(int id)
        {
            this.store = new DataStore();

            var model = this.store.GetMovieById(id);
            if(model != null)
            {
                this.Id = model.Id;
                this.Title = model.Title;
                this.Poster = model.Poster;
                this.Description = model.Description;
                this.ReleaseDate = model.ReleaseDate;
                this.Cast = model.Cast;
                this.Director = model.Director;
                this.ShowTime = model.ShowTime;
            }
        }
    }
}