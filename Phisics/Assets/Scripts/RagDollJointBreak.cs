using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagDollJointBreak : MonoBehaviour
{
    public float breakeForce;

    List<GameObject> m_brokenLimbs = new List<GameObject>();
    List<CharacterJoint> m_joints = new List<CharacterJoint>();

    public void Start()
    {
        CharacterJoint[] CJ = GetComponentsInChildren<CharacterJoint>();

        for (int i = 0; i < CJ.Length; i++)
        {
            m_joints.Add(CJ[i]);
        }
    }

    public void Update()
    {
        List<CharacterJoint> removeLimbs = new List<CharacterJoint>();
        float breakForceSqr = breakeForce * breakeForce;

        foreach (CharacterJoint cj in m_joints)
        {
            if (cj.gameObject == null)
            {
                continue;
            }

            if (cj.currentForce.sqrMagnitude > breakForceSqr)
            {
                if (cj.name.Contains("Arm") || cj.name.Contains("Head") || cj.name.Contains("UpLeg"))
                {
                    cj.transform.SetParent(null);

                    m_brokenLimbs.Add(cj.transform.gameObject);

                    removeLimbs.Add(cj);
                }
            }
        }

        foreach (CharacterJoint cj in removeLimbs)
        {
            m_joints.Remove(cj);
            Destroy(cj);
        }

    }

}
