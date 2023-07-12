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
        {
            if (other.TryGetComponent(out PlayerCollision collision))
            {
                if(collision.GetComponent<PlayerManager>().weaponList.Count > 0)
                {
                    collision.ShootStateChange();
                    _gameManager.gameStates = toState;
                }
                else
                {
                    _gameManager.gameStates = GameStates.Wait;
                    UIManager.Instance.OpenFinishRect();
                }
            }
        }
                
        if(toState == GameStates.LevelEnd)
        {
            if (other.TryGetComponent(out PlayerManager playerManager))
            {
                playerManager.NarrowWeapons();
                _gameManager.gameStates = toState;
            }
        }
        
    }
}
