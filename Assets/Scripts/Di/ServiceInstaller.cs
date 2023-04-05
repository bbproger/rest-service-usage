using Services;
using Zenject;

namespace Di
{
    public class ServiceInstaller : MonoInstaller<ServiceInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<IUserService>().To<UserService>().AsSingle().Lazy();
            Container.Bind<ITodoService>().To<TodoService>().AsSingle().Lazy();
            Container.Bind<IPhotosService>().To<PhotosService>().AsSingle().Lazy();
            
        }
    }
}