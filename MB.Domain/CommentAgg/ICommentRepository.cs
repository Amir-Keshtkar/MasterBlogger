


using System.Collections.Generic;
using System.Xml.Linq;
using MB.Application.Contracts.Comment;

namespace MB.Domain.CommentAgg {
    public interface ICommentRepository {
        List<CommentViewModel> GetComments();
        void AddAndSave(Comment entity);
        Comment GetById(long id);
        void Save();
    }
}
