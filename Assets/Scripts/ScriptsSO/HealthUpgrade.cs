using UnityEngine;

[CreateAssetMenu(fileName = "HealthUpgrade", menuName = "Upgrade/StatUpgrade/HealthUpgrade")]
public class HealthUpgrade : Upgrade
{
    public override void ApplyUpgrade(Player player)
    {
        if (player == null) return;

        if (player.TryGetComponent(out Health health))
            health.IncreaseMaxHealth(StatValue);
    }
}