using System;
using Ninject;

namespace CrazyCart.Setup
{
    public class DependecyResolver
    {
        private static IKernel _kernel;

        public static void Initialize(IKernel kernel)
        {
            _kernel = kernel;
        }

        public static T Resolve<T>()
        {
            if (_kernel == null) 
                throw new KernelNotInitializedException();

            return _kernel.Get<T>();
        }

        public static object Resolve(Type type)
        {
            if (_kernel == null)
                throw new KernelNotInitializedException();

            return _kernel.Get(type);
        }
    }

    public class KernelNotInitializedException : Exception
    {
        public override string Message
        {
            get
            {
                return string.Format("Kernel needs to be initialized before usage. Exception message was: {0}",
                    base.Message);
            }
        }
    }
}