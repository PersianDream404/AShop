using Framwork.Bus.Command;
using Modules.Product.Application.Contract.DTOs.ProductFeaturess.Create;
using Modules.Product.Application.Contract.DTOs.ProductFeaturess.Update;

namespace Modules.Product.Application.Contract.UseCase.ProductFeaturess.Commands;

public record CreateProductFeaturesCommand(CreateProductFeaturesRequestDto request) : ICommand<bool>;
public record UpdateProductFeaturesCommand(UpdateProductFeaturesRequestDto request) : ICommand<bool>;
public record DeleteProductFeaturesCommand(long Id) : ICommand<bool>;
public record ToggleProductFeaturesCommand(long Id) : ICommand<bool>;
