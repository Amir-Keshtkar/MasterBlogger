using System;
using System.Collections;
using System.Collections.Generic;
using _01_Framework.Domain;
using MB.Domain.ArticleAgg;
using MB.Domain.ArticleCategoryAgg.Services;

namespace MB.Domain.ArticleCategoryAgg {
    public class ArticleCategory : DomainBase<long> {
        public string Title { get; private set; }
        public bool IsDeleted { get; private set; }
        public ICollection<Article> Articles { get; private set; }

        protected ArticleCategory() {
        }

        public ArticleCategory (string title, IArticleCategoryValidatorService articleCategoryValidatorService) {
            GuardAgainstEmptyTitle(title);
            articleCategoryValidatorService.CheckRecordExistence(title);
            Articles=new List<Article>();
            Title = title;
            IsDeleted = false;
        }

        public void Rename (string title) {
            GuardAgainstEmptyTitle(title);

            Title = title;
        }

        public void Remove () {
            IsDeleted = true;
        }

        public void Activate () {
            IsDeleted = false;
        }

        void GuardAgainstEmptyTitle (string title) {
            if(string.IsNullOrWhiteSpace(title)) {
                throw new ArgumentNullException();
            }
        }
    }
}
