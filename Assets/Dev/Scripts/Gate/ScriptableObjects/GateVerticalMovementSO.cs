using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Clear&Shoot/GateData/VerticalData", fileName = "GateVerticalData")]
public class GateVerticalMovementSO : ScriptableObject
{
    [SerializeField] private float duration;
    [SerializeField] private float moveDistance;
    public Action<GateBase> update;

    private float startTime;

    public GateVerticalMovementSO()
    {
        var isTriggered = false;
        
        update = (gateManager) =>
        {
            if(Vector3.Distance(gateManager.playerTr.position, gateManager.gameObject.transform.position) < 10f && !isTriggered)
            {
                isTriggered = true;
                startTime = Time.time;
            }
            if(!isTriggered) return;
            
            var elapsedTime = Time.time - startTime;
            var currentPos = gateManager.gameObject.transform.position;
            // var targetZ = Mathf.MoveTowards(gateManager.startPos.z, gateManager.startPos.z + moveDistance, duration * Time.deltaTime);
            var t = elapsedTime / duration;
            
            var targetPos = Vector3.Lerp(gateManager.StartPos, gateManager.StartPos + (Vector3.forward * moveDistance), t);
            
            gateManager.gameObject.transform.position = new Vector3(currentPos.x, currentPos.y, targetPos.z);
        };
    }
}
