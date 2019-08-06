namespace TeduShop.Data.Infrastructure
{
    public class DbFactory : Disposeable, IDbFactory
    {
        private TeduShopDbContext dbContext;

        public TeduShopDbContext Init()
        {
            return dbContext ?? (dbContext = new TeduShopDbContext()); //Nếu dbContext null thì khởi tạo 1 đối tượng mới
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}