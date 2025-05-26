using UnityEngine;
using UnityEngine.InputSystem;

public class BirdScript : MonoBehaviour
{
    [SerializeField] private Rigidbody2D myRigidBody;
    [SerializeField] private float jumpForce = 10;

    private PlayerInputActions inputActions;
    public LogicScript logic;
    public bool birdIsAlive = true;
    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip wingFlap;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    private void OnEnable()
    {
        inputActions.Bird.Jump.performed += OnJumpPerformed;
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Bird.Jump.performed -= OnJumpPerformed;
        inputActions.Disable();
    }

    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        if (!birdIsAlive) return;
        Debug.Log("Jump triggered!");
        myRigidBody.linearVelocity = Vector2.up * jumpForce;
        if (audioSource && wingFlap)
        {
            audioSource.PlayOneShot(wingFlap);
        }
    }
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        logic.gameOver();
        birdIsAlive = false;
    }
}
