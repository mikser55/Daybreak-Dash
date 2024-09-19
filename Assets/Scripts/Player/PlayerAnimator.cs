using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private const string IsMoving = nameof(IsMoving);
    private const string IsExtracting = nameof(IsExtracting);

    [SerializeField] private Animator _animator;

    public void UpdateRun(bool isMoving)
    {
        _animator.SetBool(IsMoving, isMoving);
    }

    public void UpdateExtracting(bool isExtracting)
    {
        _animator.SetBool(IsExtracting, isExtracting);
    }
}