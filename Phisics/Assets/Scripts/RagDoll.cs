using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class RagDoll : MonoBehaviour
{

    private Animator m_animator = null;
    private Transform[] m_allBodyParts;
    private float m_elapsedTime = 0;

    public List<Rigidbody> rigidbodies = new List<Rigidbody>();
    public float gravity;
    public float deathTimmer;

    public bool RagdollOn
    {
        get
        {
            return !m_animator.enabled;
        }
        set
        {
            m_animator.enabled = !value;
            foreach (Rigidbody r in rigidbodies)
            {
                r.isKinematic = !value;
            }
        }
    }

	// Use this for initialization
	void Start ()
    {
        m_allBodyParts = transform.GetComponentsInChildren<Transform>();

        m_animator = GetComponent<Animator>();
        
        foreach (Rigidbody r in rigidbodies)
        {
            r.isKinematic = true;
        }

        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
	}

    // Update is called once per frame
    void Update()
    {
        if (RagdollOn == false)
        {
            transform.position += Vector3.down * gravity * Time.deltaTime;
        }

        m_elapsedTime += Time.deltaTime;

        if (m_elapsedTime > deathTimmer)
        {
            for (int i = 0; i < m_allBodyParts.Length; i++)
            {
                if (m_allBodyParts[i] != null)
                {
                    Destroy(m_allBodyParts[i].gameObject);
                }
            }

            Destroy(this);
        }
	}
}
