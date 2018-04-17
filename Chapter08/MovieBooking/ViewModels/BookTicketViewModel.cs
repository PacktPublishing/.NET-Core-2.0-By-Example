using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MovieBooking.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieBooking.ViewModels
{
    public class BookTicketViewModel :PageModel
    {
        private readonly DataStore dataStore;

        public int selectedMovieId;

        public BookTicketViewModel()
        {
            this.dataStore = new DataStore();
        }

        public void OnGet(int? id)
        {
            this.selectedMovieId = id ?? 0;
        }


        public List<MovieDetailModel> Movies => this.dataStore.GetAllMovies();

        public string SelectedMovie => this.GetMovieTitle(this.selectedMovieId);

        public string SelectedMoviePoster => this.GetMoviePoster(this.selectedMovieId);

        public string SelectedMovieDescription => this.GetMovieDescription(this.selectedMovieId);

        public string MovieTimings => Timings.ToString();

        public List<SelectListItem> Timings => this.GetMovieTimings(this.selectedMovieId);

        public List<SelectListItem> GetMovieTitles()
        {
            var list = new List<SelectListItem>();
            foreach (var item in this.Movies)
            {
                list.Add(new SelectListItem() { Text = item.Title, Value = item.Id.ToString() });
            }

            return list;
        }

        public string GetMovieTitle (int Id)
        {
            return this.Movies.FirstOrDefault(j => j.Id == Id)?.Title;
        }
        public string GetMoviePoster(int Id)
        {
            return this.Movies.FirstOrDefault(j => j.Id == Id)?.Poster;
        }

        public string GetMovieDescription(int Id)
        {
            return this.Movies.FirstOrDefault(j => j.Id == Id)?.Description;
        }

        public List<SelectListItem> GetMovieTimings(int id)
        {
            var list = new List<SelectListItem>();
            foreach (var item in this.Movies.Where(j=>j.Id == id))
            {
                list.Add(new SelectListItem() { Text = item.ShowTime, Value = item.ShowTime });
            }

            return list;
        }

        
        

    }
}
