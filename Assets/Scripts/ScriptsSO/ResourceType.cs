using UnityEngine;

[CreateAssetMenu(fileName = "NewResource", menuName = "Resource")]
public class ResourceType : ScriptableObject
{
    [field: SerializeField] private Sprite _icon;

    [field: SerializeField] public string Name { get; private set; }
}
