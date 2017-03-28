using System;

namespace ItemList.Contracts.Services
{
    public interface IIdentifierService
    {
        Guid GetIdentifier();
    }
}