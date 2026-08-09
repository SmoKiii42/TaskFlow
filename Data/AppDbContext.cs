using Microsoft.EntityFrameworkCore;
using System.IO;
using TaskFlow.Models;
using TaskFlow.Новая_папка1.TaskFlow;

namespace TaskFlow.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<TaskItem> TaskItems { get; set; }
        public DbSet<Workspace> Workspaces { get; set; }
        public DbSet<FriendRequest> FriendRequests { get; set; }
        public DbSet<Friend> Friends { get; set; }
        public DbSet<WorkspaceInvite> WorkspaceInvites { get; set; }
        public DbSet<WorkspaceMember> WorkspaceMembers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string folder = Environment.GetFolderPath(
                Environment.SpecialFolder.ApplicationData);

            string path = Path.Combine(folder, "TaskFlow");

            Directory.CreateDirectory(path);

            string databasePath = Path.Combine(path, "TaskFlow.db");

            optionsBuilder.UseSqlite($"Data Source={databasePath}");
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Friend
            modelBuilder.Entity<Friend>()
                .HasOne(f => f.User)
                .WithMany()
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Friend>()
                .HasOne(f => f.FriendUser)
                .WithMany()
                .HasForeignKey(f => f.FriendId)
                .OnDelete(DeleteBehavior.Restrict);

            // FriendRequest
            modelBuilder.Entity<FriendRequest>()
                .HasOne(fr => fr.Sender)
                .WithMany()
                .HasForeignKey(fr => fr.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<FriendRequest>()
                .HasOne(fr => fr.Receiver)
                .WithMany()
                .HasForeignKey(fr => fr.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<WorkspaceInvite>()
                    .HasOne(i => i.Workspace)
                       .WithMany()
                    .HasForeignKey(i => i.WorkspaceId)
                     .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<WorkspaceInvite>()
                .HasOne(i => i.Sender)
                .WithMany()
                .HasForeignKey(i => i.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<WorkspaceInvite>()
                .HasOne(i => i.Receiver)
                .WithMany()
                .HasForeignKey(i => i.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<WorkspaceMember>()
    .HasOne(wm => wm.Workspace)
    .WithMany()
    .HasForeignKey(wm => wm.WorkspaceId)
    .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<WorkspaceMember>()
                .HasOne(wm => wm.User)
                .WithMany()
                .HasForeignKey(wm => wm.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}