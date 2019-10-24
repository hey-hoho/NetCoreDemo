using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ConsoleApp.CatFramework
{
    public class Demo
    {
        public void Run()
        {
            var root = new Cat()
                .Register<IFoo, Foo>(Lifetime.Transient)
                .Register<IBar>(_ => new Bar(), Lifetime.Self)
                .Register<IBaz, Baz>(Lifetime.Root)
                .Register(Assembly.GetEntryAssembly());
            var cat1 = root.CreateChild();
            var cat2 = root.CreateChild();

            void GetServices<TService>(Cat cat)
            {
                cat.GetService<TService>();
                cat.GetService<TService>();
            }

            GetServices<IFoo>(cat1);
            GetServices<IBar>(cat1);
            GetServices<IBaz>(cat1);
            GetServices<IGux>(cat1);
            Console.WriteLine();
            GetServices<IFoo>(cat2);
            GetServices<IBar>(cat2);
            GetServices<IBaz>(cat2);
            GetServices<IGux>(cat2);

            //using (var root = new Cat()
            //.Register<IFoo, Foo>(Lifetime.Transient)
            //.Register<IBar>(_ => new Bar(), Lifetime.Self)
            //.Register<IBaz, Baz>(Lifetime.Root)
            //.Register(Assembly.GetEntryAssembly()))
            //{
            //    using (var cat = root.CreateChild())
            //    {
            //        cat.GetService<IFoo>();
            //        cat.GetService<IBar>();
            //        cat.GetService<IBaz>();
            //        cat.GetService<IGux>();
            //        Console.WriteLine("Child cat is disposed.");
            //    }
            //    Console.WriteLine("Root cat is disposed.");
            //}

            var cat = new Cat()
           .Register<IFoo, Foo>(Lifetime.Transient)
           .Register<IBar, Bar>(Lifetime.Transient)
           .Register(typeof(IFoobar<,>), typeof(Foobar<,>), Lifetime.Transient);

            var foobar = (Foobar<IFoo, IBar>)cat.GetService<IFoobar<IFoo, IBar>>();
            Debug.Assert(foobar.Foo is Foo);
            Debug.Assert(foobar.Bar is Bar);

            var services = new Cat()
    .Register<Base, Foo>(Lifetime.Transient)
    .Register<Base, Bar>(Lifetime.Transient)
    .Register<Base, Baz>(Lifetime.Transient)
    .GetServices<Base>();
            Debug.Assert(services.OfType<Foo>().Any());
            Debug.Assert(services.OfType<Bar>().Any());
            Debug.Assert(services.OfType<Baz>().Any());
        }
    }

    public interface IFoo { }
    public interface IBar { }
    public interface IBaz { }
    public interface IGux { }
    public interface IFoobar<T1, T2> { }
    public class Base : IDisposable
    {
        public Base() => Console.WriteLine($"Instance of {GetType().Name} is created.");
        public void Dispose() => Console.WriteLine($"Instance of {GetType().Name} is disposed.");
    }

    public class Foo : Base, IFoo { }
    public class Bar : Base, IBar { }
    public class Baz : Base, IBaz { }
    [MapTo(typeof(IGux), Lifetime.Root)]
    public class Gux : Base, IGux { }
    public class Foobar<T1, T2> : IFoobar<T1, T2>
    {
        public IFoo Foo { get; }
        public IBar Bar { get; }
        public Foobar(IFoo foo, IBar bar)
        {
            Foo = foo;
            Bar = bar;
        }
    }
}
