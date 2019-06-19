using ACACLC.Application.Interfaces;
using Cloudcrate.AspNetCore.Blazor.Browser.Storage;

namespace ACACLC.Infrastructure.Storage
{
    public class BrowserStorage : IStorage
    {
        private readonly LocalStorage localStorage;

        public BrowserStorage(LocalStorage localStorage)
        {
            this.localStorage = localStorage;
        }

        public T Get<T>(string key) where T : class
        {
            return this.localStorage.GetItem<T>(key);
        }

        public void Set(string key, object item)
        {
            this.localStorage.SetItem(key, item);
        }
    }
}
