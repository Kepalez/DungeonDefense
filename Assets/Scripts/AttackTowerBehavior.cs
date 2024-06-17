using UnityEngine;

public class AttackTowerBehavior : MonoBehaviour, TowerBehavior{

    public float AttackDistance;
    public int attackPower;
    [SerializeField] float attackCooldown;
    [SerializeField] float nextAttackTime;
    [SerializeField] LayerMask detectLayer;
    AttackBehavior attackBehavior;
    public GameObject currentTarget;

    void Start(){
        attackBehavior = GetComponent<AttackBehavior>();
    }

    public void action(){ //This will work as an update method
        nextAttackTime+=Time.deltaTime;
        detectObjects();
    }

    void detectObjects(){
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right,AttackDistance,detectLayer);
    
        // If it hits something...
        if (hit.collider != null)
        {
            float distance = Mathf.Abs(hit.point.x - transform.position.x);
            //Debug.Log (hit.collider.gameObject.name);
            if(distance <= AttackDistance){
                currentTarget = hit.collider.gameObject;
                Attack();
                //Change to attack behavior
            }else{
                Idle();
            }
        }else{
            Idle();
        }
    }

    private void Idle()
    {
        currentTarget = null;
    }

    private void Attack()
    {
        if(nextAttackTime < attackCooldown) return;
        nextAttackTime = 0;
        attackBehavior.attack();
    }
} 