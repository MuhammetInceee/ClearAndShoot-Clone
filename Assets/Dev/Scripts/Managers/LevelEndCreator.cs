using System;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelEndCreator : MonoBehaviour
{
    public int lineCount;

    public Transform startPos;
    public float defX;
    public float defZ;

    public GameObject prefab;

    private void Start()
    {
        Create();
    }
    
    private void Create()
    {
        for (var i = 0; i < lineCount; i++)
        {
            var position = startPos.position;
            var offset = Vector3.forward * defZ * i;

            var obj1 = InstantiateObject(position + offset);
            var obj2 = InstantiateObject(position + offset + Vector3.right * defX);
            var obj3 = InstantiateObject(position + offset + Vector3.left * defX);

            var health = CalculateHealth(i);
        
            SetHealthAndText(obj1, health);
            SetHealthAndText(obj2, health);
            SetHealthAndText(obj3, health);
        }
    }

    private GameObject InstantiateObject(Vector3 position)
    {
        return Instantiate(prefab, position, Quaternion.identity, transform);
    }

    private int CalculateHealth(int index)
    {
        return 50 + (index * 10) + Random.Range(30, 39);
    }

    private void SetHealthAndText(GameObject obj, int health)
    {
        var objScript = obj.GetComponent<LevelEndObjects>();
        objScript.health = health;
        objScript.UpdateText();
    }
}
