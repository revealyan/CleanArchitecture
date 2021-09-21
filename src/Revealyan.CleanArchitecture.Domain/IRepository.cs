﻿namespace Revealyan.CleanArchitecture.Domain
{
    public interface IRepository<T> where T: IAggregateRoot
    {
        public IUnitOfWork UnitOfWork { get; }
    }
}
