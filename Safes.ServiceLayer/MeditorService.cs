using Safes.Infrastructure.Interfaces.Repositories;
using Safes.Infrastructure.Interfaces.Services;
using System;

namespace Safes.ServiceLayer
{
    public class MeditorService : IMeditorService
    {
        private readonly ISafesRepositoryWrapper _repositoryWrapper;
        private bool _disposed;

        public MeditorService(ISafesRepositoryWrapper repositoryWrapper)
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
