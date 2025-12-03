using AutoMapper;
using Services.ServicesManagement.Application.Models.Abstract;
using Services.ServicesManagement.Domain.Pagination;


namespace Services.ServicesManagement.Application.Mapper
{
    public class PaginationParametersProfile : Profile
    {
        public PaginationParametersProfile()
        {
            CreateMap<PaginationParametersDto, PaginationParameters>()
                .ConvertUsing((src, context) =>
                {
                    // if dto is null or both null, return null
                    if (src == null || (!src.PageIndex.HasValue && !src.PageSize.HasValue))
                        return null;

                    // PageIndex has value, PageSize missing
                    if (src.PageIndex.HasValue && !src.PageSize.HasValue)
                    {
                        return new PaginationParameters
                        {
                            PageIndex = src.PageIndex.Value,
                        };
                    }

                    // PageSize has value, PageIndex missing
                    if (!src.PageIndex.HasValue && src.PageSize.HasValue)
                    {
                        return new PaginationParameters
                        {
                            PageSize = src.PageSize.Value
                        };
                    }

                    // both present
                    return new PaginationParameters
                    {
                        PageIndex = src.PageIndex.Value,
                        PageSize = src.PageSize.Value
                    };
                });

            // reverse map (optional)
            CreateMap<PaginationParameters, PaginationParametersDto>();
        }
    }

}
