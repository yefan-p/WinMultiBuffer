namespace DAL
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MultiBufferEntities : DbContext
    {
        public MultiBufferEntities()
            : base("name=MultiBufferContext")
        {
        }

        public virtual DbSet<tblClipboard> tblClipboards { get; set; }
        public virtual DbSet<tblUser> tblUsers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tblUser>()
                .HasMany(e => e.tblClipboards)
                .WithRequired(e => e.tblUser)
                .HasForeignKey(e => e.idUser)
                .WillCascadeOnDelete(false);
        }
    }
}
