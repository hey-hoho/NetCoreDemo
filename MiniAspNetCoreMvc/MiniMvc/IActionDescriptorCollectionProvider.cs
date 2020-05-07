using System;
using System.Collections.Generic;
using System.Text;

namespace MiniAspNetCoreMvc.MiniMvc
{
    public interface IActionDescriptorCollectionProvider
    {
        IReadOnlyList<ActionDescriptor> ActionDescriptors { get; }
    }
}
