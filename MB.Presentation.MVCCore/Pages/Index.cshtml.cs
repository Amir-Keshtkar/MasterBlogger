using MB.Infrastructure.Query;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MB.Presentation.MVCCore.Pages {
    public class IndexModel: PageModel {
        private readonly IArticleQuery _articleQuery;
        public List<ArticleQueryView> Articles { get; set; }

        public IndexModel(IArticleQuery articleQuery) {
            _articleQuery = articleQuery;
        }


        public void OnGet () {
            Articles = _articleQuery.GetArticles();
        }
    }
}