using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sts.Framework.Core.Constants;
using Sts.Framework.Core.Filter;
using Sts.Framework.Core.Interfaces;
using Sts.Framework.Core.Models.Inputs;
using Sts.Framework.Core.Models.Responses;
using Sts.Framework.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace Sts.Framework.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = "Bearer")]
public class UserProfileController : ControllerBase
{
    private readonly IUserService _userService;
    public UserProfileController(IUserService userService)
    {
        _userService = userService;
    }

    [HasPermission(Permissions = $"{CommonConstants.USER_PROFILE_VIEW_PERMISSION},{CommonConstants.USER_PROFILE_EDIT_PERMISSION}")]
    [HttpGet("{id}")]
    public async Task<BaseResponse> GetAsync([FromRoute] Guid id, CancellationToken token)
    {
        var response = await _userService.GetProfileAsync(id, token);
        return response;
    }

    [HasPermission(Permissions = $"{CommonConstants.USER_PROFILE_EDIT_PERMISSION}")]
    [HttpPost]
    public async Task<BaseResponse> AddAsync([FromBody] UserUpdateModel user)
    {
        var result = await _userService.AddProfileAsync(user);

        return result;
    }

    [HasPermission(Permissions = $"{CommonConstants.USER_PROFILE_EDIT_PERMISSION}")]
    [HttpPut("{id}")]
    public async Task<BaseResponse<User>> UpdateAsync([FromRoute] Guid id, [FromBody] UserUpdateModel user)
    {
        var result = await _userService.UpdateProfileAsync(id, user);

        return result;
    }

    [HasPermission(Permissions = $"{CommonConstants.USER_PROFILE_EDIT_PERMISSION}")]
    [HttpDelete("{id}")]
    public async Task<BaseResponse> UpdateAsync([FromRoute] Guid id)
    {
        var result = await _userService.DeleteProfileAsync(id);

        return result;
    }
}