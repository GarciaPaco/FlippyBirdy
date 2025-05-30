using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PipeMoveScript : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 7;
    [SerializeField] private float deadZone = -50;
    [SerializeField] private LogicScript logic;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (LogicScript.Instance != null)
        {
            int currentScore = LogicScript.Instance.playerScore;
            float speedMultiplier = 1f + (currentScore / 4) * 0.1f;
            transform.position += (moveSpeed * speedMultiplier * Vector3.left) * Time.deltaTime;

        }
        else
        {
            transform.position += (Vector3.left * moveSpeed) * Time.deltaTime;

        }

        if (transform.position.x < deadZone)
        {
            Destroy(gameObject);
        }
    }
}

