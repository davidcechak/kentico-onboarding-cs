using System;
using ItemList.Contracts.Services;

namespace ItemList.Services.Wrappers
{
    internal class IdentifierService : IIdentifierService
    {
        public Guid GetIdentifier() => Guid.NewGuid();
    }
}