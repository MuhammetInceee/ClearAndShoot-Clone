using System;
using System.Globalization;
using TMPro;
using UnityEngine;

public class LevelEndObjects : MonoBehaviour
{
    private static readonly int Trigger = Animator.StringToHash("Trigger");
    
    private Animator _animator;
    private GameManager _gameManager;
    private UIManager _uiManager;
    
    [SerializeField] private TextMeshPro healthText;
    
    internal float health;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _gameManager = GameManager.Instance;
        _uiManager = UIManager.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Bullet bullet))
        {
            _animator.SetTrigger(Trigger);
            health -= bullet.damage;
            UpdateText();
            bullet.GetBackPool();
        }

        if (other.TryGetComponent(out PlayerCollision playerCollision))
        {
            _gameManager.gameStates = GameStates.Wait;
            _uiManager.OpenFinishRect();
        }
    }

    internal void UpdateText()
    {
        healthText.text = health.ToString(CultureInfo.InvariantCulture);
    }
}