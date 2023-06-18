using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterController : MonoBehaviour
{
    public float speed = 5.0f; // �̵� �ӵ�
    public float rotationSpeed = 200.0f; // ȸ�� �ӵ�
    public float jumpForce = 2.0f; // ���� ��

    public Animator animator;

    private bool isJumping = false; // ���� ������ üũ
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // �յ� �̵��� w, s �Ǵ� ����Ű ���Ʒ�
        float moveVertical = Input.GetAxis("Vertical");

        // �¿� ȸ���� a, d �Ǵ� ����Ű �¿�
        float rotateHorizontal = Input.GetAxis("Horizontal");

        #region �ִϸ��̼� �ڵ�
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

        // �̵�
        Vector3 movement = transform.forward * moveVertical * speed * animationSpeed;
        rb.MovePosition(rb.position + movement * Time.deltaTime);

        // ȸ��
        Vector3 rotation = new Vector3(0, rotateHorizontal, 0) * rotationSpeed;
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation * Time.deltaTime));

        // ���� - ���� ���� �ƴ� ��, �����̽��ٸ� ������ ����
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode.Impulse);
            isJumping = true;
        }

        
    }

    // ���� �� �ٽ� ���� ��Ҵ��� üũ
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
