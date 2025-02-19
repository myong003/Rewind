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

    protected virtual void attack() {
        spawner.SpawnBullet();
    }

    void Update()
    {

    }
}
