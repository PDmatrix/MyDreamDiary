using System;
using DB.Models;
using Microsoft.EntityFrameworkCore;

namespace DB.Context
{
    public class CoreContext : DbContext
    {
        public CoreContext(DbContextOptions options)
            : base(options)
        {
        }

        public virtual DbSet<Comment> Comment { get; set; }
        public virtual DbSet<Dream> Dream { get; set; }
        public virtual DbSet<IdentityUser> IdentityUser { get; set; }
        public virtual DbSet<Post> Post { get; set; }
        public virtual DbSet<PostTag> PostTag { get; set; }
        public virtual DbSet<Tag> Tag { get; set; }
 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(
                    Environment.GetEnvironmentVariable(
                        "ASPNETCORE_CONNECTION_STRING") ??
                    throw new ArgumentException(
                        "There is no ASPNETCORE_CONNECTION_STRING provided")); 
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("comment");

                entity.HasIndex(e => e.Id)
                    .HasName("comment_id_uindex")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnName("content");

                entity.Property(e => e.DateCreated)
                    .HasColumnName("date_created")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.PostId).HasColumnName("post_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Comment)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("comment_post_id_fk");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comment)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("comment_identity_user_id_fk");
            });

            modelBuilder.Entity<Dream>(entity =>
            {
                entity.ToTable("dream");

                entity.HasIndex(e => e.Id)
                    .HasName("dream_id_uindex")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnName("content");

                entity.Property(e => e.DreamDate).HasColumnName("dream_date");
            });

            modelBuilder.Entity<IdentityUser>(entity =>
            {
                entity.ToTable("identity_user");

                entity.HasIndex(e => e.Email)
                    .HasName("identity_user_email_uindex")
                    .IsUnique();

                entity.HasIndex(e => e.Id)
                    .HasName("users_id_uindex")
                    .IsUnique();

                entity.HasIndex(e => e.Name)
                    .HasName("identity_user_name_uindex")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('users_id_seq'::regclass)");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("post");

                entity.HasIndex(e => e.DreamId)
                    .HasName("post_dream_id_uindex")
                    .IsUnique();

                entity.HasIndex(e => e.Id)
                    .HasName("post_id_uindex")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DateCreated)
                    .HasColumnName("date_created")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.DreamId).HasColumnName("dream_id");

                entity.Property(e => e.LikesCount)
                    .HasColumnName("likes_count")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Dream)
                    .WithOne(p => p.Post)
                    .HasForeignKey<Post>(d => d.DreamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("post_dream_id_fk");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Post)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("post_identity_user_id_fk");
            });

            modelBuilder.Entity<PostTag>(entity =>
            {
                entity.ToTable("post_tag");

                entity.HasIndex(e => e.Id)
                    .HasName("post_tag_id_uindex")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.PostId).HasColumnName("post_id");

                entity.Property(e => e.TagId).HasColumnName("tag_id");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.PostTag)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("post_tag_post_id_fk");

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.PostTag)
                    .HasForeignKey(d => d.TagId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("post_tag_tag_id_fk");
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.ToTable("tag");

                entity.HasIndex(e => e.Id)
                    .HasName("tag_id_uindex")
                    .IsUnique();

                entity.HasIndex(e => e.Name)
                    .HasName("tag_name_uindex")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");
            });

            modelBuilder.HasSequence("users_id_seq");
        }
    }
}
