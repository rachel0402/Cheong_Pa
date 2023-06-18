using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterController : MonoBehaviour
{
    public float speed = 5.0f; // 이동 속도
    public float rotationSpeed = 200.0f; // 회전 속도
    public float jumpForce = 2.0f; // 점프 힘

    public Animator animator;

    private bool isJumping = false; // 점프 중인지 체크
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // 앞뒤 이동은 w, s 또는 방향키 위아래
        float moveVertical = Input.GetAxis("Vertical");

        // 좌우 회전은 a, d 또는 방향키 좌우
        float rotateHorizontal = Input.GetAxis("Horizontal");

        #region 애니메이션 코드
        float animationSpeed = 0;

        if (rotateHorizontal != 0 || moveVertical != 0)
        {
            animator.SetBool("isWalk", true);
        }
        else
        {
            animator.SetBool("isWalk", false);
        }

        if (moveVertical < 0)
        {
            animator.SetFloat("playerMove", -1);
        }
        else
        {
            animator.SetFloat("playerMove", 1);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetBool("isRun", true);
            animationSpeed = 2;
        }
        else
        {
            animator.SetBool("isRun", false);
            animationSpeed = 1;
        }
        #endregion

        // 이동
        Vector3 movement = transform.forward * moveVertical * speed * animationSpeed;
        rb.MovePosition(rb.position + movement * Time.deltaTime);

        // 회전
        Vector3 rotation = new Vector3(0, rotateHorizontal, 0) * rotationSpeed;
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation * Time.deltaTime));

        // 점프 - 점프 중이 아닐 때, 스페이스바를 누르면 점프
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode.Impulse);
            isJumping = true;
        }

        
    }

    // 점프 후 다시 땅에 닿았는지 체크
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isJumping=false;
        }

        switch (collision.gameObject.tag)
        {
            case ("Bus"):
                SceneManager.LoadScene("Hyochang");
                break;

            case ("Subway"):
                SceneManager.LoadScene("Namsan");
                break; 
            default:
                break;
        }

        

    }
}
