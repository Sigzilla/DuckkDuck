using UnityEngine;

public class CameraFollowing : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] private float followSpeed = 2f;
    private float FixedY;
    private float FixedZ;


    private Transform Transform;

    private void Awake()
    {
        Transform = GetComponent<Transform>(); 
    }
    void Start()
    {
        FixedY = transform.position.y;
        FixedZ = transform.position.z;

    }

    void Update()
    {
        Vector3 desiredPosition = new Vector3(target.transform.position.x, FixedY, FixedZ);

        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime); 
    }
}
