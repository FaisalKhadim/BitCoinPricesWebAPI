using AutoMapper;
using BitCoinPricesWebAPI.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace BitCoinPricesWebAPI.Controllers;
public class BaseApiController : ControllerBase
{
    protected readonly ILoggerManagerService _loggerService;
    protected readonly IMapper _mapper;
    public BaseApiController(ILoggerManagerService loggerService, IMapper mapper)
    {
        _loggerService = loggerService;
        _mapper = mapper;
    }
}
