using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM.FrameWork.FMFrameWorkLazy.FMFrameWorkLazyDefinition
{
    /// <summary>
    /// 懒加载实现
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FMFrameWorkLazy<T> : IFMFrameWorkLazy<T>
    {
        private readonly System.Lazy<T> _value;

        public FMFrameWorkLazy(IServiceProvider serviceProvider)
        {
            _value = new System.Lazy<T>(() =>
            {
                return serviceProvider.GetService<T>();
            });
        }

        public T Value => _value.Value;
    }
}
