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
    public float health_point;
    private float attack_delay = 0f;
    private float attack_style_reset = 0f;
    private int punch_style = 0;
    private bool weapon = false;
    public GameObject sword;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (health_point > 0.0f && attack_delay == 0f)
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
        
        MotionControl();
        AttackDelay();

        if (Input.GetMouseButton(0) && attack_delay == 0.0f && health_point > 0f && !weapon)
        {
            attack_style_reset = 1f;

            if(punch_style == 0)
            {
               anim.SetTrigger("punch_1");
               attack_delay = 0.7f;
               punch_style = 1;
            }
            else if (punch_style == 1)
            {
                anim.SetTrigger("punch_2");
                attack_delay = 0.7f;
                punch_style = 0;
            }

        }

        if (Input.GetMouseButton(0) && attack_delay == 0.0f && health_point > 0f && weapon)
        {
            attack_style_reset = 1f;

            if (punch_style == 0)
            {
                anim.SetTrigger("combo_1");
                attack_delay = 0.7f;
                punch_style = 1;
            }
            else if (punch_style == 1)
            {
                anim.SetTrigger("combo_2");
                attack_delay = 0.7f;
                punch_style = 2;
            }
            else if (punch_style == 2)
            {
                anim.SetTrigger("combo_3");
                attack_delay = 0.8f;
                punch_style = 0;
            }
        }

        if(Input.GetKey(KeyCode.E) && attack_delay ==0.0f && health_point > 0.0f)
        {
            weapon = !weapon;
            sword.gameObject.SetActive(weapon);
        }
    }

    void AttackDelay()
    {
        if (attack_delay > 0f)
        {
            attack_delay = attack_delay - Time.deltaTime;
        }
        else
        {
            attack_delay = 0f;
        }

        if (attack_style_reset > 0f)
        {
            attack_style_reset = attack_style_reset - Time.deltaTime;
        }
        else
        {
            attack_style_reset = 0f;
            punch_style = 0;
        }
    }
    void MotionControl()
    {
        if (health_point > 30f)
        {
            speed = 4f;
            anim.SetBool("slowmotion", false);
            //anim.SetBool("death", false);
        }
        else if (health_point < 30f && health_point > 0f)
        {
            speed = 2.5f;
            anim.SetBool("slowmotion", true);
            anim.SetBool("death", false);
            anim.SetLayerWeight(1, 1f);
        }
        else if (health_point <= 0f)
        {
            speed = 0f;
            anim.SetBool("death", true);
            anim.SetLayerWeight(1, 0f);
        }
    }

    public void HitDamage(float damage)
    {
        health_point = health_point - damage;
        anim.SetTrigger("hit");
    }

    public void Healing(float healpoint)
    {
        health_point = health_point + healpoint;
    }
}
