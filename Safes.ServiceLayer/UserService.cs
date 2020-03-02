using Safes.Infrastructure.Interfaces.Repositories;
using Safes.Infrastructure.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Safes.ServiceLayer
{
    public class UserService : IUserService
    {
        private readonly ISafesRepositoryWrapper _repositoryWrapper;
        private bool _disposed;

        public UserService(ISafesRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
                if (disposing)
                    _repositoryWrapper.Dispose();
            _disposed = true;
        }
    }
}
