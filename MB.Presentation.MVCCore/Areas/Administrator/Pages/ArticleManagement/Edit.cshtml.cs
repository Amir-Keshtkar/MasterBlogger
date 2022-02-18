using System.ComponentModel.DataAnnotations;
using System.Globalization;
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
        private readonly IWebHostEnvironment _hostEnvironment;
        [BindProperty]
        public IFormFile Image { get; set; }

        public EditModel (IArticleApplication articleApplication,
            IArticleCategoryApplication articleCategoryApplication, IWebHostEnvironment hostEnvironment) {
            _articleApplication = articleApplication;
            _articleCategoryApplication = articleCategoryApplication;
            _hostEnvironment = hostEnvironment;
        }

        public void OnGet (long id) {
            Article = _articleApplication.Get(id);
            ArticleCategories = _articleCategoryApplication.List().Where(x => x.IsDeleted == false)
                .Select(x => new SelectListItem(x.Title, x.Id.ToString())).ToList();
        }

        public async Task<RedirectToPageResult> OnPost () {
            if(ModelState.IsValid) {
                //Save image to wwwroot/image
                var wwwRootPath = _hostEnvironment.WebRootPath;
                var fileName = Path.GetFileNameWithoutExtension(Image.FileName);
                var extension = Path.GetExtension(Image.FileName);
                Article.Image = fileName = fileName +DateTime.Now.ToString("s").Replace(":","-")+ extension;
                var path = Path.Combine(wwwRootPath + "/img/", fileName);
                await using(var fileStream = new FileStream(path, FileMode.Create)) {
                     await Image.CopyToAsync(fileStream);
                }
                _articleApplication.Edit(Article);
            }
            //var file = Path.Combine(_hostEnvironment.WebRootPath, "img", Image.FileName);
            //using(var fileStream = new FileStream(file, FileMode.Create)) {
            //    Image.CopyToAsync(fileStream);
            //}
            //Article.Image = Image.FileName;

            return RedirectToPage("./List");
        }
    }
}
