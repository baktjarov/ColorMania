using Gameplay;
using UnityEngine;
using Zenject;

public class GameplayInstaller : MonoInstaller
{
    [SerializeField] private Drawable _drawable;
    [SerializeField] private Pen _pen;

    public override void InstallBindings()
    {
        Container.Bind<Drawable>().FromInstance(_drawable);
        Container.Bind<Pen>().FromInstance(_pen);
    }
}
