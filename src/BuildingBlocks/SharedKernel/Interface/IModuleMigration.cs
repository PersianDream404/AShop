namespace SharedKernel.Interface;

public interface IModuleMigration
{
    Task ApplyAsync(CancellationToken cancellationToken = default);
}
