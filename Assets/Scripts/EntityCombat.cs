using UnityEngine;

public class EntityCombat : MonoBehaviour
{
    public double maxHealth;
    public bool team;
    public ProjectileSpawner spawner;
    private double currentHealth;
    
    public void takeDamage(double damageTaken) {
        currentHealth -= damageTaken;
        print(this.gameObject + " took " + damageTaken + " damage");
        if (currentHealth <= 0) {
            Destroy(this.gameObject);
        }
    }

    private void attack() {
        spawner.SpawnBullet();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            attack();
        }
    }
}
