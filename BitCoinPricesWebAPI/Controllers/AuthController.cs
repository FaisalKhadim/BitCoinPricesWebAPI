using AutoMapper;
using BitCoinPricesWebAPI.Core.Common;
using BitCoinPricesWebAPI.Core.Enums;
using BitCoinPricesWebAPI.Core.Models;
using BitCoinPricesWebAPI.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace BitCoinPricesWebAPI.Controllers;
[Route("api/auth")]
[ApiController]
public class AuthController : BaseApiController
{
    private readonly ISystemUserService SystemUserService;
    public AuthController(ILoggerManagerService loggerService, IMapper mapper, ISystemUserService _SystemUserService) : base(loggerService, mapper)
    {
        SystemUserService = _SystemUserService;
    }
    [HttpPost("login")]
    public async Task<IActionResult> Authenticate([FromBody] UserModel user)
    {
        ResponseModel responseModel = new ResponseModel();
        try
        {
            dynamic result = await SystemUserService.AuthenticateUser(user);
            if (result != "")
            {
                responseModel.StatusCode = 200;
                responseModel.Message = MessagesEnum.Success.ToString();
                responseModel.Data = result;
                return Ok(responseModel);
            }
            else
            {
                responseModel.StatusCode = 401;
                responseModel.Message = CommonMethods.GetEnumDescription(MessagesEnum.Failed);
                responseModel.Data = "Unauthorized";
                return Unauthorized(responseModel);
            }
        }
        catch (Exception ex)
        {
            _loggerService.LogError($"Something went wrong in the {nameof(Authenticate)} action {ex}");
            responseModel.StatusCode = 500;
            responseModel.Message = CommonMethods.GetEnumDescription(MessagesEnum.Failed);
            responseModel.Data = "Internal server error";
            return Ok(responseModel);
        }
    }
}