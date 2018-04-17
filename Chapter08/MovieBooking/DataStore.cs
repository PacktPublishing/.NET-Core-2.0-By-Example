using MovieBooking.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieBooking
{
    public class DataStore
    {
        public List<MovieDetailModel> GetAllMovies()
        {
            List<MovieDetailModel> moviesList = new List<MovieDetailModel>();
            MovieDetailModel movie1 = new MovieDetailModel();
            movie1.Id = 1;
            movie1.Poster = "images/DayInTheLifeOfNETDeveloper.png";
            movie1.Title = "A day in the life of a .NET Developer";
            movie1.Director = "Rishabh Verma";
            movie1.ShowTime = "11:00 AM";
            movie1.ReleaseDate = "23/03/2018";
            movie1.Description = "Relive a .NET developer's daily life and share his joy, excitement and experiences while coding .NET stuff";
            moviesList.Add(movie1);

            MovieDetailModel movie2 = new MovieDetailModel();
            movie2.Id = 2;
            movie2.Poster = "images/DevVsTest.png";
            movie2.Title = "Developers Vs Testers";
            movie2.Director = "Neha Shrivastava";
            movie2.ShowTime = "3:00 PM";
            movie2.ReleaseDate = "06/04/2018";
            movie2.Description = "Bored !?!? Add spice to your life by watching this Developers Vs Testers battle, which is surely more fresh and exciting than the TV soaps you have been watching. From invalid bug, no repro bugs, duplicate bugs to testing blocked due to high severity issue, it has all the drama that unfolds in an IT company";

            moviesList.Add(movie2);

            MovieDetailModel movie3 = new MovieDetailModel();
            movie3.Id = 3;
            movie3.Poster = "images/Cers.png";
            movie3.Title = "The C# ers";
            movie3.Director = "Neha Shrivastava";
            movie3.ShowTime = "6:00 PM";
            movie3.ReleaseDate = "13/04/2018";
            movie3.Description = "The coding avengers - C#ers";

            moviesList.Add(movie3);
            return moviesList;
        }

        public MovieDetailModel GetMovieById(int id)
        {
            return this.GetAllMovies().FirstOrDefault(j => j.Id == id);
        }        
    }
}
