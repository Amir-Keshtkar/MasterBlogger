using MB.Domain.ArticleCategoryAgg;

namespace MB.Infrastructure.EFCore.Repositories {
    public class ArticleCategoryRepository: IArticleCategoryRepository {
        private readonly MasterBloggerContext _context;

        public ArticleCategoryRepository (MasterBloggerContext context) {
            _context = context;
        }


        public void Add (ArticleCategory category) {
            _context.ArticleCategories.Add(category);
            Save();
        }

        public bool Exists (string Title) { 
            return _context.ArticleCategories.Any(x => x.Title == Title);
        }

        public List<ArticleCategory> GetAll () {
            return _context.ArticleCategories.OrderByDescending(x => x.Id).ToList();
        }

        public ArticleCategory GetById (long id) {
            return _context.ArticleCategories.FirstOrDefault(x => x.Id == id);
        }

        public void Save () {
            _context.SaveChanges();
        }
    }
}