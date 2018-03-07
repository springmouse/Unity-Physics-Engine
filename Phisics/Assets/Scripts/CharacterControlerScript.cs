using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControlerScript : MonoBehaviour
{
    Vector3 m_moveDirection;

    Vector3 m_startPos;
    float m_jumpTime = 1;
    
    public float Speed;
    public float jumpSpeed;
    public float rotationSpeed;

    public float gravity;

    CharacterController cc = null;

	// Use this for initialization
	void Start ()
    {
        cc = GetComponent<CharacterController>();
	}

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (cc.isGrounded)
        {
            m_moveDirection = new Vector3(0, 0, vertical);
            m_moveDirection = transform.TransformDirection(m_moveDirection);
            m_moveDirection *= Speed;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_moveDirection.y = jumpSpeed;
            }
        }
        else
        {
            m_moveDirection = new Vector3(0, m_moveDirection.y, vertical);
            m_moveDirection = transform.TransformDirection(m_moveDirection);
            m_moveDirection.x *= Speed;
            m_moveDirection.z *= Speed;
        }

        transform.Rotate(0, horizontal * rotationSpeed, 0);

        m_moveDirection.y -= gravity * Time.deltaTime;

        cc.Move(m_moveDirection * Time.deltaTime);
        
     
    }

    private void FixedUpdate()
    {
    }
}
