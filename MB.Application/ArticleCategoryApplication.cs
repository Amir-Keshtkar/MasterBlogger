﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using MB.Application.Contracts;
using MB.Application.Contracts.ArticleCategory;
using MB.Domain.ArticleCategoryAgg;
using MB.Domain.ArticleCategoryAgg.Services;

namespace MB.Application {
    public class ArticleCategoryApplication: IArticleCategoryApplication {
        private readonly IArticleCategoryRepository _articleCategoryRepository;
        private readonly IArticleCategoryValidatorService _articleCategoryValidatorService;

        public ArticleCategoryApplication (IArticleCategoryRepository articleCategoryRepository, IArticleCategoryValidatorService articleCategoryValidatorService) {
            _articleCategoryRepository = articleCategoryRepository;
            _articleCategoryValidatorService = articleCategoryValidatorService;
        }

        public void Create (CreateArticleCategory command) {
            var articleCategory = new ArticleCategory(command.Title, _articleCategoryValidatorService);
            _articleCategoryRepository.Add(articleCategory);
        }

        public RenameArticleCategory Get (long Id) {
            var articleCategory = _articleCategoryRepository.GetById(Id);
            return new RenameArticleCategory {
                Id = articleCategory.Id,
                Title = articleCategory.Title,
            };
        }

        public void Remove(long Id) {
           var articleCategory= _articleCategoryRepository.GetById(Id);
           articleCategory.Remove();
           _articleCategoryRepository.Save();
        }

        public void Activate(long Id) {
           var articleCategory= _articleCategoryRepository.GetById(Id);
           articleCategory.Activate();
           _articleCategoryRepository.Save();
        }

        public List<ArticleCategoryViewModel> List () {
            var articleCategories = _articleCategoryRepository.GetAll();
            var result = new List<ArticleCategoryViewModel>();
            foreach(var articleCategory in articleCategories) {
                result.Add(new ArticleCategoryViewModel {
                    Id = articleCategory.Id,
                    Title = articleCategory.Title,
                    IsDeleted = articleCategory.IsDeleted,
                    CreationDate = articleCategory.CreationDate.ToString(CultureInfo.InvariantCulture),
                });
            }
            return result;
        }

        public void Rename (RenameArticleCategory command) {
            var articleCategory = _articleCategoryRepository.GetById(command.Id);
            articleCategory.Rename(command.Title);
            _articleCategoryRepository.Save();
        }
    }
}
