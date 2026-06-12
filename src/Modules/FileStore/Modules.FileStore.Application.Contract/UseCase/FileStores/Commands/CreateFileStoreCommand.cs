using Framwork.Bus.Command;
using Modules.FileStore.Application.Contract.DTOs.FileStores.Create;
using Modules.FileStore.Application.Contract.DTOs.FileStores.Update;

namespace Modules.FileStore.Application.Contract.UseCase.FileStores.Commands;

public record CreateFileStoreCommand(CreateFileStoreRequestDto request) : ICommand<bool>;
public record UpdateFileStoreCommand(UpdateFileStoreRequestDto request) : ICommand<bool>;
public record DeleteFileStoreCommand(long Id) : ICommand<bool>;
