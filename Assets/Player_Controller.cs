using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    private Animator anim;
    private CharacterController characterController;
    private Vector3 playerVelocity;
    [SerializeField]
    private bool is_moving;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float gravityValue;
    private bool groundedPlayer;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        groundedPlayer = characterController.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        moveDirection.Normalize();
        characterController.Move(moveDirection * Time.deltaTime * speed);
        

        if (moveDirection != Vector3.zero)
        {
            is_moving = true;
            Quaternion rot = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, 10 * Time.deltaTime);
        }
        else
        {
            is_moving = false;
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        characterController.Move(playerVelocity * Time.deltaTime);

        anim.SetFloat("magnitude", moveDirection.magnitude);
        anim.SetBool("is_moving", is_moving);
    }
}
