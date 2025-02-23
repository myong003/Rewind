using System.Collections;
using UnityEditor.UI;
using UnityEngine;

public class ScorpionBoss : EntityCombat
{
    public float basicAttackRate = 5f;
    public float basicAttackSpread;

    public float laserAttackRate = 10f;
    private float basicAttackTimer;
    private float laserAttackTimer;

    public float chargeUpTime = 5f;
    public float laserDelay = 1f;

    public float laserRotateDuration = 5f;
    public Transform arenaCenter;

    public GameObject laser;
    public Transform laserEndpoint;

    private bool doingLaserAttack = false;

    private GameObject player;
    protected override void Start()
    {
        base.Start();
        basicAttackTimer = basicAttackRate;
        laserAttackTimer = 0;
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        if (basicAttackTimer < 0)
        {
            Vector3 firstShotDirection = player.transform.position - transform.position;
            float randX = Random.Range(basicAttackSpread * -1, basicAttackSpread);
            float randY = Random.Range(basicAttackSpread * -1, basicAttackSpread);
            Vector3 secondShotDirection = firstShotDirection + new Vector3(randX, randY, 0);
            attack(firstShotDirection);
            attack(secondShotDirection);
            basicAttackTimer = basicAttackRate;
        }
        basicAttackTimer -= Time.deltaTime;

        if (doingLaserAttack == false)
        {
            laserAttackTimer -= Time.deltaTime;
            if (laserAttackTimer < 0)
            {
                StartCoroutine(LaserAttack());
                laserAttackTimer = laserAttackRate;
            }
        }
    }

    public IEnumerator LaserAttack()
    {
        doingLaserAttack = true;
        transform.position = arenaCenter.position;
        int frames = 60;
        for (int i = 0; i < frames; i++) {
            Vector3 direction = player.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
            yield return new WaitForSeconds(chargeUpTime/frames);
        }
        yield return new WaitForSeconds(laserDelay);
        
        Vector3 posDiff = player.transform.position - laserEndpoint.position;
        print(posDiff);
        float rotationDirection = posDiff.x > 0 ? -1 : 1; // -1 for counter-clockwise, 1 for clockwise
        laser.SetActive(true);

        float startRotation = transform.eulerAngles.z;
        float targetRotation = startRotation + (360f * rotationDirection);
        print(startRotation + " " + targetRotation);
        float t = 0f;
        while (t < laserRotateDuration)
        {
            t += Time.deltaTime;
            float zRotation = Mathf.Lerp(startRotation, targetRotation, t/laserRotateDuration) % 360f;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, zRotation);
            yield return new WaitForSeconds(1/frames);
        }
        laser.SetActive(false);

        doingLaserAttack = false;
        yield return null;
    }
}
