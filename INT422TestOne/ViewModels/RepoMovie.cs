using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using INT422TestOne.ViewModels;
using AutoMapper;

namespace INT422TestOne.ViewModels
{
    public class RepoMovie: RepositoryBase
    {

///////////////////////////////////////////////////////
//  AUTOMAPPER (Create/Edit/Delete and Get methods)
///////////////////////////////////////////////////////

        public MovieFull amCreateMovie(ViewModels.MovieCreate newItem, string d)
        {
            Models.Movie movie = Mapper.Map<Models.Movie>(newItem);

            int did = Convert.ToInt32(d);
            movie.Director = dc.Directors.FirstOrDefault(n => n.Id == did);

            dc.Movies.Add(movie);
            dc.SaveChanges();

            return Mapper.Map<MovieFull>(movie);
        }

        public MovieFull amEditMovie(MovieFull editItem)
        {
            var itemToEdit = dc.Movies.Find(editItem.Id);

            if (itemToEdit == null)
            {
                return null;
            }
            else
            {
                dc.Entry(itemToEdit).CurrentValues.SetValues(editItem);
                dc.SaveChanges();
            }

            return Mapper.Map<MovieFull>(editItem);
        }

        public void DeleteMovie(int? id)
        {
            var itemToDelete = dc.Movies.Find(id);

            if (itemToDelete == null)
            {
                return;
            }
            else
            {
                try
                {
                    dc.Movies.Remove(itemToDelete);
                    dc.SaveChanges();
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }

         }

        // Get one MovieFull
        public MovieFull amGetMovieFull(int? id)
        {
            var movie = dc.Movies.Include("Genres").Include("Director").SingleOrDefault(n => n.Id == id);

            if (movie == null) return null;
            else return Mapper.Map<MovieFull>(movie);

        }

        // Get all (ListOf) MovieFull
        public IEnumerable<MovieBase> amGetListOfMovieBase() {
            var movies = dc.Movies.OrderBy(m => m.Title);
            
            if (movies == null) return null;

            return Mapper.Map<IEnumerable<MovieBase>>(movies);
        }


/////////////////////////
// WITHOUT AUTOMAPPER
/////////////////////////


        public MovieFull createMovie(string title, string price, string gids, string d)
        {
            Models.Movie m = new Models.Movie();

            m.Title = title;
            m.TicketPrice = Convert.ToDecimal(price);

            foreach (var item in gids.Split(','))
            {
                var intItem = Convert.ToInt32(item);
                var g = dc.Genres.FirstOrDefault(gg => gg.Id == intItem);
                m.Genres.Add(g);
            }

            int did = Convert.ToInt32(d);
            m.Director = dc.Directors.FirstOrDefault(n => n.Id == did);

            dc.Movies.Add(m);
            dc.SaveChanges();

            return getMovieFull(m.Id);
            // return amGetMovieFull(m.Id);   // alternate method
        }

        public MovieFull getMovieFull(int? id)
        {
            var movie = dc.Movies.Include("Genres").Include("Director").SingleOrDefault(n => n.Id == id);

            if (movie == null) return null;

            MovieFull mf = new MovieFull();
            mf.Id = movie.Id;
            mf.Title = movie.Title;
            mf.TicketPrice = movie.TicketPrice;
            //mf.Director = rd.toDirectorFull(movie.Director);      // (alternate method)
            mf.Director = rd.getDirectorFull(movie.Director.Id);
            mf.Genres = rg.toListOfGenreBase(movie.Genres);

            return mf;
        }

        public IEnumerable<MovieBase> getListOfMovieBase(){

            var movies = dc.Movies.OrderBy(m => m.Title);

            List<MovieBase> mbls = new List<MovieBase>();

            foreach(var item in movies){
                MovieBase mf = new MovieBase();
                mf.Id = item.Id;
                mf.Title = item.Title;
                mbls.Add(mf);
            }

            return mbls;
        }

        public IEnumerable<MovieFull> getListOfMovieFull(){

            var movies = dc.Movies.Include("Genres").OrderBy(m => m.Title);
            
            List<MovieFull> mfls = new List<MovieFull>();

            foreach(var item in movies){
                MovieFull mf = new MovieFull();
                mf.Id = item.Id;
                mf.Title = item.Title;
                mf.TicketPrice = item.TicketPrice;
                mf.Director = rd.getDirectorFull(item.Id);
                mf.Genres = rg.toListOfGenreBase(item.Genres);
                mfls.Add(mf);
            }

            return mfls;
        }
 
        public List<MovieBase> toListOfMovieBase(List<Models.Movie> movies) {

            List<MovieBase> mbls = new List<MovieBase>();

            foreach (var item in movies) {
                MovieBase mm = new MovieBase();
                mm.Id = item.Id;
                mm.Title = item.Title;
                mbls.Add(mm);
            }

            return mbls;
        }

////////////////////////////////////////////
// CONSTRUCTOR and Implementation Details
////////////////////////////////////////////
        public RepoMovie(){
            rd = new RepoDirector();
            rg = new RepoGenre();
        }

        // Implementation details
        RepoDirector rd;
        RepoGenre rg;
    }
}