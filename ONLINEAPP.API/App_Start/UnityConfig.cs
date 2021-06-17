using Microsoft.Practices.Unity;
using ONLINEAPP.HOME.BL.Operations;
using ONLINEAPP.HOME.INTERFACE;
using ONLINEAPP.TRANSPORTS.BL.Operations;
using ONLINEAPP.TRANSPORTS.INTERFACE;
using ONLINEAPP.GENERIC.BL.Operations;
using ONLINEAPP.GENERIC.INTERFACE;
using ONLINEAPP.EMAIL.INTERFACE;
using ONLINEAPP.EMAIL.BL.Operations;

namespace ONLINEAPP.API
{
    public static class UnityConfig
    {
        public static UnityContainer RegisterComponents()
        {

            var container = new UnityContainer();


            //ONLINE APP Site
            container.RegisterType<IUserInformationList, UserInformationListOperations>();
            //container.RegisterType<ITeamCenter, TeamCenterOperations>();
            container.RegisterType<ISiteGroup, SiteGroupOperations>();
            container.RegisterType<ITransport, TransportOperations>();
            container.RegisterType<IRegistrationRule, RegistrationRuleOperation>();
            container.RegisterType<INews, NewsOperations>();
            container.RegisterType<IEvents, EventOperations>();
            container.RegisterType<IEService, EServiceOperations>();
            container.RegisterType<IRequest, RequestOperations>();
            container.RegisterType<IEmailOperations, EmailOperations>();

            //GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
            return container;
        }
    }
}
