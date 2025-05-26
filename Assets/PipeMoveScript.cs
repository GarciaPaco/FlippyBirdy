using UnityEngine;

public class PipeMoveScript : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 7;
    [SerializeField] private float deadZone = -50;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3.left * moveSpeed) * Time.deltaTime;
        if (transform.position.x < deadZone)
        {
            Destroy(gameObject);
        }
    }
}
