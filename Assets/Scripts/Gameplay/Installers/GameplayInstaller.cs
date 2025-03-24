using Critsoft.ForgeSim2025.Gameplay.Controllers;
using Critsoft.ForgeSim2025.Gameplay.Models;
using Critsoft.ForgeSim2025.Gameplay.Quests;
using Critsoft.ForgeSim2025.Gameplay.Views;
using UnityEngine;
using Zenject;

namespace Critsoft.ForgeSim2025.Gameplay.Installers
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _fallingResourcePrefab;
        [SerializeField] private Quest _questPrefab;
        [SerializeField] private Transform _questGrid;

        public override void InstallBindings()
        {
            // Models, Views, Controllers
            Container.Bind<InventoryModel>().AsSingle().NonLazy();
            Container.Bind<InventoryView>().FromComponentInHierarchy().AsSingle();
            Container.Bind<InventoryController>().FromComponentInHierarchy().AsSingle();

            // Factories / Prefabs
            Container.BindFactory<Quest, Quest.Factory>()
                .FromComponentInNewPrefab(_questPrefab)
                .UnderTransform(_questGrid);

            Container.BindMemoryPool<FallingResource, FallingResource.Pool>()
                .WithInitialSize(GameConfig.FallingResourcesMemoryPoolSize)
                .FromComponentInNewPrefab(_fallingResourcePrefab)
                .UnderTransformGroup(GameConfig.FallingResourcesMemoryPoolId);

            // Other
            Container.Bind<ResourceSpawner>().FromComponentInHierarchy().AsSingle();
            Container.Bind<QuestManager>().FromComponentInHierarchy().AsSingle();
        }
    }
}
