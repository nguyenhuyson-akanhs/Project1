using System.IO;
using UnityEngine;

public class SaveGameController : MonoBehaviour
{
    private string _SaveLocation;
    public InputText inputText;
    public InputText ipt2;
    private void Start()
    {
        _SaveLocation = Path.Combine(Application.persistentDataPath, "saveData.json");

        LoadGame();

        Debug.Log("Đường dẫn file save: " + _SaveLocation);
    }
    public void SaveGame()
    {
        SaveData saveData;

        // check if saveData does not exists => create new()
        if (File.Exists(_SaveLocation))
        {
            saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(_SaveLocation));
        }
        else
        {
            saveData = new();
        }

        saveData.StringMess1 = inputText?._StringText ?? saveData.StringMess1;
        saveData.StringMess2 = ipt2?._StringText ?? saveData.StringMess2;

        // save game json
        File.WriteAllText(_SaveLocation, JsonUtility.ToJson(saveData));
    }
    public void LoadGame()
    {
        if (File.Exists(_SaveLocation))
        {
            SaveData saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(_SaveLocation));

            if (inputText) inputText._StringText = saveData.StringMess1; // đ ghi đ hiểu :)??
            if (ipt2) ipt2._StringText = saveData.StringMess2;
            inputText.WriteString();
        }
        else SaveGame();
    }
}
