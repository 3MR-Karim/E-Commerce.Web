
using AutoMapper;
using DomainLayer.Entities;
using Microsoft.AspNetCore.Http;
using Shared.DataTransferObjects;

namespace Presentation.Resolvers
{
    public class ProductPictureUrlResolver
        : IValueResolver<Product, ProductDto, string>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductPictureUrlResolver(
            IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string Resolve(
            Product source,
            ProductDto destination,
            string destMember,
            ResolutionContext context)
        {
            if (string.IsNullOrWhiteSpace(source.PictureUrl))
                return string.Empty;

            var request = _httpContextAccessor.HttpContext?.Request;

            if (request is null)
                return source.PictureUrl;

            return $"{request.Scheme}://{request.Host}/" +
                   source.PictureUrl.TrimStart('/');
        }
    }
}
