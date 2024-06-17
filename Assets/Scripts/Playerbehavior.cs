using System.Collections;
using UnityEngine;

public class Playerbehavior : MonoBehaviour
{
    TowerBehavior towerBehavior;
    public int totalHealth;
    private int currentHealth;
    private Rigidbody2D rb;

    public HealthBar healthBar;

    // Start is called before the first frame update
    void Awake(){
        rb = GetComponent<Rigidbody2D>();
        towerBehavior = GetComponent<TowerBehavior>();
        currentHealth = totalHealth;
        healthBar.setMaxHealth(totalHealth);
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        towerBehavior.action();
    }

    public void LoseHealth(int amount){
        currentHealth -= amount;
        StartCoroutine("blinkRedEffect");
        if(currentHealth <=  0){
            transform.parent.parent.gameObject.GetComponent<DropUnit>().occupied = false;
            Destroy(transform.parent.gameObject);
        }
        healthBar.setHealth(currentHealth);
    }

    IEnumerator blinkRedEffect(){
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.2f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}
