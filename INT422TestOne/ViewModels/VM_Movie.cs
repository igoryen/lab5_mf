﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace INT422TestOne.ViewModels
{
    public class MovieCreate
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        [Required]
        public decimal TicketPrice { get; set; }
        public int DirectorId { get; set; }

    }
    public class MovieBase {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
    }
    public class MovieFull: MovieBase
    {
        [Required]
        [Display(Name = "Ticket Price")]
        public decimal TicketPrice { get; set; }
        
        public DirectorFull Director { get; set; }
        
        public List<GenreBase> Genres { get; set; }

        public MovieFull()
        {
            this.Director = new DirectorFull();
            this.Genres = new List<GenreBase>();

        }
    }

}