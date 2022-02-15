using System.Collections.Generic;
using _01_Framework.Infrastructure;
using MB.Application.Contracts.Comment;
using MB.Domain.CommentAgg;

namespace MB.Application {
    public class CommentApplication: ICommentApplication {
        private readonly ICommentRepository _commentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CommentApplication(ICommentRepository commentRepository, IUnitOfWork unitOfWork) {
            _commentRepository = commentRepository;
            _unitOfWork = unitOfWork;
        }

        public List<CommentViewModel> GetComments () {
            return _commentRepository.GetList();
        }

        public void Add (AddComment command) {
            _unitOfWork.BeginTran();
            var comment = new Comment(command.Name, command.Email, command.Message, command.ArticleId);
            _commentRepository.Create(comment);
            _unitOfWork.Commit();
        }

        public void Cancel (long id) {
            _unitOfWork.BeginTran();
            var comment = _commentRepository.GetById(id);
            comment.Cancel();
            _unitOfWork.Commit();
        }

        public void Confirm (long id) {
            _unitOfWork.BeginTran();
            var comment = _commentRepository.GetById(id);
            comment.Confirm();
            _unitOfWork.Commit();
        }
    }
}
