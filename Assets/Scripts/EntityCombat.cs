using UnityEngine;

public class EntityCombat : MonoBehaviour
{
    public double maxHealth;
    public bool team;
    public ProjectileSpawner spawner;
    public bool isInvincible = false;
    private double currentHealth;
    
    public void takeDamage(double damageTaken) 
    {
        if (isInvincible) {
            return;
        }
        
        currentHealth -= damageTaken;
        print(this.gameObject + " took " + damageTaken + " damage");
        if (currentHealth <= 0) {
            Destroy(this.gameObject);
        }
    }

    protected virtual void attack() 
    {
        spawner.SpawnBullet();
    }

    protected virtual void attack(Vector3 direction) 
    {
        spawner.SpawnBullet(direction);
    }

    protected virtual void Start() 
    {
        currentHealth = maxHealth;
    }    
}
