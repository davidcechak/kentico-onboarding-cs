using System;

namespace ItemList.Contracts.Models
{
    public class Item
    {
        public Guid Id { get; set; }

        public string Ueid { get; set; }

        public string Value { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastUpdated { get; set; }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(Ueid)}: {Ueid}, {nameof(Value)}: {Value}, {nameof(Created)}: {Created}, {nameof(LastUpdated)}: {LastUpdated}";
        }
    }
}