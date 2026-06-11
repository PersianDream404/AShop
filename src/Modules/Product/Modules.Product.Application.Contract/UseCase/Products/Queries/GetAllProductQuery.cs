using Framwork.Bus.Query;
using Framwork.PagedList;
using Modules.Product.Application.Contract.DTOs.Brands.GetAll;
using Modules.Product.Application.Contract.DTOs.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.Product.Application.Contract.UseCase.Products.Queries;

public record GetAllProductQuery(GetAllProductRequestDto request) : IQuery<PagedList<GetAllProductResponseDto>>;
