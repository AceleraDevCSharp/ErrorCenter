﻿using System.Threading.Tasks;

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

using ErrorCenter.Services.DTOs;
using ErrorCenter.WebAPI.ViewModel;
using ErrorCenter.Services.IServices;
using Microsoft.AspNetCore.Http;
using ErrorCenter.Services.Errors;

namespace ErrorCenter.WebAPI.Controllers {
  [Route("v1/[controller]")]
  [ApiController]
  public class UsersController : ControllerBase {
    private readonly IUsersService usersService;
    private readonly IMapper mapper;
    RoleManager<Persistence.EF.Models.Environment> roleManager;

    public UsersController(
      IUsersService usersService,
      IMapper mapper,
      RoleManager<Persistence.EF.Models.Environment> roleManager
    ) {
      this.usersService = usersService;
      this.mapper = mapper;
      this.roleManager = roleManager;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<UserViewModel>> Create(
      [FromBody] UserDTO newUser
    ) {
      newUser.Validate();

      if (newUser.Invalid) {
        throw new ViewModelException(
          "Error while trying to create new user",
          StatusCodes.Status400BadRequest,
          newUser.Notifications
        );
      }

      var user = await usersService.CreateNewUser(newUser);

      var createdUser = mapper.Map<UserViewModel>(user);

      return createdUser;
    }
  }
}
