using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using TooltipAttribute = BehaviorDesigner.Runtime.Tasks.TooltipAttribute;

public class InitializationManager : Action
{
    private const string Gate = nameof(Gate);

    [Tooltip("The received object to be transmitted.")]
    [SerializeField] private SharedGameObject _object;

    private bool _hasSearched;

    public override TaskStatus OnUpdate()
    {
        if (_hasSearched == false)
        {
            _object.Value = GameObject.FindGameObjectWithTag(Gate);
            _hasSearched = true;
        }

        return _object.Value != null ? TaskStatus.Success : TaskStatus.Failure;
    }
}