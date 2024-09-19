using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class CircleFillController : MonoBehaviour
{
    private const string FillAmount = "_FillAmount";

    [SerializeField] private Player _player;

    private Vector3 _fixedPosition = new(90, 0, 0);
    private MeshRenderer _meshRenderer;
    private Material _circleMaterial;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _circleMaterial = _meshRenderer.material;
    }

    private void OnEnable()
    {
        _player.ExperienceChanged += UpdateCircle;
    }

    private void OnDisable()
    {
        _player.ExperienceChanged -= UpdateCircle;
    }

    private void Start()
    {
        _circleMaterial.SetFloat(FillAmount, 0);
    }

    public void ExperienceCircleRotation()
    {
        Quaternion EulerCirclePosition = Quaternion.Euler(_fixedPosition);
        transform.rotation = EulerCirclePosition;
    }

    private void UpdateCircle()
    {
        _circleMaterial.SetFloat(FillAmount, (float)_player.CurrentExperience / _player.ExperienceToNextLevel);

        if (_player.CurrentExperience == _player.ExperienceToNextLevel)
            _circleMaterial.SetFloat(FillAmount, 0);
    }
}