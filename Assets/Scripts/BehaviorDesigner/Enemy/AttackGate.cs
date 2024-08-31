using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class AttackGate : Attack
{
    [SerializeField] private SharedBool _isPlayerInSight;

    public override TaskStatus OnUpdate()
    {
        if (TargetHealth == null || TargetHealth.CurrentHealth == 0 || _isPlayerInSight.Value)
        {
            if (Coroutine != null)
                OnEnd();

            return TaskStatus.Failure;
        }

        Coroutine ??= StartCoroutine(AttackCoroutine());

        return TaskStatus.Running;
    }
}