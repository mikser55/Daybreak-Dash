using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerMover player))
        {
            //конец уровня
        }
    }
}