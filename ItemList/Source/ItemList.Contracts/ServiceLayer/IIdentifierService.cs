using System;

namespace ItemList.Contracts.ServiceLayer
{
    public interface IIdentifierService
    {
        Guid GetIdentifier();
    }
}