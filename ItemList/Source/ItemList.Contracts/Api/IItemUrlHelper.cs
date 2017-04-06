using System;

namespace ItemList.Contracts.Api
{
    public interface IItemUrlHelper
    {
        string GetUrl(Guid id);
    }
}