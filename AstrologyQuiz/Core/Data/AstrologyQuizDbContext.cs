using System;
using System.Collections.Generic;
using System.Text;
using Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Core.Data
{
    public class AstrologyQuizDbContext : IdentityDbContext<Gebruiker>
    {
        public AstrologyQuizDbContext(DbContextOptions<AstrologyQuizDbContext> options)
            : base(options)
        {

        }
        public virtual DbSet<Quiz> Quizzen { get; set; }
        public virtual DbSet<QuizGebruiker> QuizGebruikers { get; set; }
        public virtual DbSet<Antwoord> Antwoorden { get; set; }
        public virtual DbSet<Gebruiker> Gebruikers { get; set; }
        public virtual DbSet<Moeilijkheidsgraad> Moeilijkheidsgraden { get; set; }
        public virtual DbSet<QuizGebruikerAntwoord> QuizGebruikerAntwoorden { get; set; }
        public virtual DbSet<Vraag> Vragen { get; set; }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            base.OnModelCreating(modelbuilder);
            modelbuilder.Seed();
            modelbuilder.Entity<QuizGebruikerAntwoord>(entity =>
            {
                entity.HasKey(e => new { e.QuizGebruikerId, e.AntwoordId });
                entity.HasOne(e => e.QuizGebruiker).WithMany(q => q.QuizGebruikerAntwoorden).OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
