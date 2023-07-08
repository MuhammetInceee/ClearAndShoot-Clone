using System;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private GameManager _gameManager;
    
    [SerializeField] private GameObject cleanerParent;

    private void Awake()
    {
        _gameManager = GameManager.Instance;
    }

    internal void ChangeGameState()
    {
        _gameManager.gameStates = GameStates.Shoot;
        cleanerParent.SetActive(false);
    }

    internal void GateHit(BuffTypes buffType, float value)
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
}
