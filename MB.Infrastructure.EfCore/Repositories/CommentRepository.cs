﻿
using System.Globalization;
using _01_Framework.Infrastructure;
using MB.Application.Contracts.Comment;
using MB.Domain.CommentAgg;
using Microsoft.EntityFrameworkCore;

namespace MB.Infrastructure.EFCore.Repositories {
    public class CommentRepository: BaseRepository<long, Comment> ,  ICommentRepository {
        private readonly MasterBloggerContext _context;

        public CommentRepository (MasterBloggerContext context) : base(context) {
            _context = context;
        }

        public List<CommentViewModel> GetList () {
            return _context.Comments.Include(x => x.Article).Select(x => new CommentViewModel {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email,
                CreationDate = x.CreationDate.ToString(CultureInfo.InvariantCulture),
                Message = x.Message,
                Status = x.Status,
                Article = x.Article.Title,
            }).OrderByDescending(x => x.Id).ToList();
        }
    }
}
