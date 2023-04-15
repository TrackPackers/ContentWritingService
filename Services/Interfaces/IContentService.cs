using ContentWriterService.Models;

namespace ContentWriterService.Services.Interfaces
{
    public interface IContentService
    {
        public Task<Content> addContent(Content content);
    }
}
