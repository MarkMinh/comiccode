using Microsoft.EntityFrameworkCore;

namespace WebAPI.Models;

public partial class Prn231ComicCodeContext : DbContext
{
    public Prn231ComicCodeContext()
    {
    }

    public Prn231ComicCodeContext(DbContextOptions<Prn231ComicCodeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Chapter> Chapters { get; set; }

    public virtual DbSet<Comic> Comics { get; set; }

    public virtual DbSet<ComicGenre> ComicGenres { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Page> Pages { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserToken> UserTokens { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.ToTable("Author");

            entity.Property(e => e.AuthorId).HasColumnName("author_id");
            entity.Property(e => e.AuthorDescription)
                .HasMaxLength(255)
                .HasColumnName("author_description");
            entity.Property(e => e.AuthorName)
                .HasMaxLength(255)
                .HasColumnName("author_name");
        });

        modelBuilder.Entity<Chapter>(entity =>
        {
            entity.ToTable("Chapter");

            entity.Property(e => e.ChapterId).HasColumnName("chapter_id");
            entity.Property(e => e.ChapterNumber).HasColumnName("chapter_number");
            entity.Property(e => e.ChapterTitle)
                .HasMaxLength(255)
                .HasColumnName("chapter_title");
            entity.Property(e => e.ComicId).HasColumnName("comic_id");
            entity.Property(e => e.CreateAt)
                .HasColumnType("datetime")
                .HasColumnName("create_at");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.UpdateAt)
                .HasColumnType("datetime")
                .HasColumnName("update_at");

            entity.HasOne(d => d.Comic).WithMany(p => p.Chapters)
                .HasForeignKey(d => d.ComicId)
                .HasConstraintName("FK_Chapter_Comic");
        });

        modelBuilder.Entity<Comic>(entity =>
        {
            entity.ToTable("Comic");

            entity.Property(e => e.ComicId).HasColumnName("comic_id");
            entity.Property(e => e.AuthorId).HasColumnName("author_id");
            entity.Property(e => e.ComicDescription).HasColumnName("comic_description");
            entity.Property(e => e.ComicTitle)
                .HasMaxLength(255)
                .HasColumnName("comic_title");
            entity.Property(e => e.CoverImage)
                .HasMaxLength(255)
                .HasColumnName("cover_image");
            entity.Property(e => e.CreateAt)
                .HasColumnType("datetime")
                .HasColumnName("create_at");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.UpdateAt)
                .HasColumnType("datetime")
                .HasColumnName("update_at");

            entity.HasOne(d => d.Author).WithMany(p => p.Comics)
                .HasForeignKey(d => d.AuthorId)
                .HasConstraintName("FK_Comic_Author");
        });

        modelBuilder.Entity<ComicGenre>(entity =>
        {
            entity.ToTable("ComicGenre");

            entity.Property(e => e.ComicGenreId).HasColumnName("comic_genre_id");
            entity.Property(e => e.ComicId).HasColumnName("comic_id");
            entity.Property(e => e.GenreId).HasColumnName("genre_id");

            entity.HasOne(d => d.Comic).WithMany(p => p.ComicGenres)
                .HasForeignKey(d => d.ComicId)
                .HasConstraintName("FK_ComicGenre_Comic");

            entity.HasOne(d => d.Genre).WithMany(p => p.ComicGenres)
                .HasForeignKey(d => d.GenreId)
                .HasConstraintName("FK_ComicGenre_Genre");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.ToTable("Comment");

            entity.Property(e => e.CommentId).HasColumnName("comment_id");
            entity.Property(e => e.ChapterId).HasColumnName("chapter_id");
            entity.Property(e => e.CommentContent).HasColumnName("comment_content");
            entity.Property(e => e.CommentParentId).HasColumnName("comment_parent_id");
            entity.Property(e => e.CreateAt)
                .HasColumnType("datetime")
                .HasColumnName("create_at");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.UpdateAt)
                .HasColumnType("datetime")
                .HasColumnName("update_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Chapter).WithMany(p => p.Comments)
                .HasForeignKey(d => d.ChapterId)
                .HasConstraintName("FK_Comment_Chapter");

            entity.HasOne(d => d.User).WithMany(p => p.Comments)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Comment_User");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.ToTable("Genre");

            entity.Property(e => e.GenreId).HasColumnName("genre_id");
            entity.Property(e => e.CreateAt)
                .HasColumnType("datetime")
                .HasColumnName("create_at");
            entity.Property(e => e.GenreDescription).HasColumnName("genre_description");
            entity.Property(e => e.GenreName)
                .HasMaxLength(255)
                .HasColumnName("genre_name");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.UpdateAt)
                .HasColumnType("datetime")
                .HasColumnName("update_at");
        });

        modelBuilder.Entity<Page>(entity =>
        {
            entity.HasKey(e => e.PageId).HasName("PK_Table_1");

            entity.ToTable("Page");

            entity.Property(e => e.PageId).HasColumnName("page_id");
            entity.Property(e => e.ChapterId).HasColumnName("chapter_id");
            entity.Property(e => e.PageImage)
                .HasMaxLength(255)
                .HasColumnName("page_image");
            entity.Property(e => e.PageNumber).HasColumnName("page_number");

            entity.HasOne(d => d.Chapter).WithMany(p => p.Pages)
                .HasForeignKey(d => d.ChapterId)
                .HasConstraintName("FK_Page_Chapter");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Role");

            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .HasColumnName("role_name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreateAt)
                .HasColumnType("datetime")
                .HasColumnName("create_at");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.UpdateAt)
                .HasColumnType("datetime")
                .HasColumnName("update_at");
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .HasColumnName("username");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_User_Role");
        });

        modelBuilder.Entity<UserToken>(entity =>
        {
            entity.ToTable("UserToken");

            entity.Property(e => e.UserTokenId).HasColumnName("user_token_id");
            entity.Property(e => e.CreateAt)
                .HasColumnType("datetime")
                .HasColumnName("create_at");
            entity.Property(e => e.RefreshToken)
                .HasMaxLength(500)
                .HasColumnName("refresh_token");
            entity.Property(e => e.RefreshTokenExpiryTime)
                .HasColumnType("datetime")
                .HasColumnName("refresh_token_expiry_time");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.UserTokens)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserToken_User");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
