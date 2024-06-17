using UnityEngine;

public class ResourceTowerBehavior : MonoBehaviour, TowerBehavior
{
    GeneratorBehavior generatorBehavior;
    public float generationCooldown;
    private float nextGenerationTime;

    public AudioClip AttackSound;
    
    void Start(){
        generatorBehavior = GetComponent<GeneratorBehavior>();
        nextGenerationTime = 0;
    }
    public void action()
    {
        nextGenerationTime+=Time.deltaTime;
        if(nextGenerationTime >= generationCooldown){
            nextGenerationTime = 0;
            generatorBehavior.generate();
            AudioSource ac = GetComponent<AudioSource>();
            ac.PlayOneShot(AttackSound);
        }
    }
}