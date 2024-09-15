using Autofac;
using AutoMapper;
using ProjectsManagement.Data;
using ProjectsManagement.Profiles;
using ProjectsManagement.Repositories.Base;

namespace ProjectsManagement
{
    public class AutoFacModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Context>().InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();

            builder.Register(context => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<UserProfile>();
                cfg.AddProfile<RoleProfile>();
            }).CreateMapper()).As<IMapper>().InstancePerLifetimeScope();
        }
    }
}
