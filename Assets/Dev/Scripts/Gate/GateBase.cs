using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
#pragma warning disable CS8524

public class GateBase : MonoBehaviour
{
    private static readonly int Hit = Animator.StringToHash("Hit");
    
    [SerializeField] private TextMeshProUGUI buffValueText;
    [SerializeField] private bool hasTween;
    
    public Transform playerTr;
    public float buffValue;
    
    internal Vector3 StartPos;

    private Animator _animator;
    
    private Material _gateMaterial;
    
    [Header("Buff Values")]
    [EnumToggleButtons, SerializeField] private BuffTypes buffTypes;
    private BuffType buffType => new(buffTypes);
    [Header("Movement Values")]
    [EnumToggleButtons, SerializeField] private MovementTypes movementTypes;
    [ShowIf("@this.movementTypes == MovementTypes.horizontal || this.movementTypes == MovementTypes.both")]
    public GateHorizontalMovementSO horizontal;
    [ShowIf("@this.movementTypes == MovementTypes.vertical || this.movementTypes == MovementTypes.both")]
    public GateVerticalMovementSO vertical;

    [ShowIf("hasTween"), SerializeField] private Collider tweenCollider;
    private MovementType movementType => new(movementTypes, horizontal, vertical);

    private void Awake() => InitVariables();
    private void Update() => movementType.update?.Invoke(this);
    
    private void InitVariables()
    {
        StartPos = transform.position;
        _gateMaterial = GetComponent<MeshRenderer>().materials[0];
        _animator = GetComponent<Animator>();
        GateUpdater();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Bullet bullet))
        {
            BulletTrigger(other, bullet);
            bullet.GetBackPool();
        }

        else if (other.TryGetComponent(out PlayerCollision playerCollision))
        {
            if(tweenCollider != null) tweenCollider.enabled = false;
            playerCollision.GateHit(buffTypes, buffValue);
        }
    }

    protected virtual void BulletTrigger(Collider other, Bullet bullet){}

    protected void GateUpdater()
    {
        var sign = (buffValue > 0) ? "+" : "";
        
        buffValueText.text = $"{sign}{buffValue}\n{buffType.name}";
        _gateMaterial.color = buffValue <= 0 ? Color.red : Color.green;
        _animator.SetTrigger(Hit);
    }
}

public enum BuffTypes{fireRate, damage}

[System.Serializable]
public class BuffType
{
    internal static readonly BuffType fireRate = new(BuffTypes.fireRate);
    internal static readonly BuffType damage = new(BuffTypes.damage);

    private readonly BuffTypes buffTypes;
    internal BuffType(BuffTypes buffTypes)
    {
        this.buffTypes = buffTypes;
    } 

    internal string name => buffTypes switch
    {
        BuffTypes.fireRate => "Fire Rate",
        BuffTypes.damage => "Damage"
    };
}

[Flags]
public enum MovementTypes{stuck, horizontal, vertical, both = horizontal | vertical}

[System.Serializable]
public class MovementType
{
    internal static readonly MovementType horizontal = new(MovementTypes.horizontal, null, null);
    internal static readonly MovementType vertical = new(MovementTypes.vertical, null, null);
    internal static readonly MovementType stuck = new(MovementTypes.stuck, null, null);
    internal static readonly MovementType both = new(MovementTypes.both, null, null);

    private readonly MovementTypes movementTypes;
    private readonly GateHorizontalMovementSO horizontalData;
    private readonly GateVerticalMovementSO verticalData;

    internal MovementType(MovementTypes movementTypes, GateHorizontalMovementSO horizontalData, GateVerticalMovementSO verticalData)
    {
        this.movementTypes = movementTypes;
        this.horizontalData = horizontalData;
        this.verticalData = verticalData;
    }

    internal Action<GateBase> update => movementTypes switch
    {
        MovementTypes.stuck => null,
        MovementTypes.horizontal => horizontalData.update,
        MovementTypes.vertical => verticalData.update,
        MovementTypes.both => horizontalData.update + verticalData.update
    };
}