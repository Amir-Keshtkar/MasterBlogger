using System.Globalization;
using MB.Application.Contracts.Article;
using MB.Domain.ArticleAgg;
using Microsoft.EntityFrameworkCore;

namespace MB.Infrastructure.EFCore.Repositories {
    public class ArticleRepository: IArticleRepository {
        private readonly MasterBloggerContext _context;

        public ArticleRepository (MasterBloggerContext context) {
            _context = context;
        }

        public void CreateAndSave (Article entity) {
            _context.Articles.Add(entity);
            Save();
        }

        public List<ArticleViewModel> GetList () {
            return _context.Articles.Include(x => x.ArticleCategory).Select(x => new ArticleViewModel {
                Id = x.Id,
                Title = x.Title,
                ArticleCategory = x.ArticleCategory.Title,
                CreationDate = x.CreationDate.ToString(CultureInfo.InvariantCulture),
                IsDeleted = x.IsDeleted,
            }).OrderByDescending(x => x.Id).ToList();
        }

        public Article GetById (long Id) {
            return _context.Articles.FirstOrDefault(a => a.Id == Id);
        }

        public void Save () {
            _context.SaveChanges();
        }
    }
}
