using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private NavMeshAgent agent;
    
    public GameObject targetPlayer;
    bool IsAttack;
    int index = 0;
    float dist;
    // Start is called before the first frame update
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dist < 1.6f)
        {
            IsAttack = true;

        }
        else
        {
            IsAttack = false;
        }
    }
    private void LateUpdate()
    {
            agent.destination = targetPlayer.transform.position;
        if (IsAttack)
        {
            gameObject.GetComponent<Animator>().SetInteger("state", 1);
        }
        else
        {
            gameObject.GetComponent<Animator>().SetInteger("state", 0);
        }
        dist = Vector3.Distance(targetPlayer.transform.position, this.transform.position);
        Debug.Log(dist);

       
    }

}
