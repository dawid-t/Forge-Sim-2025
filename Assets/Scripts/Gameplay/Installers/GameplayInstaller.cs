using Critsoft.ForgeSim2025.Gameplay.Controllers;
using Critsoft.ForgeSim2025.Gameplay.Models;
using Critsoft.ForgeSim2025.Gameplay.Views;
using Zenject;

namespace Critsoft.ForgeSim2025.Gameplay.Installers
{
    public class GameplayInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            // Models, Views, Controllers
            Container.Bind<InventoryModel>().AsSingle().NonLazy();
            Container.Bind<InventoryView>().FromComponentInHierarchy().AsSingle();
            Container.Bind<InventoryController>().FromComponentInHierarchy().AsSingle();
        }
    }
}
