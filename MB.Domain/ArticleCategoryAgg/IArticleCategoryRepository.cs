using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MB.Domain.ArticleCategoryAgg {
    public interface IArticleCategoryRepository {
        List<ArticleCategory> GetAll();
        ArticleCategory GetById(long id);
        void Add(ArticleCategory category);
        bool Exists(string Title);
        void Save();
    }
}
