using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState {Idle, Walk, Attack, Dead }
public class EnemyController : MonoBehaviour
{
    private NavMeshAgent agent;
    public GameObject targetPlayer;
    bool IsAttack;
    int index = 0;
    float dist;
    [HideInInspector]
    public Player playerReference;
    // Start is called before the first frame update
    void Start()
    {
        EventTriggers.onPlayerDead += ShowGameOver;
        agent = this.GetComponent<NavMeshAgent>();
        //agent.updatePosition = false;
        playerReference = targetPlayer.GetComponent<Player>();
    }
    private void OnDisable()
    {
        EventTriggers.onPlayerDead -= ShowGameOver;
    }
    // Update is called once per frame
    void Update()
    {
        if (dist < 1.5f&&!agent.isStopped)
        {
           // FollowPlayer(false);
            IsAttack = true;
        }
        else
        {
            //FollowPlayer(true);
            //agent.nextPosition = transform.position;
            //transform.rotation = agent.transform.rotation;
            IsAttack = false;
        }
    }
    private void LateUpdate()
    {
        agent.SetDestination(targetPlayer.transform.position);

      //  agent.speed = 4;
        agent.autoBraking = false;

            if (IsAttack)
            {
                ChangeState(EnemyState.Attack);
            }
            else
            {
            if (!agent.isStopped)
            {
                ChangeState(EnemyState.Walk);
            }
            else
            {
                ChangeState(EnemyState.Idle);
            }
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
      //  ChangeState(EnemyState.Idle);
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
