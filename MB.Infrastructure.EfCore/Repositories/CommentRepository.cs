
using MB.Domain.CommentAgg;

namespace MB.Infrastructure.EFCore.Repositories {
    public class CommentRepository: ICommentRepository {
        private readonly MasterBloggerContext _context;

        public CommentRepository(MasterBloggerContext context) {
            _context = context;
        }

        public void AddAndSave (Comment command) {
            _context.Comments.Add(command);
            _context.SaveChanges();
        }
    }
}
