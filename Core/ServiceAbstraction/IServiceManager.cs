
using ServiceAbstraction;
using System;

namespace ServicesAbstraction
{
    public interface IServiceManager
    {
        IProductService ProductService { get; }
    }
}
