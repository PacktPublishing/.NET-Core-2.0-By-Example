using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MovieBooking.Pages
{
    public class MoviesModel : PageModel
    {
        public IList<MovieDetailModel> Movies { get; private set; } = new List<MovieDetailModel>();

        public void OnGet()
        {
            DataStore store = new DataStore();
            //// Get the below data using EF. Since the app demo would be deployed in Azure and would run 24 hours. SQL is a cost, which I wish to avoid, hence fetching from DataStore.
            this.Movies = store.GetAllMovies();
        }        
    }
}