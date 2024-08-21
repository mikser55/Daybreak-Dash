using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class CircleFillController : MonoBehaviour
{
    private const string FillAmount = "_FillAmount";

    [SerializeField] private Player _player;

    private SpriteRenderer _spriteRenderer;
    private Material _circleMaterial;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _circleMaterial = _spriteRenderer.material;
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
        UpdateCircle();
    }

    private void UpdateCircle()
    {
        _circleMaterial.SetFloat(FillAmount, (_player.CurrentExperience * 100) / _player.ExperienceToNextLevel);
    }
}
