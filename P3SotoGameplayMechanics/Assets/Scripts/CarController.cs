using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BeginnerCarController : MonoBehaviour
{
    [Header("Wheel Colliders")]
    public WheelCollider frontLeftCol, frontRightCol;
    public WheelCollider rearLeftCol, rearRightCol;

    [Header("Wheel Meshes")]
    public Transform frontLeftMesh, frontRightMesh;
    public Transform rearLeftMesh, rearRightMesh;

    [Header("Car Settings")]
    public float maxSteerAngle = 30f;
    public float motorForce = 1500f;
    public float brakeForce = 3000f;
    public float maxSpeed = 50f; // in meters/second

    public Rigidbody rb; // assign manually in Inspector


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    void FixedUpdate()
    {
        float vertical = Input.GetAxis("Vertical");   // W/S or Up/Down
        float horizontal = Input.GetAxis("Horizontal"); // A/D or Left/Right
        bool braking = Input.GetKey(KeyCode.Space);

        // Limit speed
        if (rb.linearVelocity.magnitude > maxSpeed && vertical > 0)
            vertical = 0;

        // Motor torque on rear wheels
        rearLeftCol.motorTorque = vertical * motorForce;
        rearRightCol.motorTorque = vertical * motorForce;

        // Steering
        float steering = horizontal * maxSteerAngle;
        frontLeftCol.steerAngle = steering;
        frontRightCol.steerAngle = steering;

        // Braking
        float brake = braking ? brakeForce : 0f;
        rearLeftCol.brakeTorque = brake;
        rearRightCol.brakeTorque = brake;
        frontLeftCol.brakeTorque = brake;
        frontRightCol.brakeTorque = brake;

        // Update wheel meshes
        UpdateWheel(frontLeftCol, frontLeftMesh);
        UpdateWheel(frontRightCol, frontRightMesh);
        UpdateWheel(rearLeftCol, rearLeftMesh);
        UpdateWheel(rearRightCol, rearRightMesh);
    }

    void UpdateWheel(WheelCollider col, Transform trans)
    {
        if (trans == null) return;
        Vector3 pos;
        Quaternion rot;
        col.GetWorldPose(out pos, out rot);
        trans.position = pos;
        trans.rotation = rot;
    }
}
