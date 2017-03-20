using System;
using System.Collections.Generic;
using ItemList.Api.Controllers;
using ItemList.Api.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace ItemList.Api.Tests
{
    [TestFixture]
    public class ItemsControllerTests
    {
        private ItemsController _itemsController;

        [SetUp]
        public void SetUp()
        {
            _itemsController = new ItemsController();
        }
    }
}
