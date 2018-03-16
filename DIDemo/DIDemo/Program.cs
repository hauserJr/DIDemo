using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace DIDemo
{
    class Program
    {
        public interface IPhone
        {
            string Call(string Num);
        }
        public class People
        {
            private readonly Func<Type, IPhone> phoneFactory;
            private IPhone _phone;

            public People(Func<Type, IPhone> phoneFactory)
            {
                this.phoneFactory = phoneFactory;
            }

            public void Use<TPhone>(Action<IPhone> config) 
                where TPhone : class, IPhone
            {
                _phone = phoneFactory(typeof(TPhone));
                config(this._phone);
            }
        }
        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            services.AddSingleton<People>()
                .AddTransient(factory => (Func<Type, IPhone>)(type => (IPhone)factory.GetService(type)));

            foreach (var iphoneType in Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => !t.IsAbstract && typeof(IPhone).IsAssignableFrom(t)))
            {
                services.AddTransient(iphoneType);
            }

            var provider = services.BuildServiceProvider();
            provider.GetService<People>().Use<iphone4S>(phone => phone.Call("123456"));
            Console.ReadLine();
        }

        public class iphone4S : IPhone
        {
            public string Call(string Num)
            {
                Console.WriteLine("IPhone4S Call to：" + Num);
                return "撥號中";
            }
        }

        public class iphone5S : IPhone
        {
            public string Call(string Num)
            {
                Console.WriteLine("IPhone5S Call to ：" + Num);
                return "撥號中";
            }
        }
    }
}
