using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudMusicLib.ServiceCore
{
    /*
     * * Невсегда ленивое создание
     */
    public abstract class LazyLoad<TValueType>
    {
        private TValueType _data;
        public TValueType GetData()
        {
            return GetDataAsync().Result;
        }
        public async Task<TValueType> GetDataAsync()
        {
            if (_data == null)
            {
                _data = await CreateAsync();
            }
            return _data;
        }
        protected virtual TValueType Create()
        {
            return CreateAsync().Result;
        }
        protected abstract Task<TValueType> CreateAsync();
        public void IntenseSet(TValueType data)
        {
            _data = data;
        }
    }
}
