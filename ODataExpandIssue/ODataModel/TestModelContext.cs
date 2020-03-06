

namespace ODataModel
{
    using Microsoft.EntityFrameworkCore;

    public partial class TestModelContext : DbContext
    {
        /// <summary>
        /// DBContext class constructor
        /// </summary>
        /// <param name="options"></param>
        public TestModelContext(DbContextOptions<TestModelContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        protected TestModelContext()
        {
        }
        public virtual DbSet<ParentModel> Parent { get; set; }
        public virtual DbSet<ChildModel> Children { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            System.Diagnostics.Contracts.Contract.Requires(modelBuilder != null);

            modelBuilder.Entity<ParentModel>(entity =>
            {
                entity.HasIndex(e => e.Key)
                    .IsUnique();

                entity.HasKey(e => e.Key);
            });

            modelBuilder.Entity<ChildModel>(entity =>
            {
                entity.HasIndex(e => e.Key)
                    .IsUnique();

                entity.HasKey(e => e.Key);

                entity.HasOne(d => d.ParentModel)
                    .WithMany(p => p.Childrens)
                    .HasForeignKey(d => d.ParentKey)
                    .OnDelete(DeleteBehavior.ClientSetNull);
                    
            });
        }

    }

}