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
        public TValueType Data
        {
            protected set
            {
                _data = value;
            }
            get
            {
                if (_data == null)
                {
                    _data = CreateAsync().Result;
                }
                return _data;
            }
        }
        public async Task<TValueType> GetDataAsync()
        {
            if (_data == null)
            {
                _data = await CreateAsync();
            }
            return _data;
        }
        protected abstract Task<TValueType> CreateAsync();
        public void IntenseSet(TValueType data)
        {
            _data = data;
        }
    }
}
