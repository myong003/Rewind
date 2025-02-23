using UnityEngine;

public class Projectile : MonoBehaviour
{
    public bool team;
    public bool disappearOnHit;
    public float damageDealt;
    private void OnTriggerEnter2D(Collider2D coll) {
        print("Triggered");
        if (coll.gameObject.tag == "Wall" && this.disappearOnHit) {
            Destroy(this.gameObject);
        }

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
