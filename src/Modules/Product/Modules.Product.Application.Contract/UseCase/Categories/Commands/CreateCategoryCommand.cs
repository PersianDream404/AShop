using Framwork.Bus.Command;
using Modules.Product.Application.Contract.DTOs.Categorys.Create;
using Modules.Product.Application.Contract.DTOs.Categorys.Update;

namespace Modules.Product.Application.Contract.UseCase.Categorys.Commands;

public record CreateCategoryCommand(CreateCategoryRequestDto request) : ICommand<bool>;
public record UpdateCategoryCommand(UpdateCategoryRequestDto request) : ICommand<bool>;
public record DeleteCategoryCommand(long Id) : ICommand<bool>;
public record ToggleCategoryCommand(long Id) : ICommand<bool>;
