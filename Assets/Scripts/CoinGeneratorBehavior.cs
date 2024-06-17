using UnityEngine;

public class CoinGeneratorBehavior : MonoBehaviour, GeneratorBehavior
{
    public GameObject coinPrefab;
    public int amountToGenerate;
    public void generate()
    {
        GetComponent<Animator>().SetTrigger("Action");
    }

    public void startGenerate(){
        GameObject generatedCoin = Instantiate(coinPrefab,transform.position,Quaternion.identity);
        generatedCoin.transform.Translate(Vector3.up*0.5f);
        Destroy(generatedCoin,1);
        GameManager.Instance.playerMoney+=amountToGenerate;
    }
}