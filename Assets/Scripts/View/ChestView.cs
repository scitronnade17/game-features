using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ChestView : MonoBehaviour
{
    [SerializeField] private Button buttonOpen;
    [SerializeField] private Text remainTimeText;
    [SerializeField] private Text nameChestText;
    [SerializeField] private Image image;

    private string id;
    private Sprite iconClosed;
    private Sprite iconOpened;

    private IChestBus chestBus;

    [Inject]
    public void Construct(IChestBus _chestBus)
    {
        chestBus = _chestBus;
    }

    private void Start()
    {
        buttonOpen.onClick.AddListener(Open);
    }

    public void Setup(string _id, string _name, Sprite _iconClosed, Sprite _iconOpened)
    {
        id = _id;
        nameChestText.text = _name;
        iconClosed = _iconClosed;
        iconOpened = _iconOpened;
        image.sprite = iconClosed;
    }

    public void UpdateState(ChestState chestState)
    {
        if (chestState.ReadyToOpen)
            ReadyState();
        else
            UpdateTimer(chestState.Timer.GetRemainTimeInSeconds());
    }

    public void UpdateTimer(double currentTime)
    {
        double remainSeconds = currentTime;

        if (remainSeconds <= 0)
        {
            remainTimeText.text = "00:00:00";
            ReadyState();
            return;
        }

        ShowTimer();
        remainTimeText.text = SecondsToFormat(remainSeconds);
    }

    private void Open()
    {
        chestBus.ChestOpenClick(id);
        image.sprite = iconClosed;
    }

    private void ShowTimer()
    {
        buttonOpen.gameObject.SetActive(false);
        remainTimeText.gameObject.SetActive(true);
    }

    private void ReadyState()
    {
        image.sprite = iconOpened;
        buttonOpen.gameObject.SetActive(true);
        remainTimeText.gameObject.SetActive(false);
    }

    private string SecondsToFormat(double seconds)
    {
        var timeSpan = TimeSpan.FromSeconds(seconds);
        return $"{timeSpan.TotalHours:00}:{timeSpan.Minutes:00}:{timeSpan.Seconds:00}";
    }
}