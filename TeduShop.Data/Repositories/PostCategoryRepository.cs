using TeduShop.Data.Infrastructure;
using TeduShop.Model.Model;

namespace TeduShop.Data.Repositories
{
    public interface IPostCategoryRepositoryRepository
    {
    }

    public class PostCategoryRepositoryRepository : RepositoryBase<PostCategory>, IPostCategoryRepositoryRepository
    {
        public PostCategoryRepositoryRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
