using Autofac;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using MusicPlayer.Data;
using MusicPlayer.Infrastructure.Albums;
using MusicPlayer.Infrastructure.Blobs;
using MusicPlayer.Utilities.Helpers;

namespace MusicPlayer
{
    [UsedImplicitly]
    public class InfrastructureModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(AlbumService).Assembly)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces()
                .InstancePerRequest();
            builder.RegisterAssemblyTypes(typeof(AlbumRepository).Assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces()
                .InstancePerRequest();
            builder.RegisterType<ExpressionHelper>()
                .As<IExpressionHelper>()
                .InstancePerRequest();
            builder.RegisterType<MusicPlayerContext>()
                .As<DbContext>()
                .InstancePerDependency();
            builder.RegisterType<BlobStorageService>().AsImplementedInterfaces()
                .WithParameter(new TypedParameter(typeof(string), "Blobs__MusicPlayer"))
                .InstancePerRequest();
        }
    }
}
