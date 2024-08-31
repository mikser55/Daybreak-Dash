using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class AttackPlayer : Attack
{
    private Transform _myTransform;
    private Transform _targetTransform;

    public override void OnStart()
    {
        base.OnStart();

        _targetTransform = Target.Value.transform;
        _myTransform = transform;
    }

    public override TaskStatus OnUpdate()
    {
        if (TargetHealth == null || TargetHealth.CurrentHealth == 0 || (_targetTransform.position - _myTransform.position).sqrMagnitude > 1.5f * 1.5f)
        {
            if (Coroutine != null)
                OnEnd();

            return TaskStatus.Failure;
        }

        Coroutine ??= StartCoroutine(AttackCoroutine());

        return TaskStatus.Running;
    }
}