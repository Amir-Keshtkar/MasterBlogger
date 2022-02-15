using System.Security.Cryptography.X509Certificates;
using System.Xml;
using MB.Domain.ArticleCategoryAgg.Exceptions;

namespace MB.Domain.ArticleAgg.Services;

public class ArticleValidatorService: IArticleValidatorService {
    private readonly IArticleRepository _articleRepository;

    public ArticleValidatorService(IArticleRepository articleRepository) {
        _articleRepository = articleRepository;
    }

    public void CheckRecordExistence (string title) {
        if (_articleRepository.Exists(x=>x.Title==title)) {
            throw new DuplicatedRecordException();
        }
    }
}