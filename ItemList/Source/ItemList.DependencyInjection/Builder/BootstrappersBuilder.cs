using System;
using System.Collections.Generic;
using System.Linq;
using ItemList.Contracts.DependencyInjection;

namespace ItemList.DependencyInjection.Builder
{
    internal class BootstrappersBuilder : IBootstrappersBuilder
    {
        private readonly List<Func<IBootstrapper>> _list = new List<Func<IBootstrapper>>();
        private bool _isSealed;

        public static IBootstrappersBuilder Create() => new BootstrappersBuilder();

        private BootstrappersBuilder()
        {
        }

        public IBootstrappersBuilder Include<TRegistration>() where TRegistration : IBootstrapper, new()
        {
            if (_isSealed)
            {
                throw new InvalidOperationException("Include is not supported after Seal has been called.");
            }
            _list.Add(() => new TRegistration());
            return this;
        }

        public IEnumerable<IBootstrapper> AsEnumerable()
        {
            _isSealed = true;
            return _list
                .Select(creator => creator())
                .ToList()
                .AsReadOnly();
        }
    }
}