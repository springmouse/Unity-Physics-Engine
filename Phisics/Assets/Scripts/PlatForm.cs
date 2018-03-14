using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatForm : MonoBehaviour
{
    public List<Transform> moveToPoints;

    public float speed;

    Transform m_currMovemeantPoint;
    int m_moveToListPoint;

    private void Start()
    {
        m_moveToListPoint = 0;
        m_currMovemeantPoint = moveToPoints[m_moveToListPoint];
        m_moveToListPoint++;
    }

    void Update()
    {
        if (moveToPoints.Count <= 1)
        {
            Debug.LogError(name + " does not have cotain enought move to points");
            return;
        }

        if ((m_currMovemeantPoint.position - transform.position).magnitude < 0.1f)
        {
            if (m_moveToListPoint == moveToPoints.Count)
            {
                m_moveToListPoint = 0;
            }

            m_currMovemeantPoint = moveToPoints[m_moveToListPoint];
            m_moveToListPoint++;
        }

        Vector3 movemeant = ((m_currMovemeantPoint.position - transform.position).normalized * speed) * Time.deltaTime;
        transform.position += movemeant;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.SetParent(null);
        }
    }
}
