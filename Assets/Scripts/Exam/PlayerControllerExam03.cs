using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerExam03 : MonoBehaviour
{
    public float speed = 5f;
    public float xRange = 10;
    public GameObject projectilePrefab;

    public bool enableAutoFireMode = true;
    public float autoFireInterval = 0.5f;

    private float horizontalInput;
    private InputAction moveAction;
    private InputAction shootAction;

    private float fireTimer = 0f;

    private void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        shootAction = InputSystem.actions.FindAction("Shoot");
    }

    private void OnEnable()
    {
        moveAction.Enable();
        shootAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
        shootAction.Disable();
    }

    void Update()
    {
        // -------- Movement --------
        horizontalInput = moveAction.ReadValue<Vector2>().x;
        transform.Translate(Vector3.right * horizontalInput * speed * Time.deltaTime);

        if (transform.position.x < -xRange)
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);

        if (transform.position.x > xRange)
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);

        // -------- Shooting --------
        if (enableAutoFireMode)
        {
            // Auto
            fireTimer += Time.deltaTime;

            if (fireTimer >= autoFireInterval)
            {
                Shoot();
                fireTimer = 0f;
            }
        }
        else
        {
            // Manual
            if (shootAction.triggered)
            {
                Shoot();
            }
        }
    }

    void Shoot()
    {
        Instantiate(projectilePrefab, transform.position, transform.rotation);
    }
}

