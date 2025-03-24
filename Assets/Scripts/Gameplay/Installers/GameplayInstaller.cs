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

            // Other
            Container.Bind<QuestManager>().FromComponentInHierarchy().AsSingle();
        }
    }
}
