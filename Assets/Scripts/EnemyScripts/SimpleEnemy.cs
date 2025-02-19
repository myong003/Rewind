using UnityEngine;

public class SimpleEnemy : MonoBehaviour
{
    public GameObject simpleBullet;
    public GameObject spawnLocation;

    public int bulletSpeed = 100;
    public float maxTimeBetweenSpawn = 0.5f;

    private float coolDownForSpawner = 0;
    


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            coolDownForSpawner = 100;

        }


        if (Input.GetKeyUp(KeyCode.R))
        {
            coolDownForSpawner = maxTimeBetweenSpawn;
        }


        if (coolDownForSpawner <= 0)
        {
            SpawnBullet();
            coolDownForSpawner = maxTimeBetweenSpawn;
        }
        else
        {
            coolDownForSpawner -= Time.deltaTime;
        }
    }

    private void SpawnBullet()
    {
        float randX = Random.Range(-1f , 1f);
        float randY = Random.Range(-1f , 0f);

        Vector3 bulletDirection = new Vector3(randX, randY, 0);

        GameObject bullet = Instantiate(simpleBullet, spawnLocation.transform.position, spawnLocation.transform.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(bulletDirection * bulletSpeed);

    }
}
