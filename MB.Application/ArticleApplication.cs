using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_Framework.Infrastructure;
using MB.Application.Contracts.Article;
using MB.Domain.ArticleAgg;

namespace MB.Application {
    public class ArticleApplication: IArticleApplication {
        private readonly IArticleRepository _articleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ArticleApplication (IArticleRepository articleRepository, IUnitOfWork unitOfWork) {
            _articleRepository = articleRepository;
            _unitOfWork = unitOfWork;
        }

        public void Create (CreateArticle command) {
            _unitOfWork.BeginTran();
            var article = new Article(command.Title, command.ShortDescription, command.Image, command.Content, command.ArticleCategoryId);
            _articleRepository.Create(article);
            _unitOfWork.Commit();
        }

        public void Edit (EditArticle command) {
            _unitOfWork.BeginTran();
            var article = _articleRepository.GetById(command.Id);
            article.EditArticle(command.Title, command.ShortDescription, command.Image, command.Content, command.ArticleCategoryId);
            _unitOfWork.Commit();
        }

        public EditArticle Get (long Id) {
            var article = _articleRepository.GetById(Id);
            return new EditArticle {
                Id = article.Id,
                Title = article.Title,
                ShortDescription = article.ShortDescription,
                Image = article.Image,
                Content = article.Content,
                ArticleCategoryId = article.ArticleCategoryId
            };
        }

        public List<ArticleViewModel> GetList () {
            return _articleRepository.GetList();
        }

        public void Activate (long Id) {
            _unitOfWork.BeginTran();
            var article = _articleRepository.GetById(Id);
            article.Activate();
            _unitOfWork.Commit();
        }

        public void Remove (long Id) {
            _unitOfWork.BeginTran();
            var article = _articleRepository.GetById(Id);
            article.Remove();
            _unitOfWork.Commit();
        }
    }
}
