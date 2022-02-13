using MB.Application.Contracts.Article;
using MB.Application.Contracts.ArticleCategory;
using MB.Domain.ArticleAgg;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MB.Presentation.MVCCore.Areas.Administrator.Pages.ArticleManagement {
    public class EditModel: PageModel {
        [BindProperty] public EditArticle Article { get; set; }
        private readonly IArticleApplication _articleApplication;
        private readonly IArticleCategoryApplication _articleCategoryApplication;
        public List<SelectListItem> ArticleCategories;

        public EditModel (IArticleApplication articleApplication, IArticleCategoryApplication articleCategoryApplication) {
            _articleApplication = articleApplication;
            _articleCategoryApplication = articleCategoryApplication;
        }

        public void OnGet (long Id) {
            Article = _articleApplication.Get(Id);
            ArticleCategories = _articleCategoryApplication.List().Select(x => new SelectListItem(x.Title, x.Id.ToString())).ToList();
        }

        public RedirectToPageResult OnPost() {
            _articleApplication.Edit(Article);
            return RedirectToPage("./List");
        }
    }
}
