using System;

namespace LibraryTemplate
{
    public class Class1
    {
        ITemplateService _service;

        public Class1(ITemplateService service)
        {
            _service = service;
        }

        public string GetAssemblyName()
        {
            return _service.GetName();
            return this.GetType().Assembly.GetName().Name;
        }
    }

    public interface ITemplateService
    {
        string GetName();
    }

    public class TemplateService : ITemplateService
    {
        ITemplateService2 _service2;
        public TemplateService(ITemplateService2 service2)
        {
            _service2 = service2;
        }

        public string GetName()
        {
            return "hoho123   "+_service2.GetName();
        }
    }

    public interface ITemplateService2
    {
        string GetName();
    }

    public class TemplateService2 : ITemplateService2
    {
        public string GetName()
        {
            return "TemplateService2";
        }
    }
}
