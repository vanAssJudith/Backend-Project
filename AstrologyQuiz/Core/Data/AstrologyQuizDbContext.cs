﻿using System;
using System.Collections.Generic;
using System.Text;
using Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Core.Data
{
    public class AstrologyQuizDbContext : IdentityDbContext
    {
        public AstrologyQuizDbContext(DbContextOptions<AstrologyQuizDbContext> options)
            : base(options)
        {

        }

        public virtual DbSet<Quiz> Quizzen { get; set; }
        public virtual DbSet<QuizGebruiker> QuizGebruikers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Seed();
        }
    }
}
