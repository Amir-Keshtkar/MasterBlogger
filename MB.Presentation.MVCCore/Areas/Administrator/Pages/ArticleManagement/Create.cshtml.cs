using MB.Application.Contracts.Article;
using MB.Application.Contracts.ArticleCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Web;

namespace MB.Presentation.MVCCore.Areas.Administrator.Pages.ArticleManagement {
    public class CreateModel: PageModel {
        private readonly IArticleApplication _articleApplication;
        public List<SelectListItem> ArticleCategories;
        private readonly IArticleCategoryApplication _articleCategoryApplication;

        private readonly IWebHostEnvironment _hostEnvironment;
        [BindProperty]
        public IFormFile Image { get; set; }

        public CreateModel (IArticleApplication articleApplication, IArticleCategoryApplication categoryApplication, IWebHostEnvironment hostEnvironment) {
            _articleApplication = articleApplication;
            _articleCategoryApplication = categoryApplication;
            _hostEnvironment = hostEnvironment;
        }

        public void OnGet () {
            ArticleCategories = _articleCategoryApplication.List().Where(x => x.IsDeleted == false).Select(x => new SelectListItem(x.Title, x.Id.ToString())).ToList();
        }


        public async Task<RedirectToPageResult> OnPost (CreateArticle command) {

            if(ModelState.IsValid) {
                //Save image to wwwroot/image
                var wwwRootPath = _hostEnvironment.WebRootPath;
                var fileName = Path.GetFileNameWithoutExtension(Image.FileName);
                var extension = Path.GetExtension(Image.FileName);
                command.Image = fileName = fileName + DateTime.Now.ToString("s").Replace(":","-") + extension;
                var path = Path.Combine(wwwRootPath + "/img/", fileName);
                await using var fileStream = new FileStream(path, FileMode.Create);
                await Image.CopyToAsync(fileStream);
                _articleApplication.Create(command);
            }
            //var file = Path.Combine(_hostEnvironment.WebRootPath, "img", Image.FileName);
            //using(var fileStream = new FileStream(file, FileMode.Create)) {
            //    Image.CopyToAsync(fileStream);
            //}

            //command.Image = Image.Name;
            return RedirectToPage("./List");
            //return RedirectToPage(nameof(ListModel));
        }

    }
}

