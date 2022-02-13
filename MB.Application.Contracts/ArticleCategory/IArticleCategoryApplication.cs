using System;
using System.Collections.Generic;

namespace MB.Application.Contracts.ArticleCategory {
    public interface IArticleCategoryApplication {
        List<ArticleCategoryViewModel> List();
        void Create(CreateArticleCategory command);
        void Rename(RenameArticleCategory command);
        RenameArticleCategory Get(long Id);
        void Remove(long Id);
        void Activate(long Id);
    }
}
