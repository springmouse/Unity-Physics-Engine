﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControlerScript : MonoBehaviour
{
    Vector3 m_moveDirection;
    Vector3 m_averageMoveDirection;

    Vector3 m_startPos;
    float m_jumpTime = 1;

    public float Speed;
    public float jumpSpeed;
    public float rotationSpeed;
    public float hitForce;
    public float mass;

    public float gravity;

    public LineRenderer LR;
    public float laserTime;
    float m_elapsedLaserTime = 0;

    CharacterController cc = null;

    // Use this for initialization
    void Start()
    {
        cc = GetComponent<CharacterController>();
        m_moveDirection = Vector3.zero;
    }

    private void Update()
    {
        Move();

        ShootLaser();

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * 0.5f, transform.localScale.z);
            transform.position += new Vector3(0, -0.5f, 0);
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * 2, transform.localScale.z);
        }

        if (m_elapsedLaserTime > laserTime)
        {
            LR.positionCount = 0;
        }
    }

    private void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float turn = Input.GetAxis("Turn");

        m_elapsedLaserTime += Time.deltaTime;

        if (cc.isGrounded)
        {
            m_moveDirection = new Vector3(horizontal, 0, vertical);
            m_moveDirection = transform.TransformDirection(m_moveDirection);
            m_moveDirection *= Speed;

            if (Input.GetKey(KeyCode.Space))
            {
                m_moveDirection.y = jumpSpeed;
            }
        }
        else
        {
            m_moveDirection = new Vector3(horizontal, m_moveDirection.y, vertical);
            m_moveDirection = transform.TransformDirection(m_moveDirection);
            m_moveDirection.x *= Speed;
            m_moveDirection.z *= Speed;
        }

        transform.Rotate(0, turn * rotationSpeed, 0);

        m_moveDirection.y -= gravity * Time.deltaTime;

        cc.Move(m_moveDirection * Time.deltaTime);

        m_averageMoveDirection = (m_averageMoveDirection + m_moveDirection) / 2;
    }

    void ShootLaser()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Input.GetMouseButtonDown(0))
        {
            Physics.Raycast(ray, out hit, 25);

            GameObject go;
            RagDoll rd = null;

            Vector3[] linePoints = new Vector3[2];
            linePoints[0] = transform.position;

            if (hit.transform != null)
            {
                go = hit.transform.gameObject;
                linePoints[1] = hit.point;

                if ((rd = go.GetComponentInParent<RagDoll>()) != null)
                {
                    rd.RagdollOn = true;
                }

                if (go.GetComponent<Rigidbody>())
                {
                    go.GetComponent<Rigidbody>().AddForce(ray.direction * hitForce);
                }
            }
            else
            {
                linePoints[1] = Camera.main.ScreenToWorldPoint(Input.mousePosition) + ray.direction * 25;
            }

            LR.positionCount = 2;
            LR.SetPositions(linePoints);

            m_elapsedLaserTime = 0;
        }

    }
    
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.GetComponent<Rigidbody>())
        {
            Vector3 dir = (hit.transform.position - transform.position).normalized;

            dir = dir / Time.deltaTime;

            Vector3 force = Vector3.Scale(dir, m_moveDirection) / (1 / mass) * Time.deltaTime;

            hit.gameObject.GetComponent<Rigidbody>().AddForce(force);
        }
    }

    private void FixedUpdate()
    {
    }
}
