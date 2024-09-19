using System.Collections;
using UniRx;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    private const string Shop = nameof(Shop);
    private const string FillAmount = "_FillAmount";

    [SerializeField] private float _timeToOpen = 5f;
    [SerializeField] private MeshRenderer _meshRenderer;

    private Coroutine _coroutine;
    private Material _circleMaterial;
    private float _timer;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _circleMaterial = _meshRenderer.material;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player _))
            _coroutine = StartCoroutine(OpenShopCoroutine());
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Player _))
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            ResetFillCircle();
        }
    }

    private IEnumerator OpenShopCoroutine()
    {
        while (_timer < _timeToOpen)
        {
            _timer++;
            _circleMaterial.SetFloat(FillAmount, _timer / _timeToOpen);
            yield return new WaitForSeconds(1);
        }

        MessageBroker.Default.Publish(new PauseSource(Shop));
        MessageBroker.Default.Publish(new ShopSource());
    }

    private void ResetFillCircle()
    {
        _timer = 0;
        _circleMaterial.SetFloat(FillAmount, 0);
    }
}