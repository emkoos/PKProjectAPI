using Microsoft.EntityFrameworkCore;
using PKProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKProject.Infrastructure.Context
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Board> Boards { get; set; }
        public DbSet<BoardType> BoardTypes { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Column> Columns { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UsersTeam> UsersTeams { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UsersTeam>()
                .HasKey(ts => new { ts.UserEmail, ts.TeamId });

            modelBuilder.Entity<UsersTeam>()
                .HasOne(ts => ts.User)
                .WithMany(t => t.UserTeams)
                .HasForeignKey(ts => ts.UserEmail);

            modelBuilder.Entity<UsersTeam>()
                .HasOne(ts => ts.Team)
                .WithMany(s => s.UserTeams)
                .HasForeignKey(ts => ts.TeamId);

            modelBuilder.Entity<Board>()
                .HasMany(s => s.Columns)
                .WithOne(g => g.Board)
                .IsRequired();

            modelBuilder.Entity<BoardType>()
                .HasMany(s => s.Boards)
                .WithOne(g => g.BoardType)
                .IsRequired();

            modelBuilder.Entity<Card>()
                .HasMany(s => s.Comments)
                .WithOne(g => g.Card)
                .IsRequired();

            modelBuilder.Entity<Status>()
                .HasMany(s => s.Cards)
                .WithOne(g => g.Status)
                .IsRequired();

            modelBuilder.Entity<Team>()
                .HasMany(s => s.Boards)
                .WithOne(g => g.Team)
                .IsRequired();

            modelBuilder.Entity<Team>()
                .HasMany(s => s.UserTeams)
                .WithOne(g => g.Team)
                .IsRequired();

            modelBuilder.Entity<User>()
                .HasMany(s => s.UserTeams)
                .WithOne(g => g.User)
                .IsRequired();

            modelBuilder.Entity<User>()
                .HasMany(s => s.Comments)
                .WithOne(g => g.User)
                .IsRequired();

            modelBuilder.Entity<User>()
                .HasMany(s => s.Cards)
                .WithOne(g => g.User)
                .IsRequired();

            modelBuilder.Entity<Column>()
                .HasMany(s => s.Cards)
                .WithOne(g => g.Column)
                .IsRequired();

            modelBuilder.Entity<Board>()
                .HasOne(s => s.Team)
                .WithMany(g => g.Boards)
                .HasForeignKey(s => s.TeamId);

            modelBuilder.Entity<Board>()
                .HasOne(s => s.BoardType)
                .WithMany(g => g.Boards)
                .HasForeignKey(s => s.BoardTypeId);

            modelBuilder.Entity<Card>()
                .HasOne(s => s.User)
                .WithMany(g => g.Cards)
                .HasForeignKey(s => s.UserEmail);

            modelBuilder.Entity<Card>()
                .HasOne(s => s.Column)
                .WithMany(g => g.Cards)
                .HasForeignKey(s => s.ColumnId);

            modelBuilder.Entity<Card>()
                .HasOne(s => s.Status)
                .WithMany(g => g.Cards)
                .HasForeignKey(s => s.StatusId);

            modelBuilder.Entity<Comment>()
                .HasOne(s => s.User)
                .WithMany(g => g.Comments)
                .HasForeignKey(s => s.UserEmail)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comment>()
                .HasOne(s => s.Card)
                .WithMany(g => g.Comments)
                .HasForeignKey(s => s.CardId);

            modelBuilder.Entity<UsersTeam>()
                .HasOne(s => s.User)
                .WithMany(g => g.UserTeams)
                .HasForeignKey(s => s.UserEmail);

            modelBuilder.Entity<UsersTeam>()
                .HasOne(s => s.Team)
                .WithMany(g => g.UserTeams)
                .HasForeignKey(s => s.TeamId);

            modelBuilder.Entity<Column>()
                .HasOne(s => s.Board)
                .WithMany(g => g.Columns)
                .HasForeignKey(s => s.BoardId);
        }
    }
}
