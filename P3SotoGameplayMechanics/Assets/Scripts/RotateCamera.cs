using UnityEngine;

public class SimpleFollowCamera : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(0, 5, -10); // Behind and above

    void LateUpdate()
    {
        if (player == null) return;

        // Just follow player with offset
        transform.position = player.position + offset;

        // Always look at player
        transform.LookAt(player.position);
    }
}



