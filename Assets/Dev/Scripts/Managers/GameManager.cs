public class GameManager : Singleton<GameManager>
{
    // [SerializeField] private SaveableData saveableData;
    public GameStates gameStates;
    public PlateManager[] allPlates;
    public float fireRate;
    public float damage;

    // private void Awake()
    // {
    //     LoadJson();
    //     fireRate = saveableData.fireRate;
    //     damage = saveableData.damage;
    // }

    private void Awake()
    {
        allPlates = FindObjectsOfType<PlateManager>();
    }

    internal void DamageUpdate(float value)
    {
        damage += value;
    }

    internal void FireRateUpdate(float value)
    {
        fireRate += value;
    }

    // private void SaveToJson()
    // {
    //     var saveData = JsonUtility.ToJson(this.saveableData);
    //     var filePath = Application.persistentDataPath + "/SaveData.json";
    //     System.IO.File.WriteAllText(filePath, saveData);
    // }
    //
    // private void LoadJson()
    // {
    //     var filePath = Application.persistentDataPath + "/SaveData.json";
    //     if(!System.IO.File.Exists(filePath)) return;
    //     var saveData = System.IO.File.ReadAllText(filePath);
    //     this.saveableData = JsonUtility.FromJson<SaveableData>(saveData);
    // }
    //
    // private void OnApplicationQuit()
    // {
    //     SaveToJson();
    // }
}

public enum GameStates{Wait, Clean, Shoot, LevelEnd}

// [Serializable]
// public struct SaveableData
// {
//     public float fireRate;
//     public float damage;
// }
