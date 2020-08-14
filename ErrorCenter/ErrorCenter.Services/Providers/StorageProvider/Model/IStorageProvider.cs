using Microsoft.AspNetCore.Http;

namespace ErrorCenter.Services.Providers.StorageProvider.Model
{
    public interface IStorageProvider
    {
        public string SaveFile(IFormFile file);
        public void DeleteFile(string fileName);
    }
}
