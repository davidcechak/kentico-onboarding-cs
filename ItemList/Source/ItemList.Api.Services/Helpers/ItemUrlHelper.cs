using System;
using System.Web.Http.Routing;
using ItemList.Contracts.Api;

namespace ItemList.Api.Services.Helpers
{
    internal class ItemUrlHelper : IItemUrlHelper
    {
        private readonly UrlHelper _urlHelper;

        public ItemUrlHelper(UrlHelper urlHelper)
        {
            _urlHelper = urlHelper;
        }

        public string GetUrl(Guid id)
        {
            return _urlHelper.Link(null, new {id});
        }
    }
}