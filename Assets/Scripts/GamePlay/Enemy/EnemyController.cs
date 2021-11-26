using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState { Walk, Attack, Dead }
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
        EventTriggers.onPlayerDead += ShowGameOver;
        agent = this.GetComponent<NavMeshAgent>();
    }
    private void OnDisable()
    {
        EventTriggers.onPlayerDead -= ShowGameOver;
    }
    // Update is called once per frame
    void Update()
    {
        if (dist < 1.2f)
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
            ChangeState(EnemyState.Attack);
        }
        else
        {
            ChangeState(EnemyState.Walk);
        }
        dist = Vector3.Distance(targetPlayer.transform.position, this.transform.position);
       
    }
    public void FollowPlayer(bool can)
    {
        if (can)
        {
            agent.isStopped = false;
        }
        else
        {
            agent.isStopped = true;
        }
    }
    public void ShowGameOver()
    {
        agent.isStopped = true;
    }
    public bool CanAttack()
    {
        return IsAttack;
    }
    public void ChangeState(EnemyState state)
    {
        gameObject.GetComponent<Animator>().SetInteger("state", (int)state);
    }

}
