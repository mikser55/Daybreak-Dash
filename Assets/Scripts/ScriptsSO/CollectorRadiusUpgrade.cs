using UnityEngine;

[CreateAssetMenu(fileName = "CollectorUpgrade", menuName = "Upgrade/StatUpgrade/CollectorUpgrade")]
public class CollectorRadiusUpgrade : Upgrade
{
    public override void ApplyUpgrade(Player player)
    {
        if (player == null) return;

        if (player.TryGetComponent(out ScrapCollector collector))
            collector.IncreaseRadius(StatValue);
    }
}