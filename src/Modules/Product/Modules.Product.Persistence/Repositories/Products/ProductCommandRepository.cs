using Infrastructure.Repositories;
using Modules.Product.Domain.Entities.Products;
using Modules.Product.Domain.Interface;
using Modules.Product.Domain.Interface.Products;
using Modules.Product.Persistence.Context;
using SharedKernel.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.Product.Persistence.Repositories.Users;


public class ProductCommandRepository
    : CommandRepository<Modules.Product.Domain.Entities.Products.Product>, IProductCommandRepository
{
    public ProductCommandRepository(ProductWriteDbContext context) : base(context)
    {
    }

 
}
