using System;

namespace MorningGirl.CrmWebApiSample
{
    class CrmWebApiRoot<T>
    {
        public string odatacontext { get; set; }
        public T[] value { get; set; }
    }
}
