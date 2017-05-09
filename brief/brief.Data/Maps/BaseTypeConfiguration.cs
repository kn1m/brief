namespace brief.Data.Maps
{
    using System.Data.Entity.ModelConfiguration;

    public class BaseTypeConfiguration<TEntity> : EntityTypeConfiguration<TEntity> where TEntity : class 
    {
        public BaseTypeConfiguration()
        {
            
        }
    }
}
