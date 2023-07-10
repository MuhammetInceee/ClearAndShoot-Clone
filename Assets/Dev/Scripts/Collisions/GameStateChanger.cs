using System;
using UnityEngine;

public class GameStateChanger : MonoBehaviour
{
    private GameManager _gameManager;
    [SerializeField] private GameStates toState;

    private void Awake()
    {
        _gameManager = GameManager.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(toState == GameStates.Shoot)
            if (other.TryGetComponent(out PlayerCollision collision)) collision.ShootStateChange();
        
        _gameManager.gameStates = toState;
    }
}
