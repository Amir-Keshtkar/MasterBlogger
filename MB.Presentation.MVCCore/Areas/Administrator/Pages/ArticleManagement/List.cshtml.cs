using MB.Application.Contracts.Article;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MB.Presentation.MVCCore.Areas.Administrator.Pages.ArticleManagement {
    public class ListModel: PageModel {
        public List<ArticleViewModel> Articles { get; set; }
        private readonly IArticleApplication _articleApplication;

        public ListModel (IArticleApplication articleApplication) {
            _articleApplication = articleApplication;
        }
        
        public void OnGet () {
            Articles =_articleApplication.GetList();
        }

        public RedirectToPageResult OnPostRemove(long Id) {
            _articleApplication.Remove(Id);
            return RedirectToPage("./List");
        }

        public RedirectToPageResult OnPostActivate(long Id) {
            _articleApplication.Activate(Id);
            return RedirectToPage("./List");
        }
    }
}
