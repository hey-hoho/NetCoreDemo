using System;
using System.Collections.Generic;
using System.Text;

namespace MiniAspNetCoreMvc.MiniMvc
{
    public interface IActionDescriptorProvider
    {
        IEnumerable<ActionDescriptor> ActionDescriptors { get; }
    }
}
