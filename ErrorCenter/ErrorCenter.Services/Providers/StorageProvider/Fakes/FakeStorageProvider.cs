using System;
using System.IO;
using System.Collections.Generic;

using Microsoft.AspNetCore.Http;

using ErrorCenter.Services.Providers.StorageProvider.Model;

namespace ErrorCenter.Services.Providers.StorageProvider.Fakes {
  public class FakeStorageProvider : IStorageProvider {
    private List<string> files = new List<string>();

    public string SaveFile(IFormFile file) {
      var fileName = Guid.NewGuid().ToString();
      files.Add(fileName);

      return fileName;
    }

    public void DeleteFile(string fileName) {
      var idx = files.FindIndex(file => file == fileName);
      files.RemoveAt(idx);
    }
  }
}