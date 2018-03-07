using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class RagDoll : MonoBehaviour
{

    private Animator m_animator = null;
    public List<Rigidbody> rigidbodies = new List<Rigidbody>();

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
        m_animator = GetComponent<Animator>();

        foreach (Rigidbody r in rigidbodies)
        {
            r.isKinematic = true;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
