using MB.Application.Contracts.Article;
using MB.Application.Contracts.ArticleCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MB.Presentation.MVCCore.Areas.Administrator.Pages.ArticleManagement {
    public class CreateModel: PageModel {
        private readonly IArticleApplication _articleApplication;
        public List<SelectListItem> ArticleCategories;
        private readonly IArticleCategoryApplication _articleCategoryApplication;

        public CreateModel (IArticleApplication articleApplication, IArticleCategoryApplication categoryApplication) {
            _articleApplication = articleApplication;
            _articleCategoryApplication = categoryApplication;
        }

        public void OnGet () {
            ArticleCategories = _articleCategoryApplication.List().Where(x=>x.IsDeleted==false).Select(x=>new SelectListItem(x.Title,x.Id.ToString())).ToList();
        }

        public RedirectToPageResult OnPost(CreateArticle command) {
            _articleApplication.Create(command);
            return RedirectToPage("./List");
        }

    }
}
