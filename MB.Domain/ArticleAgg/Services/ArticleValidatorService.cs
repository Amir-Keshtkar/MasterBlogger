using MB.Domain.ArticleCategoryAgg.Exceptions;

namespace MB.Domain.ArticleAgg.Services;

public class ArticleValidatorService: IArticleValidatorService {
    private readonly IArticleRepository _articleRepository;

    public ArticleValidatorService(IArticleRepository articleRepository) {
        _articleRepository = articleRepository;
    }

    public void CheckRecordExistence (string Title) {
        if (_articleRepository.Exist(Title)) {
            throw new DuplicatedRecordException();
        }
    }
}