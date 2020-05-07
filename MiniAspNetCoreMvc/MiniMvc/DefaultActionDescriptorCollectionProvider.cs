using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniAspNetCoreMvc.MiniMvc
{
    public class DefaultActionDescriptorCollectionProvider : IActionDescriptorCollectionProvider
    {
        private readonly Lazy<IReadOnlyList<ActionDescriptor>> _accessor;

        public IReadOnlyList<ActionDescriptor> ActionDescriptors => _accessor.Value;

        /// <summary>
        /// 利用在构造函数中注入的IActionDescriptorProvider对象列表来提供描述Action的ActionDescriptor对象
        /// </summary>
        /// <param name="providers"></param>
        public DefaultActionDescriptorCollectionProvider(IEnumerable<IActionDescriptorProvider> providers)
            => _accessor = new Lazy<IReadOnlyList<ActionDescriptor>>(() => providers.SelectMany(it => it.ActionDescriptors).ToList());
    }
}
