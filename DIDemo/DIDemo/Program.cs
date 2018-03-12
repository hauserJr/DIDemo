using Microsoft.Extensions.DependencyInjection;
using System;

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
            private IPhone _phone;
            public People(IPhone phone) => this._phone = phone;

            public void Use<TPhone>(Action<IPhone> config)
                where TPhone : class, IPhone
            {
                config(this._phone);
            }
        }

        static void Main(string[] args)
        {
            ServiceProvider provider = new ServiceCollection()
                                    .AddSingleton<IPhone, iphone5S>()
                                    .AddSingleton<IPhone, iphone4S>()
                                    .AddSingleton<People>()
                                    .BuildServiceProvider();

            provider.GetService<People>().Use<IPhone>(phone => phone.Call("123456"));
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
