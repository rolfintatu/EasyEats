using Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Aggregates.CatalogAggregate
{
    public class Image : AuditableEntity
    {

        public Image() { }

        public Image(int productId, string url)
            => (this.ProductId, this.Url) = (productId, url);

        public int Id { get; }
        public string Url { get; }

        public int ProductId { get; }
    }
}
