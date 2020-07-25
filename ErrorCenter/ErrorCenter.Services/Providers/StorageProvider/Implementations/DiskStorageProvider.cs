using System;
using System.IO;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

using ErrorCenter.Services.Providers.StorageProvider.Model;

namespace ErrorCenter.Services.Providers.StorageProvider.Implementations {
  public class DiskStorageProvider : IStorageProvider {
    private readonly IHostEnvironment hostEnvironment;
    private readonly string UsersAvatarsFolder;

    public DiskStorageProvider(IHostEnvironment hostEnvironment) {
      this.hostEnvironment = hostEnvironment;
      UsersAvatarsFolder = Path.Combine(
        this.hostEnvironment.ContentRootPath,
        "UsersAvatars"
      );
    }

    public string SaveFile(IFormFile file) {
      

      if (!Directory.Exists(UsersAvatarsFolder))
        Directory.CreateDirectory(UsersAvatarsFolder);

      using FileStream uploadedFile = File.Create(
        Path.Combine(
          UsersAvatarsFolder,
          Guid.NewGuid().ToString()
          + Path.GetExtension(file.FileName)
        )
      );
      file.CopyTo(uploadedFile);
      uploadedFile.Flush();

      return uploadedFile.Name;
    }

    public void DeleteFile(string fileName) {
      File.Delete(
        Path.Combine(
          UsersAvatarsFolder,
          fileName
        )
      );
    }
  }
}
