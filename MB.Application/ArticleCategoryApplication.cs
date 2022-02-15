using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using _01_Framework.Infrastructure;
using MB.Application.Contracts.ArticleCategory;
using MB.Domain.ArticleCategoryAgg;
using MB.Domain.ArticleCategoryAgg.Services;

namespace MB.Application {
    public class ArticleCategoryApplication: IArticleCategoryApplication {
        private readonly IArticleCategoryRepository _articleCategoryRepository;
        private readonly IArticleCategoryValidatorService _articleCategoryValidatorService;
        private readonly IUnitOfWork _unitOfWork;

        public ArticleCategoryApplication (IArticleCategoryRepository articleCategoryRepository, IArticleCategoryValidatorService articleCategoryValidatorService, IUnitOfWork unitOfWork) {
            _articleCategoryRepository = articleCategoryRepository;
            _articleCategoryValidatorService = articleCategoryValidatorService;
            _unitOfWork = unitOfWork;
        }

        public List<ArticleCategoryViewModel> List () {
            var articleCategories = _articleCategoryRepository.GetAll();
            return articleCategories.Select(articleCategory => new ArticleCategoryViewModel {
                Id = articleCategory.Id, Title = articleCategory.Title, IsDeleted = articleCategory.IsDeleted, CreationDate = articleCategory.CreationDate.ToString(CultureInfo.InvariantCulture),
            })
                .OrderByDescending(x => x.Id).ToList();
        }

        public RenameArticleCategory Get (long Id) {
            var articleCategory = _articleCategoryRepository.GetById(Id);
            return new RenameArticleCategory {
                Id = articleCategory.Id,
                Title = articleCategory.Title,
            };
        }

        public void Create (CreateArticleCategory command) {
            _unitOfWork.BeginTran();
            var articleCategory = new ArticleCategory(command.Title, _articleCategoryValidatorService);
            _articleCategoryRepository.Create(articleCategory);
            _unitOfWork.Commit();
        }

        public void Remove (long Id) {
            _unitOfWork.BeginTran();
            var articleCategory = _articleCategoryRepository.GetById(Id);
            articleCategory.Remove();
            _unitOfWork.Commit();
        }

        public void Activate (long Id) {
            _unitOfWork.BeginTran();
            var articleCategory = _articleCategoryRepository.GetById(Id);
            articleCategory.Activate();
            _unitOfWork.Commit();
        }


        public void Rename (RenameArticleCategory command) {
            _unitOfWork.BeginTran();
            var articleCategory = _articleCategoryRepository.GetById(command.Id);
            articleCategory.Rename(command.Title);
            _unitOfWork.Commit();
        }
    }
}
