using UnityEngine;

public class PlayerCombat : EntityCombat
{
    public PlayerLook playerLook;

    public PlayerWeapon playerWeapon;
    protected override void attack() {
        // spawner.SpawnBullet(playerLook.mouseDirection);
        playerWeapon.shoot(spawner, playerLook);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            attack();
        }
    }
}
