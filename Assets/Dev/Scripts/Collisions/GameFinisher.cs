using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFinisher : MonoBehaviour
{
    private GameManager _gameManager;
    private UIManager _uiManager;
    private MoneyControlManager _moneyManager;

    private void Awake()
    {
        _gameManager = GameManager.Instance;
        _uiManager = UIManager.Instance;
        _moneyManager = MoneyControlManager.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerCollision playerCollision))
        {
            _gameManager.gameStates = GameStates.Wait;
            _uiManager.OpenFinishRect();
            _moneyManager.ChangeMoney(250);
        }
    }
}
