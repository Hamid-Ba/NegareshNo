using NegareshNo.Core.ViewModels.Consultant;
using NegareshNo.Data.Model.Consulting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NegareshNo.Core.Services.ADT
{
    public interface IArticleService
    {
        Task<IEnumerable<ArticleAdminIndexVM>> GetAllArticleForAdmin();
        Task<IEnumerable<Article>> GetAllArticleForSite();

        Task<int> AddArticle(Article article);
        Task<int> UpdateArticle(Article article);
        Task DeleteArticle(int articleId);
        Task<Article> GetArticleById(int articleId);
        Task<bool> IsArticleExist(int articleId);
        Task<string> GetWriterNameOfArticle(int writerId);
        Task<string> GetWriterNameOfArticleById(int articleId);
    }
}
