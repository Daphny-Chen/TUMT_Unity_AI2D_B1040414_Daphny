using UnityEngine;

public class CameraControl : MonoBehaviour
{

    [Header("速度"), Range(0, 10)]
    public float speed = 3;

    private Transform target;

    private void Start()
    {
        target = GameObject.Find("主角").transform;
    }

    private void LateUpdate()
    {
        Vector3 cam = transform.position;
        Vector3 tar = target.position;
        tar.z = -10;
        tar.y = Mathf.Clamp(tar.y, -1, 1);
        //tar.x = Mathf.Clamp(tar.y, -42, 42);
        transform.position = Vector3.Lerp(cam, tar, 0.3f * Time.deltaTime * speed);
    }



}
