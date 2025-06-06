using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] float attackDistance = 10f;
    [SerializeField, Range(0f, 180f)] float maxViewAngle = 60f;

    public Vector3 DirectionToPlayer { get; private set; }
    public float DistanceFromPlayer { get; private set; }
    public Player Player { get; private set; }
    public bool CanAttack { get; private set; }
    public EnemyActiveWeapon EnemyActiveWeapon { get; private set; }
    public float AngleToPlayer { get; private set; }
    public bool PlayerInFOV { get; private set; }

    NavMeshAgent agent;
    

    public virtual void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        EnemyActiveWeapon = GetComponentInChildren<EnemyActiveWeapon>();
    }

    public virtual void Start()
    {
        Player = FindFirstObjectByType<Player>();
    }

    public virtual void Update()
    {
        if (!Player) return;

        DirectionToPlayer = Player.transform.position - transform.position;
        DistanceFromPlayer = DirectionToPlayer.magnitude;
        AngleToPlayer = Vector3.Angle(transform.forward, DirectionToPlayer);

        bool inAttackDistance = DistanceFromPlayer <= attackDistance;

        PlayerInFOV = AngleToPlayer <= maxViewAngle;
        CanAttack = inAttackDistance && agent.hasPath;

        if (PlayerInFOV && inAttackDistance) agent.SetDestination(Player.transform.position);
    }

    public virtual void Attack()
    {
        if (!CanAttack) return;
    }
}
