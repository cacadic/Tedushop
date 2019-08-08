using System.Collections.Generic;
using TeduShop.Data.Infrastructure;
using TeduShop.Data.Repositories;
using TeduShop.Model.Model;

namespace TeduShop.Services
{
    public interface IPostCategoryService
    {
        PostCategory Add(PostCategory entity);

        void Update(PostCategory entity);

        PostCategory Delete(int id);

        IEnumerable<PostCategory> GetAll();

        IEnumerable<PostCategory> GetAllByParentIdPaging(int parentId, int pageIndex, int pageSize, out int totalRow);

        PostCategory GetById(int id);

        void SaveChanges();
    }

    public class PostCategoryService : IPostCategoryService
    {
        #region Variables

        private IPostCategoryRepository _postCategoryRepository;
        private IUnitOfWork _unitOfWork;

        #endregion Variables

        #region Contrustor

        public PostCategoryService(IPostCategoryRepository postCategoryRepository, IUnitOfWork unitOfWork)
        {
            this._postCategoryRepository = postCategoryRepository;
            this._unitOfWork = unitOfWork;
        }

        #endregion Contrustor

        #region Implements

        public PostCategory Add(PostCategory entity)
        {
            return _postCategoryRepository.Add(entity);
        }

        public PostCategory Delete(int id)
        {
            return _postCategoryRepository.Delete(id);
        }

        public IEnumerable<PostCategory> GetAll()
        {
            return _postCategoryRepository.GetAll();
        }

        public PostCategory GetById(int id)
        {
            return _postCategoryRepository.GetSingleById(id);
        }

        public void Update(PostCategory entity)
        {
            _postCategoryRepository.Update(entity);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public IEnumerable<PostCategory> GetAllByParentIdPaging(int parentId, int pageIndex, int pageSize, out int totalRow)
        {
            return _postCategoryRepository.GetMultiPaging(x => x.Status && x.ParentID == parentId, out totalRow, pageIndex, pageSize);
        }

        #endregion Implements
    }
}