using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Movement
{
    [TaskDescription("Seek the target specified using the Unity NavMesh.")]
    [TaskCategory("Movement")]
    [HelpURL("https://www.opsive.com/support/documentation/behavior-designer-movement-pack/")]
    [TaskIcon("3278c95539f686f47a519013713b31ac", "9f01c6fc9429bae4bacb3d426405ffe4")]
    public class Seek : NavMeshMovement
    {
        [Tooltip("The GameObject that the agent is seeking")]
        [UnityEngine.Serialization.FormerlySerializedAs("target")]
        [SerializeField] private SharedGameObject _target;
        [Tooltip("If target is null then use the target position")]
        [UnityEngine.Serialization.FormerlySerializedAs("targetPosition")]
        [SerializeField] private SharedVector3 _targetPosition;

        public override void OnStart()
        {
            base.OnStart();
            SetDestination(GetTargetPosition());
        }

        public override TaskStatus OnUpdate()
        {
            if (HasArrived()) {
                return TaskStatus.Success;
            }

            SetDestination(GetTargetPosition());

            return TaskStatus.Running;
        }
        
        private Vector3 GetTargetPosition()
        {
            if (_target.Value != null) {
                return _target.Value.transform.position;
            }
            return _targetPosition.Value;
        }

        public override void OnReset()
        {
            base.OnReset();
            _target = null;
            _targetPosition = Vector3.zero;
        }
    }
}