using UnityEngine;
[RequireComponent (typeof(AnimationBehaviour))]

public class MoveBehaviour : MonoBehaviour
{
    private CharacterController _cC;
    private AnimationBehaviour _aB;
    private Vector3 velocity;
    [SerializeField] private float speed = 4;
    [SerializeField] private float sprintMultiplier = 2;
    [SerializeField] private Transform cameraPosition;
    [SerializeField] private float gravity = -2;
    private void Awake()
    {
        _cC = GetComponent<CharacterController>();
        _aB = GetComponent<AnimationBehaviour>();
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void FixedUpdate()
    {
        if(_cC.isGrounded && velocity.y < 0)
        {
            velocity.y = -2;
        }
        velocity.y += gravity * Time.deltaTime;
        _cC.Move(velocity * Time.deltaTime);
    }
    public void MoveCharacter(Vector3 direction, bool run)
    {
        Vector3 forward = cameraPosition.forward;
        forward.y = 0;
        Vector3 right = cameraPosition.right;
        right.y = 0;
        Vector3 movement = direction.x * right + direction.z * forward;
        if(!run)
            Walk(movement);
        else
            Run(movement);
        _aB.Walk(movement);
    }
    private void Walk(Vector3 movement)
    {
        _cC.Move(movement * speed * Time.deltaTime);
    }
    private void Run(Vector3 movement)
    {
        _cC.Move(movement * speed * sprintMultiplier * Time.deltaTime);
    }
}
