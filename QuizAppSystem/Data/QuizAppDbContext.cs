using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QuizAppSystem.Models;

public class QuizAppDbContext : IdentityDbContext<IdentityUser>
{
    public DbSet<Admin> Admins { get; set; }
    public DbSet<Participant> Participants { get; set; }
    public DbSet<Examiner> Examiners { get; set; }
    public DbSet<Exam> Exams { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<PaymentTransaction> PaymentTransactions { get; set; }
    public DbSet<Wallet> Wallets { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Answer> Answers { get; set; }


    public QuizAppDbContext(DbContextOptions<QuizAppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Configure data type for PaymentTransaction.Amount
        modelBuilder.Entity<PaymentTransaction>()
            .Property(pt => pt.Amount)
            .HasColumnType("decimal(18,2)");


        // Configure IdentityUserRole relationships
        modelBuilder.Entity<IdentityUserRole<string>>()
            .HasOne<IdentityRole>()
            .WithMany()
            .HasForeignKey(ur => ur.RoleId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<IdentityUserRole<string>>()
            .HasOne<IdentityUser>()
            .WithMany()
            .HasForeignKey(ur => ur.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Exam>()
            .Property(e => e.ExamCode)
            .ValueGeneratedOnAdd(); // Auto-generate ExamCode

        modelBuilder.Entity<Exam>()
            .Property(e => e.SubjectId)
            .ValueGeneratedOnAdd();

        // Configure Exam relationships
        modelBuilder.Entity<Exam>()
            .HasOne(e => e.CreatedByExaminer) // Ensure the property is "CreatedByExaminer"
            .WithMany(e => e.CreatedExams)
            .HasForeignKey(e => e.CreatedByExaminerId)
            .OnDelete(DeleteBehavior.NoAction);

        //modelBuilder.Entity<Exam>()
        //    .HasOne(e => e.Admin)
        //    .WithMany(a => a.AdministeredExams)
        //    .HasForeignKey(e => e.AdminId)
        //    .OnDelete(DeleteBehavior.NoAction);

        //// Configure Admin and Examiner relationships
        //modelBuilder.Entity<Admin>()
        //    .HasMany(a => a.AdministeredExams) // Use the correct property name "AdministeredExams"
        //    .WithOne(e => e.Admin)
        //    .HasForeignKey(e => e.AdminId)
        //    .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Examiner>()
            .HasMany(e => e.CreatedExams)
            .WithOne(e => e.CreatedByExaminer) // Ensure the property is "CreatedByExaminer"
            .HasForeignKey(e => e.CreatedByExaminerId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Examiner>()
            .HasMany(e => e.ExaminedExams) // Use the correct property name "ExaminedExams"
            .WithOne(e => e.Examiner)
            .HasForeignKey(e => e.ExaminerId)
            .OnDelete(DeleteBehavior.NoAction);

        // Additional configuration for your custom User entity
        modelBuilder.Entity<User>().ToTable("AspNetUsers");

        modelBuilder.Entity<Wallet>()
   .HasKey(w => w.WalletId);

        modelBuilder.Entity<Wallet>()
          .Property<Guid>("ParticipantId") // Update data type based on your choice (string or Guid)
          .IsRequired();

        modelBuilder.Entity<Wallet>()
          .HasOne(w => w.Participant)
          .WithMany()
          .HasForeignKey("ParticipantId")
          .OnDelete(DeleteBehavior.Cascade);

        // Configure the Answer entity
        modelBuilder.Entity<Answer>()
            .HasKey(a => a.Id);

        modelBuilder.Entity<Answer>()
            .HasOne(a => a.Question)
            .WithMany(q => q.Answers)
            .HasForeignKey(a => a.QuestionId)
            .OnDelete(DeleteBehavior.Cascade);
    }



}
