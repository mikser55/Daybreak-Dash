using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

[TaskDescription("Repeats the child task until the child task returns success or failure based on the end conditions. " +
                 "The conditional abort can be used to abort the repeater early.")]
[TaskIcon("{SkinColor}RepeatIcon.png")]
public class Repeater : Decorator
{
    public enum AbortType
    {
        None,
        Self,
        LowerPriority,
        Both
    }

    [Tooltip("The number of times to repeat the child task. Set to -1 to repeat forever")]
    public SharedInt count = -1;
    [Tooltip("Should the repeater end after the first failure?")]
    public SharedBool endOnFailure;
    [Tooltip("Should the repeater end after the first success?")]
    public SharedBool endOnSuccess;
    [Tooltip("Should the repeater reset the child after each iteration?")]
    public SharedBool resetChild;
    [Tooltip("Condition to end the repeater")]
    public SharedBool endCondition;
    [Tooltip("The type of conditional abort")]
    public AbortType abortType = AbortType.None;

    // The number of times the child task has been run.
    private int executionCount;
    // The status of the child after it has finished running.
    private TaskStatus executionStatus = TaskStatus.Inactive;

    public override bool CanExecute()
    {
        // Continue executing until we've reached the count or the child task returns failure.
        return (count.Value == -1 || executionCount < count.Value) &&
               (!endOnFailure.Value || (endOnFailure.Value && executionStatus != TaskStatus.Failure)) &&
               (!endOnSuccess.Value || (endOnSuccess.Value && executionStatus != TaskStatus.Success)) &&
               (endCondition == null || endCondition.Value);
    }

    public override bool CanRunParallelChildren()
    {
        return abortType == AbortType.LowerPriority || abortType == AbortType.Both;
    }

    public override void OnChildExecuted(TaskStatus childStatus)
    {
        // Update the execution status after the child has finished running.
        executionStatus = childStatus;
        if ((childStatus == TaskStatus.Success && endOnSuccess.Value) || (childStatus == TaskStatus.Failure && endOnFailure.Value))
        {
            executionCount = count.Value;
        }
        else
        {
            executionCount++;
        }
    }

    public override void OnEnd()
    {
        // Reset the variables back to their starting values.
        executionCount = 0;
        executionStatus = TaskStatus.Inactive;
    }

    public override void OnReset()
    {
        // Reset the public properties back to their original values.
        count = -1;
        endOnFailure = false;
        endOnSuccess = false;
        resetChild = false;
        endCondition = null;
        abortType = AbortType.None;
    }

    public override TaskStatus OnUpdate()
    {
        if (HasLowerPriorityAbort())
        {
            return TaskStatus.Running;
        }
        return base.OnUpdate();
    }

    private bool HasLowerPriorityAbort()
    {
        return (abortType == AbortType.LowerPriority || abortType == AbortType.Both) &&
               (endCondition == null || endCondition.Value);
    }
}