using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject followedObject;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPos = followedObject.transform.position;
        Vector3 newPos = transform.position;

        if (targetPos.x > minX && targetPos.x < maxX) {
            newPos.x = targetPos.x;
        }
        if (targetPos.y > minY && targetPos.y < maxY) {
            newPos.y = targetPos.y;
        }
        transform.position = newPos;
    }
}
