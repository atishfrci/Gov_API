using ONLINEAPP.ANONYMOUS.BL.Operations;
using ONLINEAPP.ANONYMOUS.INTERFACE;
using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace ONLINEAPP.ANS
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            container.RegisterType<INews, NewsOperations>();
            container.RegisterType<IEvents, EventOperations>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}