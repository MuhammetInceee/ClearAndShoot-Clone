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
        //TODO Refactor This Code
        for (var i = 0; i < lineCount; i++)
        {
            var position = startPos.position;
            
            var obj1 = Instantiate(prefab, position + Vector3.forward * defZ * i, Quaternion.identity, transform);
            var obj2 = Instantiate(prefab, position + Vector3.forward * defZ * i + Vector3.right * defX, Quaternion.identity, transform);
            var obj3 = Instantiate(prefab, position + Vector3.forward * defZ * i + Vector3.left * defX, Quaternion.identity, transform);

            var health = 50 + (i * 10) + Random.Range(30, 39);
            
            var obj1Script = obj1.GetComponent<LevelEndObjects>();
            var obj2Script = obj2.GetComponent<LevelEndObjects>();
            var obj3Script = obj3.GetComponent<LevelEndObjects>();
            
            obj1Script.health = health;
            obj1Script.UpdateText();
            obj2Script.health = health;
            obj2Script.UpdateText();
            obj3Script.health = health;
            obj3Script.UpdateText();
        }
    }
}
