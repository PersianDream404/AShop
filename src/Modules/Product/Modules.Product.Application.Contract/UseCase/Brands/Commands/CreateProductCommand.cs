using Framwork.Bus.Command;
using Modules.Product.Application.Contract.DTOs.Brands.Create;
using Modules.Product.Application.Contract.DTOs.Brands.Update;

namespace Modules.Product.Application.Contract.UseCase.Brands.Commands;

public record CreateBrandCommand(CreateBrandRequestDto request) : ICommand<bool>;
public record UpdateBrandCommand(UpdateBrandRequestDto request) : ICommand<bool>;
public record DeleteBrandCommand(long Id) : ICommand<bool>;
