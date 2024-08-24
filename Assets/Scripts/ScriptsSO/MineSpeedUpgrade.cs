using UnityEngine;

[CreateAssetMenu(fileName = "MineSpeedUpgrade", menuName = "Upgrade/StatUpgrade/MineSpeedUpgrade")]
public class MineSpeedUpgrade : Upgrade
{
    public override void ApplyUpgrade(Player player)
    {
        if (player == null) return;

        if (player.TryGetComponent(out Pickaxe pickaxe))
            pickaxe.IncreaseMineSpeed(StatValue);
    }
}