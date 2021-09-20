using Autofac;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using WebApplication.OrderingAPI.Applications.Commands;

namespace WebApplication.OrderingAPI.Infrastructure.AutofacModules
{
    public class MediatorModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
                    .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(typeof(CreateOrderCommand).GetTypeInfo().Assembly)
                    .AsClosedTypesOf(typeof(IRequestHandler<,>));
            
            builder.RegisterType(typeof(IdentifiedCommandHandler<CancelOrderCommand>))
                    .As<IRequestHandler<IdentifiedCommand<CancelOrderCommand>, bool>>()
                    .AsImplementedInterfaces();

            builder.Register<ServiceFactory>(context =>
            {
                var componentContext = context.Resolve<IComponentContext>();
                return t => { object o; return componentContext.TryResolve(t, out o) ? o : null; };
            });
        }
    }
}
