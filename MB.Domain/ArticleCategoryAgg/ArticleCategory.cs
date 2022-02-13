using System;
using System.Collections;
using System.Collections.Generic;
using MB.Domain.ArticleAgg;
using MB.Domain.ArticleCategoryAgg.Services;

namespace MB.Domain.ArticleCategoryAgg {
    public class ArticleCategory {
        public long Id { get; }
        public string Title { get; private set; }
        public bool IsDeleted { get; private set; }
        public DateTime CreationDate { get; private set; }
        public ICollection<Article> Articles { get; private set; }

        public ArticleCategory (string title, IArticleCategoryValidatorService articleCategoryValidatorService) {
            GuardAgainstEmptyTitle(title);
            articleCategoryValidatorService.CheckRecordExistence(title);
            Articles=new List<Article>();
            Title = title;
            IsDeleted = false;
            CreationDate = DateTime.Now;
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
