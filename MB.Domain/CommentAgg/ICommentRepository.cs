


using System.Collections.Generic;
using MB.Application.Contracts.Comment;

namespace MB.Domain.CommentAgg {
    public interface ICommentRepository {
        List<CommentViewModel> GetComments();
        void AddAndSave(Comment entity);
    }
}
