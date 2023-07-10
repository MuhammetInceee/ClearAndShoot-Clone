using System;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private GameManager _gameManager;
    private IncrementalData _globalDamage;
    private IncrementalData _globalFireRate;

    internal bool _canMove = true;
    
    [SerializeField] private GameObject cleanerParent;

    private void Awake()
    {
        _gameManager = GameManager.Instance;
    }

    internal void ShootStateChange()
    {
        // _gameManager.gameStates = GameStates.Shoot;
        cleanerParent.SetActive(false);
    }

    internal void GateHitSuccessful(BuffTypes buffType, float value)
    {
        if (buffType == BuffTypes.damage)
        {
            _gameManager.DamageUpdate(value);
        }
        else
        {
            _gameManager.FireRateUpdate(value);
        }
    }

    internal void GateHitFail(GameObject gate)
    {
        _canMove = false;
        var tweenMove = transform.TweenMove(transform.position + Vector3.back * 2, 0.3f, () =>
        {
            _canMove = true;
            gate.SetActive(false);
        });
        StartCoroutine(tweenMove);
    }
}
