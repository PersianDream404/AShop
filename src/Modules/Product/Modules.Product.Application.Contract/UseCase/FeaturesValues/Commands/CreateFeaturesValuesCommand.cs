using Framwork.Bus.Command;
using Modules.Product.Application.Contract.DTOs.FeaturesValuess.Create;
using Modules.Product.Application.Contract.DTOs.FeaturesValuess.Update;

namespace Modules.Product.Application.Contract.UseCase.FeaturesValuess.Commands;

public record CreateFeaturesValuesCommand(CreateFeaturesValuesRequestDto request) : ICommand<bool>;
public record UpdateFeaturesValuesCommand(UpdateFeaturesValuesRequestDto request) : ICommand<bool>;
public record DeleteFeaturesValuesCommand(long Id) : ICommand<bool>;
public record ToggleFeaturesValuesCommand(long Id) : ICommand<bool>;
