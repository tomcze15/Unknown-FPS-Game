using UnityEngine;
using UnityEngine.AI;
using UnknownFPSGame.Scripts.Enemy;

public class TestEnemy : EnemyControl
{
    [SerializeField] private NavMeshAgent agent;
    public float hp;

    // Start is called before the first frame update
    void Start()
    {
        UpdateData();
        if(!agent) 
           agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(GameObject.FindGameObjectWithTag("Player").transform.position);
        hp = Health;
    }

    public override void TakeDamage(float damage)
    {
        //Bullet bullet = weapon.GetComponent<Bullet>();

        //if (bullet)
            Health -= damage;
        //else
            //return;

        if (Health <= 0)
            Destroy(this.gameObject);
    }

}
