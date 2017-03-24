using System;

namespace ItemList.Api.Helpers
{
    public interface IItemUrlHelper
    {
        string GetUrl(Guid id);
    }
}