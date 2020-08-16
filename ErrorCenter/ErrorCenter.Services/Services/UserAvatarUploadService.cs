using System;
using System.IO;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

using ErrorCenter.Services.DTOs;
using ErrorCenter.Services.Errors;
using ErrorCenter.Services.IServices;
using ErrorCenter.Services.Providers.StorageProvider.Model;

namespace ErrorCenter.Services.Services
{
    public class UserAvatarUploadService : IUserAvatarUploadService
    {
        public IUsersRepository usersRepository;
        public IStorageProvider storageProvider;

        public UserAvatarUploadService(
          IUsersRepository usersRepository,
          IStorageProvider storageProvider
        )
        {
            this.usersRepository = usersRepository;
            this.storageProvider = storageProvider;
        }

        public async Task<string> UploadUserAvatar(
          string user_email,
          UserAvatarDTO file
        )
        {
            var user = await usersRepository.FindByEmailTracking(user_email);

            if (user == null)
            {
                throw new UserException(
                  "Requesting user is no longer valid",
                  StatusCodes.Status401Unauthorized
                );
            }

            if (!user.Avatar.Equals("default.png"))
                storageProvider.DeleteFile(user.Avatar);

            string avatarFileName = storageProvider.SaveFile(file.avatar);

            user.Avatar = Path.GetFileName(avatarFileName);

            await usersRepository.Save(user);

            return user.Avatar;
        }
    }
}
