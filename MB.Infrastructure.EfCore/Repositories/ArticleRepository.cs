using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MB.Application.Contracts.Article;
using MB.Domain.ArticleAgg;
using MB.Domain.ArticleCategoryAgg;
using Microsoft.EntityFrameworkCore;

namespace MB.Infrastructure.EFCore.Repositories {
    public class ArticleRepository : IArticleRepository {
        private readonly MasterBloggerContext _context;

        public ArticleRepository(MasterBloggerContext context) {
            _context = context;
        }

        public void CreateAndSave (Article entity) {
            _context.Articles.Add(entity);
            _context.SaveChanges();
        }

        public List<ArticleViewModel> GetList () {
            return _context.Articles.Include(x => x.ArticleCategory).Select(x => new ArticleViewModel{
                Id = x.Id,
                Title = x.Title,
                ArticleCategory = x.ArticleCategory.Title,
                CreationDate = x.CreationDate.ToString(CultureInfo.InvariantCulture),
                IsDeleted = x.IsDeleted,
            }).ToList();
        }
        
    }
}
