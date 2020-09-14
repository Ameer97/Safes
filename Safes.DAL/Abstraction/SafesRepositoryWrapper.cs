using Safes.DAL.Contexts;
using Safes.DAL.Repositories;
//using Safes.DAL.Repositories;
using Safes.Infrastructure.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Safes.DAL.Abstraction
{
    public class SafesRepositoryWrapper : ISafesRepositoryWrapper
    {
        private IBoxRepository _boxRepository;
        private IThankRepository _thankRepository;
        private IStaticRepository _staticRepository;
        private IStaticBoxReuseRepository _staticBoxReuseRepository;
        private IUserRepository _userRepository;
        private IOwnerRepository _ownerRepository;
        private IMeditorRepository _meditorRepository;
        private IEventRepository _eventRepository;
        private bool _disposed;
        private SafesDbContext _context;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public SafesRepositoryWrapper(SafesDbContext context)
        {
            _context = context;
        }

        public IBoxRepository BoxRepository => _boxRepository ??= new BoxRepository(_context);
        public IThankRepository ThankRepository => _thankRepository ??= new ThankRepository(_context);

        public IStaticRepository StaticRepository => _staticRepository ??= new StaticRepository(_context);
        public IStaticBoxReuseRepository StaticBoxReuseRepository => _staticBoxReuseRepository ??= new StaticBoxReuseRepository(_context);

        public IUserRepository UserRepository => _userRepository ??= new UserRepository(_context);


        public IOwnerRepository OwnerRepository => _ownerRepository ??= new OwnerRepository(_context);

        public IMeditorRepository MeditorRepository => _meditorRepository ??= new MeditorRepository(_context);

        public IEventRepository EventRepository => _eventRepository ??= new EventRepository(_context);

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
                if (disposing)
                {
                    _userRepository?.Dispose();
                    _eventRepository?.Dispose();
                    _meditorRepository?.Dispose();
                    _ownerRepository?.Dispose();
                    _staticRepository?.Dispose();
                    _staticBoxReuseRepository?.Dispose();
                    _boxRepository?.Dispose();
                    _thankRepository?.Dispose();
                }

            _disposed = true;
        }
    }
}
