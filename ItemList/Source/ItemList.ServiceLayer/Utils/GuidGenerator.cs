using System;
using ItemList.Contracts.ServiceLayer;

namespace ItemList.ServiceLayer.Utils
{
    public class GuidGenerator : IGuidGenerator
    {
        public Guid GenerateGuid()
        {
            return new Guid();
        }
    }
}