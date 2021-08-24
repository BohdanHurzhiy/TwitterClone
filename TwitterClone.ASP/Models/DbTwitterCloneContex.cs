using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TwitterClone.ASP.Models
{
    public class DbTwitterCloneContex : IdentityDbContext<User>
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<Repost> RePosts { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Liked> Likeds { get; set; }
        public DbSet<LikedAnswer> LikedsAnswer { get; set; }
        public DbSet<Photos> Photos { get; set; }
        public DbSet<RelationshipsUser> Relationships { get; set; }
        public DbSet<Tag> Tags { get; set; }
       // public DbSet<TagsPost> TagsPosts { get; set; }


        public DbTwitterCloneContex(DbContextOptions<DbTwitterCloneContex> options)
            : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public DbTwitterCloneContex()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured) 
        //    { 
        //        optionsBuilder.UseSqlServer("Server = (localdb)\\mssqllocaldb;Database = DbTwitterClone;Trusted_Connection = True;");
        //    }
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Post>(PostConfigure);
            modelBuilder.Entity<Liked>(LikedConfigure);
            modelBuilder.Entity<Tag>(TagConfigure);
           // modelBuilder.Entity<TagsPost>(TagsPostConfigure);
        }
        
        public void PostConfigure(EntityTypeBuilder<Post> builder)
        {
            builder.Property(p => p.PublicationDate)
                .HasDefaultValueSql("GETDATE()");            
            builder
                .HasMany(p => p.Tags)
                .WithMany(t => t.Posts)
                .UsingEntity(t => t.ToTable("TagsPosts"));

        }
        public void LikedConfigure(EntityTypeBuilder<Liked> builder)
        {
            //builder.HasData(
            //    new Liked[]
            //    {
            //        new() {Id = 1, UserId = "1", PostId = 1 },
            //        new() {Id = 2, UserId = "1", PostId = 2 },
            //        new() {Id = 3, UserId = "1", PostId = 3 },
            //        new() {Id = 4, UserId = "2", PostId = 4 },
            //        new() {Id = 5, UserId = "2", PostId = 5 },
            //        new() {Id = 6, UserId = "2", PostId = 6 },
            //        new() {Id = 7, UserId = "2", PostId = 2 },
            //        new() {Id = 8, UserId = "3", PostId = 6 },
            //        new() {Id = 9, UserId = "2", PostId = 2 },
            //        new() {Id = 10, UserId = "2", PostId = 7 },
            //        new() {Id = 11, UserId = "7", PostId = 8 },
            //        new() {Id = 12, UserId = "6", PostId = 1 },
            //        new() {Id = 13, UserId = "3", PostId = 1 },
            //        new() {Id = 14, UserId = "12", PostId = 1 },
            //        new() {Id = 15, UserId = "1", PostId = 2 }
            //    });
        }
        public void TagConfigure(EntityTypeBuilder<Tag> builder)
        {
            builder.HasData(
                new Tag[]
                {
                    new() {Id = 1, TagsText = "тег1" },
                    new() {Id = 2, TagsText = "тег2" },
                    new() {Id = 3, TagsText = "тег3" },
                    new() {Id = 4, TagsText = "тег4" }
                });
        }
        //public void TagsPostConfigure(EntityTypeBuilder<TagsPost> builder)
        //{
        //    builder.HasData(
        //        new TagsPost[]
        //        {
        //            new() {Id = 1, PostId = 1, TagId = 1 },
        //            new() {Id = 2, PostId = 1, TagId = 2 },
        //            new() {Id = 3, PostId = 1, TagId = 3 },
        //            new() {Id = 4, PostId = 2, TagId = 4 },
        //            new() {Id = 5, PostId = 2, TagId = 1 },
        //            new() {Id = 6, PostId = 3, TagId = 3 },
        //            new() {Id = 7, PostId = 4, TagId = 3 },
        //            new() {Id = 8, PostId = 5, TagId = 4 },
        //            new() {Id = 9, PostId = 6, TagId = 1 },
        //            new() {Id = 10, PostId = 7, TagId = 4 },
        //            new() {Id = 11, PostId = 8, TagId = 2 }
        //        });
        //}

    }
}
