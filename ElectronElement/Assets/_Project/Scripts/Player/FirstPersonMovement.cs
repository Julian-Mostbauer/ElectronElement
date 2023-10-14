using Unity.VisualScripting;
using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{
    [Space, SerializeField] private float speed = 3f;
    [SerializeField] private float sprintSpeed = 5f;
    [SerializeField] private float airSpeed = 2f;
    [SerializeField] private float climbSpeed;

    [Space, SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jump = 1f;

    [Space, SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;

    [Space, SerializeField] private Transform ladderCheck;
    [SerializeField] private float ladderDistance = 0.1f;
    [SerializeField] private LayerMask ladderMask;

    [Space, SerializeField] private PauseMenu pauseMenu;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private bool tryingToClimb;
    private float currentGravity;
    private float currentSpeed;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        currentGravity = gravity;
        currentSpeed = speed;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) pauseMenu.TogglePause();

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        tryingToClimb = Physics.CheckSphere(ladderCheck.position, ladderDistance, ladderMask);

        currentSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : isGrounded ? speed : airSpeed;

        if ((isGrounded || tryingToClimb) && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(currentSpeed * Time.deltaTime * move);

        if (tryingToClimb && Input.GetKey(KeyCode.W)) velocity = transform.up * climbSpeed;
        if (Input.GetButtonDown("Jump") && isGrounded && !tryingToClimb)
        {
            velocity.y = Mathf.Sqrt(jump * -2f * currentGravity);
        }

        velocity.y += currentGravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);    }
}