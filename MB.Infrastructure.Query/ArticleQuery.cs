﻿using System.Globalization;
using MB.Domain.CommentAgg;
using MB.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;

namespace MB.Infrastructure.Query;

public class ArticleQuery: IArticleQuery {
    private readonly MasterBloggerContext _context;

    public ArticleQuery (MasterBloggerContext context) {
        _context = context;
    }

    public List<ArticleQueryView> GetArticles () {
        return _context.Articles.Where(z=>z.IsDeleted==false)
            .Include(x => x.ArticleCategory)
            .Include(x => x.Comments)
            .Select(x => new ArticleQueryView {
                Id = x.Id,
                Title = x.Title,
                ArticleCategory = x.ArticleCategory.Title,
                CreationDate = x.CreationDate.ToString(CultureInfo.InvariantCulture),
                ShortDescription = x.ShortDescription,
                Image = x.Image,
                CommentsCount = x.Comments.Count(z => z.Status == Statuses.Confirmed),
            }).ToList();
    }

    public ArticleQueryView GetArticle (long id) {
        return _context.Articles
            .Include(x => x.ArticleCategory)
            .Select(x => new ArticleQueryView {
                Id = x.Id,
                Title = x.Title,
                ArticleCategory = x.ArticleCategory.Title,
                CreationDate = x.CreationDate.ToString(CultureInfo.InvariantCulture),
                ShortDescription = x.ShortDescription,
                Image = x.Image,
                Content = x.Content,
                CommentsCount = x.Comments.Count(z => z.Status == Statuses.Confirmed),
                Comments = MapComments(x.Comments.Where(z => z.Status == Statuses.Confirmed)),
            }).FirstOrDefault(x => x.Id == id);
    }

    private static List<CommentQueryView> MapComments (IEnumerable<Comment> enumerable) {
        return enumerable.Select(comment => new CommentQueryView {
            Name = comment.Name,
            Message = comment.Message,
            CreationDate = comment.CreationDate.ToString(CultureInfo.InvariantCulture),
        }).ToList();
    }

}