using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class UpgradeWindow : MonoBehaviour
{
    private List<CardView> cards = new List<CardView>();

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    [Inject]
    public void Construct(IUpgradeWindowPresenter presenter)
    {
        presenter.Register(this);
    }

    public void CreateCard(CardView card)
    {
        cards.Add(card);
    }

    public void ShowUpgradeWindow()
    {
        gameObject.SetActive(true);
    }

    public void CloseUpgradeWindow()
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