using Framwork.Bus.Command;
using Modules.Product.Application.Contract.DTOs.Colors.Create;
using Modules.Product.Application.Contract.DTOs.Colors.Update;

namespace Modules.Product.Application.Contract.UseCase.Colors.Commands;

public record CreateColorCommand(CreateColorRequestDto request) : ICommand<bool>;
public record UpdateColorCommand(UpdateColorRequestDto request) : ICommand<bool>;
public record DeleteColorCommand(long Id) : ICommand<bool>;
public record ToggleColorCommand(long Id) : ICommand<bool>;
