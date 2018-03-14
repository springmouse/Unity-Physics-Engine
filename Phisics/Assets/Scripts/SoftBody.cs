using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoftBody : MonoBehaviour
{
    public float leangth;
    public float height;
    public float width;

    public GameObject body;

    List<GameObject> m_myBody = new List<GameObject>();

    
	void Start ()
    {
        CreateSoftBody();
	}

    private void CreateSoftBody()
    {
        for (int x  = 0; x < leangth; x++)
        {
            for (int y = 0; y < height; y++)
            {
                for (int z = 0; z < width; z++)
                {
                    GameObject go = Instantiate(body, transform.position + new Vector3(body.transform.localScale.x * x + 1, body.transform.localScale.y * y + 1, body.transform.localScale.z * z + 1), Quaternion.identity);
                    go.transform.SetParent(transform);
                    m_myBody.Add(go);
                    go.name = "(" + x + ", " + y + ", " + z + ")";

                }
            }
        }

        List<GameObject> addedBodys = new List<GameObject>();
        float linkDistance = body.transform.localScale.magnitude + (body.transform.localScale.magnitude * 0.15f);

        for (int i = 0; i < m_myBody.Count; i++)
        {
            for (int u = 0; u < m_myBody.Count; u++)
            {
                if (m_myBody[i] == m_myBody[u])
                {
                    continue;
                }

                bool link = true;

                for (int p = 0; p < addedBodys.Count; p++)
                {
                    if (m_myBody[u] == addedBodys[p])
                    {
                        link = false;
                        break;
                    }
                }

                if (link == true)
                {
                    if ((m_myBody[u].transform.position - m_myBody[i].transform.position).magnitude < linkDistance)
                    {
                        SpringJoint sp = m_myBody[i].AddComponent<SpringJoint>();
                        sp.connectedBody = m_myBody[u].GetComponent<Rigidbody>();
                        sp.spring = 200;
                        sp.breakForce = 80;
                    }
                }
            }
            addedBodys.Add(m_myBody[i]);

        }
    }

    bool runOnce = true;

	void Update ()
    {
        if (runOnce == true)
        {
            for (int i = 0; i < m_myBody.Count; i++)
            {
                if ((m_myBody[i].GetComponent<RagDoll>() == true))
                {
                    m_myBody[i].GetComponent<RagDoll>().RagdollOn = true;
                }
            }
            runOnce = false;
        }        
    }
}
