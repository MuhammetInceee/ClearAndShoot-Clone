using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Clear&Shoot/GateData/HorizontalData", fileName = "GateHorizontalData")]
public class GateHorizontalMovementSO : ScriptableObject
{
    [SerializeField] private float leftBorder;
    [SerializeField] private float rightBorder;
    [SerializeField] private float speed;
    [SerializeField] private float triggerValue;
    
    public Action<GateBase> update;
    
    private bool _isTriggered = false;

    public GateHorizontalMovementSO()
    {
        update = (gateManager) =>
        {
            if(Vector3.Distance(gateManager.playerTr.position, gateManager.gameObject.transform.position) < triggerValue) _isTriggered = true;
            if(!_isTriggered) return;
            
            var currentPos = gateManager.gameObject.transform.position;
            var duration = Mathf.PingPong(Time.time * speed, 1f);
            var targetX = Mathf.Lerp(leftBorder, rightBorder, duration);
            
            gateManager.gameObject.transform.position = new Vector3(targetX, currentPos.y, currentPos.z);
        };
    }
}
