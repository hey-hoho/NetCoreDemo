using System;
using System.Collections.Generic;
using System.Text;

namespace MiniAspNetCoreMvc.MiniMvc
{
    public abstract class Controller
    {
        public ActionContext ActionContext { get; internal set; }
    }
}
