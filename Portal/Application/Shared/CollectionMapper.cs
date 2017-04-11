using System;
using System.Collections.Generic;
using System.Linq;

namespace Portal.Application.Shared
{
    public static class CollectionMapper
    {
        public static IEnumerable<TResult> ToMappedCollection<T, TResult>(
            this IEnumerable<T> collection, Func<T, TResult> castFunction) => 
            collection.Select(castFunction).ToList();
    }
}
