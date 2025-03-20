using Photon.Pun;
using UnityEngine;

public class MovmentPlayer : MonoBehaviourPun
{
    [Header("Links")]
    [SerializeField] private Transform body;
    [SerializeField] private Transform groundChecker;
    [SerializeField] private Transform cellingChecker;
    [SerializeField] private LayerMask collisionLayer;
    [SerializeField] private CharacterController cc;

    [Header("Settings")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float gravity = -40f;

    private bool isGround;
    private Vector3 moveDirection;
    private Vector3 velocity;

    private void Update()
    {
        isGround = Physics.CheckSphere(body.position, cc.radius, collisionLayer);

        if (isGround && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if(!isGround && velocity.y > 0)
        {
            if(Physics.CheckSphere(cellingChecker.position, cc.radius, collisionLayer))
            {
                velocity.y = -2f;
            }
        }

        moveDirection = body.right * Input.GetAxisRaw("Horizontal") + body.forward * Input.GetAxisRaw("Vertical");
       

        cc.Move(moveDirection * moveSpeed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        if (Input.GetButtonDown("Jump") && isGround)
        {
            Jump();
        }

        cc.Move(velocity * Time.deltaTime);
    }

    internal void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }
}
