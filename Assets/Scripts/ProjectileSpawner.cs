using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    public GameObject projectile;
    public Transform spawnPosition;
    public bool team;
    public float projectileSpeed;

    public void SpawnBullet() {
        if (projectile != null) {
            Vector3 direction = this.gameObject.transform.forward;
            GameObject spawnedProjectile = Instantiate(projectile, spawnPosition.position, spawnPosition.rotation);
            spawnedProjectile.GetComponent<Rigidbody2D>().AddForce(direction * projectileSpeed);
            spawnedProjectile.GetComponent<Projectile>().team = team;
        }
    }

    public void SpawnBullet(Vector3 direction) {
        if (projectile != null) {
            GameObject spawnedProjectile = Instantiate(projectile, spawnPosition.position, spawnPosition.rotation);
            spawnedProjectile.GetComponent<Rigidbody2D>().AddForce(direction.normalized * projectileSpeed, ForceMode2D.Impulse);
            spawnedProjectile.GetComponent<Projectile>().team = team;
        }
    }
}
