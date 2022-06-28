using UnityEngine;
using Zenject;
using System.Collections;
using System.Collections.Generic;

public class StoragesInstaller : MonoInstaller
{
    [SerializeField]
    private List<GameObject> backgrounds;
    [SerializeField]
    private List<Sprite> selectionIcon;
    [SerializeField]
    private List<Bubble> bubbles;

    public override void InstallBindings()
    {
        if (backgrounds != null)
        {
            BackgroundsStorage backgroundsStorage = new BackgroundsStorage(backgrounds);
            Container.Bind<BackgroundsStorage>().FromInstance(backgroundsStorage).AsSingle();
        }
        if (selectionIcon != null)
        {
            ImagesStorage imagesStorage = new ImagesStorage(selectionIcon);
            Container.Bind<ImagesStorage>().FromInstance(imagesStorage).AsSingle();
        }
        if (bubbles != null)
        {
            BubblesStorage bubblessStorage = new BubblesStorage(bubbles);
            Container.Bind<BubblesStorage>().FromInstance(bubblessStorage).AsSingle();
        }
    }
}