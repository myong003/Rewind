using UnityEngine;

public class Pistol : PlayerWeapon
{
    public override void shoot(ProjectileSpawner spawner, PlayerLook playerLook)
    {
        spawner.SpawnBullet(playerLook.mouseDirection);
    }
}
