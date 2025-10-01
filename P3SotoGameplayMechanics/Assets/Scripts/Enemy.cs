using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    private Rigidbody EnemyRb;
    private GameObject player;

    void Start()
    {
        EnemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    void Update()
    {
        Vector3 LookDirection = (player.transform.position - transform.position).normalized;
        EnemyRb.AddForce(LookDirection * speed);
    }
}
