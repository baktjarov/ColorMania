using Gameplay;
using Interfaces;
using Services;
using UnityEngine;
using Zenject;

public class GameplayInstaller : MonoInstaller
{
    [SerializeField] private Drawable _drawable;
    [SerializeField] private Pen _pen;
    [SerializeField] ColorPickService _colorPickService;

    public override void InstallBindings()
    {
        InjectService.SetDIContainer(Container);

        Container.Bind<Drawable>().FromInstance(_drawable);
        Container.Bind<IColorPicker>().FromInstance(_colorPickService);
        Container.Bind<Pen>().FromInstance(_pen);
    }
}
