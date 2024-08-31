using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime.Tasks.Movement;
using TooltipAttribute = BehaviorDesigner.Runtime.Tasks.TooltipAttribute;
using UnityEditor;

[TaskDescription("Check to see if the target object is within sight of the agent.")]
[TaskCategory("Custom")]
public class SimplifiedCanSeeObject : Conditional
{
    [Tooltip("The target object that is being searched.")]
    [SerializeField] private SharedGameObject _target;
    [Tooltip("The object that is within sight.")]
    [SerializeField] private SharedGameObject _returnedObject;
    [Tooltip("The field of view angle of the agent (in degrees).")]
    [SerializeField] private SharedFloat _fieldOfViewAngle = 360;
    [Tooltip("The distance that the agent can see.")]
    [SerializeField] private SharedFloat _viewDistance = 10;
    [Tooltip("The raycast offset relative to the pivot position.")]
    [SerializeField] private SharedVector3 _offset;
    [Tooltip("The LayerMask of the objects to ignore when performing the line of sight check.")]
    [SerializeField] private LayerMask _ignoreLayerMask;
    [Tooltip("SharedBool to indicate if the target is seen.")]
    [SerializeField] private SharedBool _isTargetSeen;

    private bool _isTargetMarked;

    public override void OnTriggerEnter(Collider other)
    {
        if (_isTargetMarked == false)
        {
            if (other.TryGetComponent(out Player target))
            {
                _target = target.gameObject;
                _isTargetMarked = false;
            }
        }
    }

    public override TaskStatus OnUpdate()
    {
        _returnedObject.Value = null;

        if (_target.Value != null)
        {
            bool targetSeen = MovementUtility.WithinSight(transform, _offset.Value, _fieldOfViewAngle.Value, _viewDistance.Value, _target.Value, _ignoreLayerMask);

            if (targetSeen)
            {
                _returnedObject.Value = _target.Value;
                _isTargetSeen.Value = true;
                return TaskStatus.Success;
            }
            else
            {
                _isTargetSeen.Value = false;
                return TaskStatus.Failure;
            }
        }

        _isTargetSeen.Value = false;
        return TaskStatus.Failure;
    }

    public override void OnEnd()
    {
        base.OnEnd();
    }

    public override void OnDrawGizmos()
    {
        Transform objectTransform = Owner.transform;
        Handles.color = Color.yellow;
        float halfFOV = _fieldOfViewAngle.Value * 0.5f;
        Vector3 offsetPosition = objectTransform.TransformPoint(_offset.Value);

        Handles.DrawWireArc(offsetPosition, objectTransform.up, Quaternion.Euler(0, -halfFOV, 0) * objectTransform.forward, _fieldOfViewAngle.Value, _viewDistance.Value);
        Handles.DrawLine(offsetPosition, offsetPosition + Quaternion.Euler(0, -halfFOV, 0) * objectTransform.forward * _viewDistance.Value);
        Handles.DrawLine(offsetPosition, offsetPosition + Quaternion.Euler(0, halfFOV, 0) * objectTransform.forward * _viewDistance.Value);

        if (_target.Value == null) return;

        if (_target.Value != null)
        {
            Handles.color = Color.red;
            Handles.DrawLine(offsetPosition, _target.Value.transform.position);
        }
    }
}