using UnityEngine;

public class EntityCombat : MonoBehaviour
{
    public float maxHealth;
    public bool team;
    public ProjectileSpawner spawner;
    public bool isInvincible = false;
    public AudioSource hitSound;
    private float currentHealth;
    
    public void takeDamage(float damageTaken) 
    {
        if (isInvincible) {
            return;
        }

        if (hitSound != null) {
            hitSound.Play();
        }
        currentHealth -= damageTaken;
        print(this.gameObject + " took " + damageTaken + " damage");
        if (currentHealth <= 0) {
            Destroy(this.gameObject);
        }
    }

    public float getCurrentHealth() {
        return currentHealth;
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
