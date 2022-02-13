using MB.Application.Contracts.ArticleCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MB.Presentation.MVCCore.Areas.Administrator.Pages.ArticleCategoryManagement {
    public class EditModel: PageModel {
        [BindProperty] public RenameArticleCategory ArticleCategory { get; set; }
        private readonly IArticleCategoryApplication _arttiArticleCategoryApplication;

        public EditModel(IArticleCategoryApplication articleCategoryApplication) {
            _arttiArticleCategoryApplication = articleCategoryApplication;
        }

        public void OnGet (long Id) {
            ArticleCategory = _arttiArticleCategoryApplication.Get(Id);
        }

        public RedirectToPageResult OnPost() {
            _arttiArticleCategoryApplication.Rename(ArticleCategory);
            return RedirectToPage("./List");
        }
    }
}
