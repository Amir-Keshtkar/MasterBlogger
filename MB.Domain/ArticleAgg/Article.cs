using System;
using System.Collections.Generic;
using System.IO;
using _01_Framework.Domain;
using MB.Domain.ArticleCategoryAgg;
using MB.Domain.CommentAgg;

namespace MB.Domain.ArticleAgg {
    public class Article : DomainBase<long> {
        public string Title { get; private set; }
        public string ShortDescription { get; private set; }
        public string Image { get; private set; }
        public string Content { get; private set; }
        public bool IsDeleted { get; private set; }
        public long ArticleCategoryId { get; set; }
        public ArticleCategory ArticleCategory { get; set; }
        public ICollection<Comment> Comments { get; private set; }

        //public byte[] Image { get; set; }

        protected Article (){
        }

        public Article (string title, string shortDescription, string image, string content, long articleCategoryId) {
            Validate(title, articleCategoryId);
            Title = title;
            ShortDescription = shortDescription;
            Image = image;
            Content = content;
            ArticleCategoryId = articleCategoryId;
            IsDeleted = false;
            Comments = new List<Comment>();
        }

        private static void Validate (string title, long articleCategoryId) {
            if(string.IsNullOrWhiteSpace(title)) {
                throw new ArgumentNullException();
            }
            if(articleCategoryId == 0) {
                throw new ArgumentOutOfRangeException();
            }
        }

        public void EditArticle (string title, string shortDescription, string image, string content, long articleCategoryId) {
            Validate(title, articleCategoryId);
            Title = title;
            ShortDescription = shortDescription;
            Image = image;
            Content = content;
            ArticleCategoryId = articleCategoryId;
        }

        public void Remove () {
            IsDeleted = true;
        }

        public void Activate () {
            IsDeleted = false;
        }
    }
}
