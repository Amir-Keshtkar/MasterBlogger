﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MB.Application.Contracts.Article;
using MB.Domain.ArticleAgg;

namespace MB.Application {
    public class ArticleApplication: IArticleApplication {
        private readonly IArticleRepository _articleRepository;

        public ArticleApplication (IArticleRepository articleRepository) {
            _articleRepository = articleRepository;
        }

        public void Create (CreateArticle command) {
            var article = new Article(command.Title, command.ShortDescription, command.Image, command.Content, command.ArticleCategoryId);
            _articleRepository.Create(article);
        }

        public void Edit (EditArticle command) {
            var article = _articleRepository.GetById(command.Id);
            article.EditArticle(command.Title, command.ShortDescription, command.Image, command.Content, command.ArticleCategoryId);

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
            var article = _articleRepository.GetById(Id);
            article.Activate();
        }

        public void Remove (long Id) {
            var article = _articleRepository.GetById(Id);
            article.Remove();
        }
    }
}
