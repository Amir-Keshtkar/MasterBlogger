using System;

namespace MB.Domain.ArticleCategoryAgg.Services;

public class ArticleCategoryValidatorService: IArticleCategoryValidatorService {
    private readonly IArticleCategoryRepository _articleCategoryRepository;

    public ArticleCategoryValidatorService(IArticleCategoryRepository articleCategoryRepository) {
        _articleCategoryRepository = articleCategoryRepository;
    }

    public void CheckRecordExistence (string Title) {
        if (_articleCategoryRepository.Exists(Title)) {
            throw new Exception("This record already exists in database");
        }
    }
}