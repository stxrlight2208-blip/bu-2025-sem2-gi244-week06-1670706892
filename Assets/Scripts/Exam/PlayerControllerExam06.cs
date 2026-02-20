using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerExam06 : MonoBehaviour
{
    public enum MovementAxis
    {
        Horizontal,
        Vertical
    }

    public MovementAxis movementAxis;
    public float speed;
    public Transform Player1;
    public Transform Player2;
    public Camera targetCamera;

    public float offsetY = 10f;
    public float minZoom = 5f;
    public float maxZoom = 20f;
    public float ZoomLimiter = 10f;

    private float horizontalInput;
    private float verticalInput;
    private InputAction moveAction;

    private void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = moveAction.ReadValue<Vector2>().x;
        verticalInput = moveAction.ReadValue<Vector2>().y;

        if (movementAxis == MovementAxis.Horizontal)
        {
            transform.Translate(horizontalInput * speed * Time.deltaTime * Vector3.right);
        }
        else if (movementAxis == MovementAxis.Vertical)
        {
            transform.Translate(verticalInput * speed * Time.deltaTime * Vector3.forward);
        }
        
    }
    void LateUpdate()
    {
        MoveCamera();
        ZoomCamera();
    }
    void MoveCamera()
    {
        Vector3 centerPoint = (Player1.position + Player2.position) / 2f;
        transform.position = new Vector3(centerPoint.x, offsetY, centerPoint.z);
    }
    void ZoomCamera()
    {
        float distance = Vector3.Distance(Player1.position, Player2.position);

        float newZoom = Mathf.Lerp(minZoom, maxZoom, distance / ZoomLimiter);
        targetCamera.orthographicSize = newZoom;
    }
}
