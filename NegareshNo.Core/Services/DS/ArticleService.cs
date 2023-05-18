using Microsoft.EntityFrameworkCore;
using NegareshNo.Core.Services.ADT;
using NegareshNo.Core.ViewModels.Consultant;
using NegareshNo.Data.Model.Consulting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NegareshNo.Core.Services.DS
{
    public class ArticleService : IArticleService
    {
        private readonly UnitOfWork.UnitOfWork UW;

        public ArticleService(UnitOfWork.UnitOfWork uw) => UW = uw;

        public async Task<IEnumerable<ArticleAdminIndexVM>> GetAllArticleForAdmin()
        {
            return await UW.Context.Articles.Include(c => c.Consultant).Select(a => new ArticleAdminIndexVM
            {
                ArticleId = a.ArticleId,
                ConsultantName = a.Consultant.ConsultantFullName,
                PublishedDate = a.PublishedDate,
                Summery = a.Summery,
                IsPublished = a.IsPublished,
                Title = a.Title
            }).ToListAsync();
        }

        public async Task<Article> GetArticleById(int articleId)
        {
            if (await IsArticleExist(articleId))
            {
                return await UW.GetRepository<Article>().GetEntityByIdAsync(articleId);
            }

            return null;
        }

        public async Task<int> AddArticle(Article article)
        {
            if (article != null)
            {
                article.PublishedDate = DateTime.Now;
                await UW.GetRepository<Article>().AddEntityAsync(article);
                await UW.CommitAsync();

                return article.ArticleId;
            }

            return 0;
        }

        public async Task<bool> IsArticleExist(int articleId) => await UW.Context.Articles.AnyAsync(a => a.ArticleId == articleId);

        public async Task<string> GetWriterNameOfArticle(int writerId) => await UW.Context.Consultants.Where(c => c.ConsultantId == writerId).Select(c => c.ConsultantFullName)
            .SingleOrDefaultAsync();

        public async Task<int> UpdateArticle(Article article)
        {
            if (article != null)
            {
                var oldArticle = await GetArticleById(article.ArticleId);

                if (!oldArticle.IsPublished) article.PublishedDate = DateTime.Now;

                oldArticle.ConsultingId = article.ConsultingId;
                oldArticle.Description = article.Description;
                oldArticle.IsPublished = article.IsPublished;
                oldArticle.PublishedDate = article.PublishedDate;
                oldArticle.Summery = article.Summery;
                oldArticle.Title = article.Title;

                UW.GetRepository<Article>().UpdateEntity(oldArticle);
                await UW.CommitAsync();

                return article.ArticleId;
            }

            return 0;
        }

        public async Task DeleteArticle(int articleId)
        {
            if (await IsArticleExist(articleId))
            {
                var article = await GetArticleById(articleId);
                article.IsDelete = true;
                await UW.CommitAsync();
            }
        }

        public async Task<IEnumerable<Article>> GetAllArticleForSite() => await UW.Context.Articles.Where(a => a.IsPublished).ToListAsync();

        public async Task<string> GetWriterNameOfArticleById(int articleId)
        {
            return await UW.Context.Articles.Include(c => c.Consultant).Where(a => a.ArticleId == articleId).Select(c =>
              c.Consultant.ConsultantFullName).SingleOrDefaultAsync();
        }

    }
}
