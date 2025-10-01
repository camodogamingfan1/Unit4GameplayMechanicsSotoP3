using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private GameObject focalPoint;

    [Header("Movement")]
    public float speed = 5.0f;

    [Header("Jump")]
    public float jumpForce = 8.0f;
    private bool isGrounded;

    [Header("Dash")]
    public float dashForce = 20f;
    public float dashCooldown = 1f;
    private float lastDashTime;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    void Update()
    {
        Move();
        Jump();
        Dash();
    }

    private void Move()
    {
        float forwardInput = Input.GetAxis("Vertical");
        float sideInput = Input.GetAxis("Horizontal");

        Vector3 moveDirection =
            focalPoint.transform.forward * forwardInput +
            focalPoint.transform.right * sideInput;

        playerRb.AddForce(moveDirection * speed);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    private void Dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time > lastDashTime + dashCooldown)
        {
            Vector3 dashDirection =
                focalPoint.transform.forward * Input.GetAxis("Vertical") +
                focalPoint.transform.right * Input.GetAxis("Horizontal");

            if (dashDirection.magnitude > 0.1f)
            {
                playerRb.AddForce(dashDirection.normalized * dashForce, ForceMode.Impulse);
                lastDashTime = Time.time;
            }
        }
    }

    // Detect ground contact
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}


