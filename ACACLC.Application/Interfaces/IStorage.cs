using System;
using System.Collections.Generic;
using System.Text;

namespace ACACLC.Application.Interfaces
{
    public interface IStorage
    {
        T Get<T>(string key) where T : class;

        void Set(string key, object item);
    }
}
