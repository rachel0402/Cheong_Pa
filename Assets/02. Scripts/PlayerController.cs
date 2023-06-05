using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float h = 0.0f;
    private float v = 0.0f;
    private float w = 0.0f;
    Rigidbody rb = null;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(h, 0, v));

        if (Input.GetKeyDown(KeyCode.Space))
        {    
            rb.AddForce(Vector3.up * w, ForceMode.Impulse);
            
        }
    }
}
