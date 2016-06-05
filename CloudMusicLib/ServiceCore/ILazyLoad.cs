using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudMusicLib.ServiceCore
{
    public interface ILazyLoad<TValueType>
    {
        bool IsValueCreated();
        void LazyCreateValue();
    }
}
