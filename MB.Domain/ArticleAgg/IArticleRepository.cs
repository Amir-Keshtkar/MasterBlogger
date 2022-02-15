﻿using System.Collections.Generic;
using _01_Framework.Infrastructure;
using MB.Application.Contracts.Article;

namespace MB.Domain.ArticleAgg {
    public interface IArticleRepository : IRepository<long, Article> {
        public List<ArticleViewModel> GetList();
    }
}
