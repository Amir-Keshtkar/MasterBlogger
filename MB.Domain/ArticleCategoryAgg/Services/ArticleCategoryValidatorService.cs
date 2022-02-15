using System;

namespace MB.Domain.ArticleCategoryAgg.Services;

public class ArticleCategoryValidatorService: IArticleCategoryValidatorService {
    private readonly IArticleCategoryRepository _articleCategoryRepository;

    public ArticleCategoryValidatorService(IArticleCategoryRepository articleCategoryRepository) {
        _articleCategoryRepository = articleCategoryRepository;
    }

    public void CheckRecordExistence (string title) {
        if (_articleCategoryRepository.Exists(x => x.Title==title)) {
            throw new Exception("This record already exists in database");
        }
    }
}