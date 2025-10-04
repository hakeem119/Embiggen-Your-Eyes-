using Microsoft.EntityFrameworkCore;
using NasaProject.Models;


namespace NasaProject.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<ImageMetadata> ImageMetadatas { get; set; }
        public DbSet<Annotation> Annotations { get; set; }
        public DbSet<ProcessingJob> ProcessingJobs { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User -> Images (NO cascade: deleting a User will be restricted if it still has Images)
            modelBuilder.Entity<Image>()
                .HasOne(i => i.User)
                .WithMany(u => u.Images)
                .HasForeignKey(i => i.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Image -> Metadata (1:1) - cascading delete from Image to its Metadata is OK
            modelBuilder.Entity<Image>()
                .HasOne(i => i.Metadata)
                .WithOne(m => m.Image)
                .HasForeignKey<ImageMetadata>(m => m.ImageId)
                .OnDelete(DeleteBehavior.Cascade);

            // Annotation -> Image (cascade: remove annotations when image removed)
            modelBuilder.Entity<Annotation>()
                .HasOne(a => a.Image)
                .WithMany(i => i.Annotations)
                .HasForeignKey(a => a.ImageId)
                .OnDelete(DeleteBehavior.Cascade);

            // Annotation -> User (NO cascade: prevent multiple cascade paths)
            modelBuilder.Entity<Annotation>()
                .HasOne(a => a.User)
                .WithMany(u => u.Annotations)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // ProcessingJob -> Image (cascade: remove jobs when image removed)
            modelBuilder.Entity<ProcessingJob>()
                .HasOne(p => p.Image)
                .WithMany(i => i.ProcessingJobs)
                .HasForeignKey(p => p.ImageId)
                .OnDelete(DeleteBehavior.Cascade);

            // ProcessingJob -> User (NO cascade: prevent multiple cascade paths)
            modelBuilder.Entity<ProcessingJob>()
                .HasOne(p => p.User)
                .WithMany(u => u.ProcessingJobs)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }


    }
}
