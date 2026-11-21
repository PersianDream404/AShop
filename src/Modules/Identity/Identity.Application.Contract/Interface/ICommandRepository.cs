namespace Identity.Application.Contract.Interface;

public interface ICommandRepository<T> : Identity.Domain.Interface.ICommandRepository<T>
    where T : class
{
}
