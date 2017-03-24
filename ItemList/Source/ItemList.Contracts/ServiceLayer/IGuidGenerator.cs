using System;

namespace ItemList.Contracts.ServiceLayer
{
    public interface IGuidGenerator
    {
        Guid GenerateGuid();
    }
}