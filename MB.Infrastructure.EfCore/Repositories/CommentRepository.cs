
using System.Globalization;
using MB.Application.Contracts.Comment;
using MB.Domain.CommentAgg;
using Microsoft.EntityFrameworkCore;

namespace MB.Infrastructure.EFCore.Repositories {
    public class CommentRepository: ICommentRepository {
        private readonly MasterBloggerContext _context;

        public CommentRepository (MasterBloggerContext context) {
            _context = context;
        }

        public void AddAndSave (Comment command) {
            _context.Comments.Add(command);
            Save();
        }

        public Comment GetById (long id) {
             return _context.Comments.FirstOrDefault(c => c.Id == id);
        }

        public void Save () {
            _context.SaveChanges();
        }

        public List<CommentViewModel> GetComments () {
            return _context.Comments.Include(x => x.Article).Select(x => new CommentViewModel {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email,
                CreationDate = x.CreationDate.ToString(CultureInfo.InvariantCulture),
                Message = x.Message,
                Status = x.Status,
                Article = x.Article.Title,
            }).ToList();
        }
    }
}
