﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using INT422TestOne.Models;

namespace INT422TestOne.ViewModels
{
    public class RepositoryBase
    {
        public RepositoryBase(){

            dc = new DataContext();

            // turn off EF tracking changes and lazy loading
            //   we do it ourselves
            dc.Configuration.ProxyCreationEnabled = false;
            dc.Configuration.LazyLoadingEnabled = false;

        }

        // implementation details
        protected DataContext dc;
    }
}