﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace INT422TestOne.ViewModels
{
    public class RepoDirector: RepositoryBase
    {

        public DirectorFull getDirectorFull(int? id)
        {
            var director = dc.Directors.Include("Movies").FirstOrDefault(i => i.Id == id);
            if (director == null) return null;

            DirectorFull df = new DirectorFull();
            df.DirectorId = director.Id;
            df.Name = director.Name;
            List<MovieBase> mv = new List<MovieBase>();
            foreach (var item in director.Movies)
            {
                MovieBase mb = new MovieBase();
                mb.Id = item.Id;
                mb.Title = item.Title;
                mv.Add(mb);
            }
            df.Movies = mv;

            return df;
        }

        public IEnumerable<DirectorBase> getListOfDirectorBase()
        {
            var directors = dc.Directors.OrderBy(d => d.Name);
            if (directors == null) return null;

            List<DirectorBase> dls = new List<DirectorBase>();
            foreach (var item in directors)
            {
                DirectorBase db = new DirectorBase();
                db.DirectorId = item.Id;
                db.Name = item.Name;
                dls.Add(db);    
            }
            return dls;
        }

        public DirectorFull toDirectorFull(Models.Director d)
        {
            if (d == null) return null;

            DirectorFull df = new DirectorFull();
            df.DirectorId = d.Id;
            df.Name = d.Name;
            
            df.Movies = new List<MovieBase>();
            foreach (var item in d.Movies){
                MovieBase m = new MovieBase();
                m.Id = item.Id;
                m.Title = item.Title;
                df.Movies.Add(m);
            }
            
            return df;
        }

        public SelectList getDirectorSelectList(){
           SelectList sl = new SelectList(getListOfDirectorBase(), "DirectorId", "Name");
           return sl;
       }
    }
}