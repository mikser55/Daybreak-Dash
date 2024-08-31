using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;

public abstract class Attack : Action
{
    private const string Gate = nameof(Gate);

    [SerializeField] protected SharedGameObject Target;
    [SerializeField] protected float AttackDamage = 10f;
    [SerializeField] protected float AttackCooldown = 1f;

    protected Health TargetHealth;
    protected Coroutine Coroutine;

    public override void OnStart()
    {
        if (Target.Value.TryGetComponent(out Health health))
            TargetHealth = health;
    }

    public override void OnEnd()
    {
        StopAllCoroutines();
        Coroutine = null;
    }

    public abstract override TaskStatus OnUpdate();

    protected IEnumerator AttackCoroutine()
    {
        WaitForSeconds wait = new(AttackCooldown);

        while (true)
        {
            TargetHealth.TakeDamage(AttackDamage);
            yield return wait;
        }
    }
}