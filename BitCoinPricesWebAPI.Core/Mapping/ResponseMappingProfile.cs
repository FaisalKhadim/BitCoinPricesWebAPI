using AutoMapper;
using BitCoinPricesWebAPI.Core.Dtos;
using BitCoinPricesWebAPI.Core.Models;
namespace BitCoinPricesWebAPI.Core.Mapping
{
    public class ResponseMappingProfile:Profile
    {
        public ResponseMappingProfile()
        {
            CreateMap<BitStampResponseDTO,BitStampResponse>();
        }
    }
}
