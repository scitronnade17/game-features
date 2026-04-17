using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SaveManager : MonoBehaviour
{
    [SerializeField] private Button saveButton;
    [SerializeField] private Button clearButton;

    private ISaveLoadService saveLoadService;

    [Inject]
    public void Construct(ISaveLoadService _saveLoadService)
    {
        saveLoadService = _saveLoadService;
    }

    private void Start()
    {
        saveButton.onClick.AddListener(() => saveLoadService.Save());
        clearButton.onClick.AddListener(ClearSave);
    }

    private void ClearSave()
    {
        PlayerPrefs.DeleteKey(ProgressService.ProgressKey);
        PlayerPrefs.Save();
    }
}