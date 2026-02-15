using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private FloatingItem beltItem;
    [SerializeField] private GameObject beltEquipment;
    [SerializeField] private Player player;
    [SerializeField] private CharacterController characterController;
    private string _saveFilePath;
    private void Awake()
    {
        beltItem.gameObject.SetActive(true);
        beltEquipment.SetActive(false);
        _saveFilePath = Application.persistentDataPath + "/savefile.json";
    }
    private void Start()
    {
        LoadGame();
    }
    public void EquipItem()
    {
        beltEquipment.SetActive(true);
        beltItem.gameObject.SetActive(false);
    }
    public void SaveGame()
    {
        SaveData saveData = new SaveData(player.transform.position);
        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(_saveFilePath, json);
    }
    public void LoadGame()
    {
        if (File.Exists(_saveFilePath))
        {
            string json = File.ReadAllText(_saveFilePath);
            SaveData saveData = JsonUtility.FromJson<SaveData>(json);
            characterController.enabled = false;
            player.transform.position = saveData.GetPlayerPosition;
            characterController.enabled = true;
        }
    }
}
