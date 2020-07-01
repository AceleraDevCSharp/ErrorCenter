using System;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;

using ErrorCenter.Domain;
using ErrorCenter.Persistence.EF.Context;
using ErrorCenter.UserScope.DTOs;

namespace ErrorCenter.UserScope {
	public class AuthenticateUserService {
		private readonly IConfiguration Configuration;
		public AuthenticateUserService() {
			//
    }

		public async Task<User> Execute(AuthenticateUserDTO data) {
			//
			return new User();
		}
	}
}