using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LevelUpWindow : MonoBehaviour
{
    private List<CardView> cards = new List<CardView>();

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    [Inject]
    public void Construct(ILevelUpWindowPresenter presenter)
    {
        presenter.Register(this);
    }

    public void ShowLevelUpWindow()
    {
        gameObject.SetActive(true);
    }

    public void CloseLevelUpWindow()
    {
        DestroyCards();
        gameObject.SetActive(false);
    }

    private void DestroyCards()
    {
        for (int i = 0; i < cards.Count; i++)
            cards[i].DestroyCard();

        cards.Clear();
    }
}