using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Clear&Shoot/GateData/VerticalData", fileName = "GateVerticalData")]
public class GateVerticalMovementSO : ScriptableObject
{
    [SerializeField] private float duration;
    [SerializeField] private float moveDistance;
    [SerializeField] private float triggerValue;
    
    public Action<GateBase> update;

    private float _startTime;

    public GateVerticalMovementSO()
    {
        var isTriggered = false;
        
        update = (gateManager) =>
        {
            if(Vector3.Distance(gateManager.playerTr.position, gateManager.gameObject.transform.position) < triggerValue && !isTriggered)
            {
                isTriggered = true;
                _startTime = Time.time;
            }
            if(!isTriggered) return;
            
            var elapsedTime = Time.time - _startTime;
            var currentPos = gateManager.gameObject.transform.position;
            var t = elapsedTime / duration;
            
            var targetPos = Vector3.Lerp(gateManager.StartPos, gateManager.StartPos + (Vector3.forward * moveDistance), t);
            
            gateManager.gameObject.transform.position = new Vector3(currentPos.x, currentPos.y, targetPos.z);
        };
    }
}
