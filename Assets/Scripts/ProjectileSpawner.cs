using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    public GameObject projectile;
    public Transform spawnPosition;
    public Vector3 direction;
    public float projectileSpeed;

    public void SpawnBullet() {
        if (projectile != null) {
            GameObject spawnedProjectile = Instantiate(projectile, spawnPosition.position, spawnPosition.rotation);
            spawnedProjectile.GetComponent<Rigidbody2D>().AddForce(direction * projectileSpeed);
        }
    }
}
