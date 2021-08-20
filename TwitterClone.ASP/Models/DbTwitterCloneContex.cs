using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TwitterClone.ASP.Models
{
    public class DbTwitterCloneContex : DbContext
    {
        public DbSet<User> Users { get; set; }
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
        { }

        public DbTwitterCloneContex()
        {
           Database.EnsureDeleted();
           Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured) 
            { 
                optionsBuilder.UseSqlServer("Server = (localdb)\\mssqllocaldb;Database = DbTwitterClone;Trusted_Connection = True;");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(UserConfigure);
            modelBuilder.Entity<Post>(PostConfigure);
            modelBuilder.Entity<Liked>(LikedConfigure);
            modelBuilder.Entity<Tag>(TagConfigure);
           // modelBuilder.Entity<TagsPost>(TagsPostConfigure);
        }
        // конфигурация для типа User
        public void UserConfigure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(
                new User[]
                {
                    new() { Name = "Tom", Alias = "T", SecondName = "Patison" },
                    new() {Name = "Adam", Alias = "A", SecondName = "Morkovkin" },
                    new() { Name = "Jopa", Alias = "J", SecondName = "Kabanov" },
                    new() { Name = "Petr", Alias = "P", SecondName = "Moroshko" },
                    new() { Name = "Lola", Alias = "L", SecondName = "Sharifov" },
                    new() { Name = "Kolya", Alias = "K", SecondName = "Moroz" },
                    new() { Name = "Edison", Alias = "E", SecondName = "Ivanov" },
                    new() { Name = "Paramon", Alias = "Pa", SecondName = "Lunin" },
                    new() { Name = "Kostya", Alias = "Ko", SecondName = "Kabachkov" },
                    new() { Name = "Bohdan", Alias = "B", SecondName = "Bulgakov" },
                    new() { Name = "Alla", Alias = "Al", SecondName = "Patison" }
                });
        }
        public void PostConfigure(EntityTypeBuilder<Post> builder)
        {
            builder.Property(p => p.PublicationDate)
                .HasDefaultValueSql("GETDATE()");
            builder.HasData(
                new Post[]
                {
                    new() {Id = 1, UserId = 1, TextPost = "Some Text User 1" },
                    new() {Id = 2, UserId = 1, TextPost = "Some Text User 1" },
                    new() {Id = 3, UserId = 2, TextPost = "Some Text User 2" },
                    new() {Id = 4, UserId = 4, TextPost = "Some Text User 4" },
                    new() {Id = 5, UserId = 5, TextPost = "Some Text User 5" },
                    new() {Id = 6, UserId = 6, TextPost = "Some Text User 6" },
                    new() {Id = 7, UserId = 1, TextPost = "Some Text User 1" },
                    new() {Id = 8, UserId = 7, TextPost = "Some Text User 7" },
                    new() {Id = 9, UserId = 12, TextPost = "Some Text User 23" },
                    new() {Id = 10, UserId = 5, TextPost = "Some Text User 5" },
                    new() {Id = 11, UserId = 2, TextPost = "Some Text User 2" },
                    new() {Id = 12, UserId = 7, TextPost = "Some Text User 7" },
                    new() {Id = 13, UserId = 8, TextPost = "Some Text User 8" },
                    new() {Id = 14, UserId = 2, TextPost = "Some Text User 2" },
                    new() {Id = 15, UserId = 4, TextPost = "Some Text User 4" },
                    new() {Id = 16, UserId = 1, TextPost = "Some Text User 1" }
                });
            builder
                .HasMany(p => p.Tags)
                .WithMany(t => t.Posts)
                .UsingEntity(t => t.ToTable("TagsPosts"));

        }
        public void LikedConfigure(EntityTypeBuilder<Liked> builder)
        {
            builder.HasData(
                new Liked[]
                {
                    new() {Id = 1, UserId = 1, PostId = 1 },
                    new() {Id = 2, UserId = 1, PostId = 2 },
                    new() {Id = 3, UserId = 1, PostId = 3 },
                    new() {Id = 4, UserId = 4, PostId = 4 },
                    new() {Id = 5, UserId = 4, PostId = 5 },
                    new() {Id = 6, UserId = 2, PostId = 6 },
                    new() {Id = 7, UserId = 2, PostId = 2 },
                    new() {Id = 8, UserId = 3, PostId = 6 },
                    new() {Id = 9, UserId = 2, PostId = 2 },
                    new() {Id = 10, UserId = 2, PostId = 7 },
                    new() {Id = 11, UserId = 7, PostId = 8 },
                    new() {Id = 12, UserId = 6, PostId = 1 },
                    new() {Id = 13, UserId = 3, PostId = 1 },
                    new() {Id = 14, UserId = 12, PostId = 1 },
                    new() {Id = 15, UserId = 5, PostId = 2 }
                });
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
