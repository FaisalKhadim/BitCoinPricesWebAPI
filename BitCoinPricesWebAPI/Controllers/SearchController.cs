using AutoMapper;
using BitCoinPricesWebAPI.Core.Models;
using BitCoinPricesWebAPI.Service.Interfaces;
using BitCoinPricesWebAPI.Core.Enums;
using BitCoinPricesWebAPI.Core.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace BitCoinPricesWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SearchController : BaseApiController
    {
        private readonly ISearchService _searchService;
        public SearchController(ILoggerManagerService loggerService, IMapper mapper, ISearchService searchService) : base(loggerService, mapper)
        {
            _searchService = searchService;
        }
        [HttpGet("SelectAllSources")]
        public async Task<IActionResult> SelectAllSources()
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                dynamic graphData = await _searchService.SelectAllSources();
                responseModel.StatusCode = 200;
                responseModel.Message = MessagesEnum.Success.ToString();
                responseModel.Data = graphData;
                return Ok(responseModel);
            }
            catch (Exception ex)
            {
                _loggerService.LogError($"Something went wrong in the {nameof(SelectAllSources)} action {ex}");
                responseModel.StatusCode = 500;
                responseModel.Message = CommonMethods.GetEnumDescription(MessagesEnum.Failed);
                responseModel.Data = "Internal server error";
                return Ok(responseModel);
            }
        }
        [HttpPost("GetPricesAndInsertSourceData")]
        public async Task<IActionResult> GetPricesAndInsertbitstamp(string SourceEndPoint)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                var resultData = await _searchService.GetPricesAndInsertbitstamp(SourceEndPoint);
                
                if (resultData == "false")
                {
                    responseModel.StatusCode = 200;
                    responseModel.Message = MessagesEnum.InsertFailed.ToString();
                    responseModel.Data = resultData;
                    return Ok(responseModel);
                }
                else if (resultData == "")
                {
                    responseModel.StatusCode = 200;
                    responseModel.Message = MessagesEnum.Failed.ToString();
                    responseModel.Data = resultData;
                    return Ok(responseModel);
                }
                else
                {
                    responseModel.StatusCode = 200;
                    responseModel.Message = MessagesEnum.Success.ToString();
                    responseModel.Data = resultData;
                    return Ok(responseModel);
                }
            }
            catch (Exception ex)
            {
                _loggerService.LogError($"Something went wrong in the {nameof(GetPricesAndInsertbitstamp)} action {ex}");
                responseModel.StatusCode = 500;
                responseModel.Message = CommonMethods.GetEnumDescription(MessagesEnum.Failed);
                responseModel.Data = "Internal server error";
                return Ok(responseModel);
            }
        }
        [HttpPost("GetPricesAndInsertALL")]
        public async Task<IActionResult> GetPricesAndInsertALL(string SourceEndPoint)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                var resultData = await _searchService.GetPricesAndInsertALL(SourceEndPoint);
                if (resultData == "false") 
                {
                    responseModel.StatusCode = 200;
                    responseModel.Message = MessagesEnum.InsertFailed.ToString();
                    responseModel.Data = resultData;
                    return Ok(responseModel);
                }
                else if (resultData=="")
                {
                    responseModel.StatusCode = 200;
                    responseModel.Message = MessagesEnum.Failed.ToString();
                    responseModel.Data = resultData;
                    return Ok(responseModel);
                }
                else
                {
                    responseModel.StatusCode = 200;
                    responseModel.Message = MessagesEnum.Success.ToString();
                    responseModel.Data = resultData;
                    return Ok(responseModel);
                }
            }
            catch (Exception ex)
            {
                _loggerService.LogError($"Something went wrong in the {nameof(GetPricesAndInsertALL)} action {ex}");
                responseModel.StatusCode = 500;
                responseModel.Message = CommonMethods.GetEnumDescription(MessagesEnum.Failed);
                responseModel.Data = "Internal server error";
                return Ok(responseModel);
            }
        }
        [HttpGet("GetHistroy")]
        public async Task<IActionResult> GetHistroy()
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                IEnumerable<dynamic> HistData = await _searchService.GetHistroy();
                //IEnumerable<BitStampResponseDTO> ResponseDto = _mapper.Map<IEnumerable<BitStampResponseDTO>>(HistData);
                responseModel.StatusCode = 200;
                responseModel.Message = MessagesEnum.Success.ToString();
                responseModel.Data = HistData;
                return Ok(responseModel);
            }
            catch (Exception ex)
            {
                _loggerService.LogError($"Something went wrong in the {nameof(GetHistroy)} action {ex}");
                responseModel.StatusCode = 500;
                responseModel.Message = CommonMethods.GetEnumDescription(MessagesEnum.Failed);
                responseModel.Data = "Internal server error";
                return Ok(responseModel);
            }
        }
    }
}
