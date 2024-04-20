﻿namespace PeopleDictionary.Core.Base
{
    public class PagedResult<T>
    {
        public IEnumerable<T>? Items { get; set; }
        public int TotalCount { get; set; }
    }
}
