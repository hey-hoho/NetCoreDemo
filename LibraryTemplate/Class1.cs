using System;

namespace LibraryTemplate
{
    public class Class1
    {
        public string GetAssemblyName()
        {
            return this.GetType().Assembly.GetName().Name;
        }
    }
}
