using System;

namespace Safes.Infrastructure.Interfaces.Repositories
{
    public interface ISafesRepositoryWrapper : IDisposable
    {
        IBoxRepository BoxRepository { get; }
        IStaticRepository StaticRepository { get; }
        IStaticBoxReuseRepository StaticBoxReuseRepository { get; }
        IUserRepository UserRepository { get; }
        IOwnerRepository OwnerRepository { get; }
        IMeditorRepository MeditorRepository { get; }
        IEventRepository EventRepository { get; }
    }
}
