using Framwork.Bus.Command;
using Framwork.Bus.Query;
using Modules.Product.Application.Contract.DTOs.Products.Create;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.Product.Application.Contract.UseCase.Products.Commands;

public record CreateProductCommand(CreateProductRequestDto request) : ICommand<bool>;
