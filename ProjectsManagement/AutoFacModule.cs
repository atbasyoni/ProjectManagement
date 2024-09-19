using Autofac;
using AutoMapper;
using ProjectsManagement.Data;
using ProjectsManagement.Models;
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
            builder.RegisterType<UserState>().InstancePerLifetimeScope();
            builder.RegisterType<ControllereParameters>().InstancePerLifetimeScope();

            builder.Register(context => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<UserProfile>();
                cfg.AddProfile<RoleProfile>();
                cfg.AddProfile<ProjectProfile>();
                cfg.AddProfile<TaskProfile>();
            }).CreateMapper()).As<IMapper>().InstancePerLifetimeScope();
        }
    }
}
