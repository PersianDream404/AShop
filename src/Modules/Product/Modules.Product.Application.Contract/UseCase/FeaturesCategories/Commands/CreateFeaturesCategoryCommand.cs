using Framwork.Bus.Command;
using Modules.Product.Application.Contract.DTOs.FeaturesCategorys.Create;
using Modules.Product.Application.Contract.DTOs.FeaturesCategorys.Update;

namespace Modules.Product.Application.Contract.UseCase.FeaturesCategorys.Commands;

public record CreateFeaturesCategoryCommand(CreateFeaturesCategoryRequestDto request) : ICommand<bool>;
public record UpdateFeaturesCategoryCommand(UpdateFeaturesCategoryRequestDto request) : ICommand<bool>;
public record DeleteFeaturesCategoryCommand(long Id) : ICommand<bool>;
public record ToggleFeaturesCategoryCommand(long Id) : ICommand<bool>;
