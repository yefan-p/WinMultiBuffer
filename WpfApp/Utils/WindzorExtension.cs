using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace MultiBuffer.WpfApp.Utils
{
    public static class WindzorExtension
    {
        public static void RegisterService<TService, TImpl>(this IWindsorContainer container)
            where TImpl : TService
            where TService : class
        {
            container.Register(Component
                  .For<TService>()
                  .ImplementedBy<TImpl>()
                  .LifestyleTransient());
        }

        public static void RegisterSingleton<TService, TImpl>(this IWindsorContainer container)
            where TImpl : TService
            where TService : class
        {
            container.Register(Component
                  .For<TService>()
                  .ImplementedBy<TImpl>()
                  .LifestyleSingleton());
        }
    }
}