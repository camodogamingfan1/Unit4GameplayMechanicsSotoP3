using UnityEngine;

public class RotateCameraX : MonoBehaviour
{
    public float followSpeed = 10f;
    public float speed = 10f; // Assuming 'speed' is needed for rotation
    public GameObject player; // Assuming 'player' is needed for position

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up, horizontalInput * speed * Time.deltaTime);

        // Smoothly move the focal point toward player
        transform.position = Vector3.Lerp(
            transform.position,
            player.transform.position,
            followSpeed * Time.deltaTime
        );
    }
}
