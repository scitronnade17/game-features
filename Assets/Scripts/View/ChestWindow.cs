using UnityEngine;
using Zenject;

public class ChestWindow : MonoBehaviour
{
    public Transform ChestParent => this.gameObject.transform;

    [Inject]
    public void Construct(IChestWindowPresenter presenter)
    {
        presenter.Register(this);
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void UpdateWindow()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}