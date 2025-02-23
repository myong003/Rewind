using UnityEngine;

public class Projectile : EntityCombat
{
    public bool disappearOnHit;
    public double damageDealt;
    private void OnTriggerEnter2D(Collider2D coll) {
        print("Triggered");
        EntityCombat enemy = coll.gameObject.GetComponent<EntityCombat>();
        if (enemy != null && enemy.team != this.team) {
            print("Hit");
            enemy.takeDamage(this.damageDealt);

            if (this.disappearOnHit) {
                Destroy(this.gameObject);
            }
        }
    }
}
