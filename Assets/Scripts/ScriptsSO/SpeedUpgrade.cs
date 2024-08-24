using UnityEngine;

[CreateAssetMenu(fileName = "SpeedUpgrade", menuName = "Upgrade/StatUpgrade/SpeedUpgrade")]
public class SpeedUpgrade : Upgrade
{
    public override void ApplyUpgrade(Player player)
    {
        if (player == null) return;

        if (player.TryGetComponent(out PlayerMover mover))
            mover.IncreaseMovespeed(StatValue);
    }
}