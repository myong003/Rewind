using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject followedObject;

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPos = followedObject.transform.position;
        targetPos.z = transform.position.z;
        transform.position = targetPos;
    }
}
