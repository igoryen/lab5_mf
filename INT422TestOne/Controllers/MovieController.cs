using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using INT422TestOne.ViewModels;

namespace INT422TestOne.Controllers
{
    public class MovieController : Controller
    { 
        private RepoMovie repo = new RepoMovie();
        private RepoGenre gen = new RepoGenre();
        private RepoDirector dir = new RepoDirector();

        //
        // GET: /Movie/
        public ActionResult Index()
        {
            //return View(repo.getListOfMovieBase());
            return View(repo.amGetListOfMovieBase());
        }

        //
        // GET: /Movie/Details/5
        public ActionResult Details(int id)
        {
            //return View(repo.getMovieFull(id));
            return View(repo.amGetMovieFull(id));
        }

////////////////////////////
// CREATE
////////////////////////////

        // GET: /Movie/Create
        public ActionResult MovieCreate()
        {
            ViewModels.MovieCreate newItem = new ViewModels.MovieCreate();

            ViewBag.genres = gen.getGenreSelectList();
            ViewBag.directors = dir.getDirectorSelectList();

            return View("MovieCreate", newItem);
           
        }

        // POST: /Movie/Create
        [HttpPost]
        public ActionResult MovieCreate(FormCollection form, ViewModels.MovieCreate newItem)
        {
            if (ModelState.IsValid)
            {
                  try
                  {
                      if (form.Count == 4)
                      {
                          var addedItem = repo.amCreateMovie(newItem, form[3]);
                          if (addedItem == null)
                          {
                              return View("Error");
                          }
                          else
                          {
                              return RedirectToAction("Details", new { Id = addedItem.Id });
                          }
                      }

                      return RedirectToAction("Index");
                  }
                  catch (Exception e)
                  {
                      ViewBag.ExceptionMessage = e.Message;

                      return View("Error");
                  }

            }
            else
            {
                return View("Error");
            }
        }


        //
        // GET: /Movie/Create
        public ActionResult Create()
        {
            ViewBag.genres = gen.getGenreSelectList();
            ViewBag.directors = dir.getDirectorSelectList();

            return View();
        }

        //
        // POST: /Movie/Create
        [HttpPost]
        public ActionResult Create(FormCollection form)
        {
            try
            {
                if (form.Count == 5)
                {
                    repo.createMovie(form[1], form[2], form[3], form[4]);
                }

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ViewBag.ExceptionMessage = e.Message;

                return View("Error");
            }
        }


////////////////////////////
// EDIT
////////////////////////////

        // GET: /Movies/Edit/5
        public ActionResult Edit(int? id)
        {
            // do not query if id is null
            if (id == null)
            {
                ViewBag.ExceptionMessage = "That was an invalid record";
                return View("Error");
            }

            var movie = repo.amGetMovieFull(id);
            if (movie == null)
            {
                ViewBag.ExceptionMessage = "That record could not be edited because it doesn't exist";
                return View("Error");
            }

            return View(movie);

        }

        // POST: /Movies/Edit/5
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Title,TicketPrice")]ViewModels.MovieFull editedItem)
        {

            if (ModelState.IsValid)
            {
                var newItem = repo.amEditMovie(editedItem);
                if (newItem == null)
                {
                    ViewBag.ExceptionMessage = "record " + editedItem.Id + " was not found.";
                    return View("Error");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return View("Error");
            }
        }

////////////////////////////
// DELETE
////////////////////////////

        // GET: /Movies/Delete/5
        public ActionResult Delete(int? id)
        {
            // do not query if id is null
            if (id == null)
            {
                ViewBag.ExceptionMessage = "That was an invalid record";
                return View("Error");
            }

            var movie = repo.amGetMovieFull(id);
            if (movie == null)
            {
                ViewBag.ExceptionMessage = "That record could not be deleted because it doesn't exist";
                return View("Error");
            }

            return View(movie);

        }

        // POST: /Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            repo.DeleteMovie(id);

            return RedirectToAction("Index");
        }

    }
}
