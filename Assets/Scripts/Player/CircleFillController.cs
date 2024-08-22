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
        _circleMaterial.SetFloat(FillAmount, 0);
    }

    private void UpdateCircle()
    {
        _circleMaterial.SetFloat(FillAmount, (float)_player.CurrentExperience / _player.ExperienceToNextLevel);

        if (_player.CurrentExperience == _player.ExperienceToNextLevel)
            _circleMaterial.SetFloat(FillAmount, 0);
    }
}