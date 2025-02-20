using Unity.Mathematics;
using UnityEngine;

public class Shotgun : PlayerWeapon
{
    public int projectileCount = 5;
    public float spread = 30;
    public override void shoot(ProjectileSpawner spawner, PlayerLook playerLook)
    {
        float perBulletSpread = spread / projectileCount;

        for (int i = 0; i < projectileCount; i++)
        {
            float angle = -spread/2 + perBulletSpread * i;
            Vector3 newDirection = Quaternion.Euler(0, 0, angle) * playerLook.mouseDirection;
            spawner.SpawnBullet(newDirection);
        }
        
    }
}
