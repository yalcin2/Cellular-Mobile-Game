using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableScript : MonoBehaviour
{

    public GameObject consumable;

    public GameObject holder;

    private bool timePassed;

    public static bool canSpawn;

    public Vector3 center;
    private Vector3 pos;

    public Vector3 size;

    // Start is called before the first frame update
    void Start()
    {
        size.x = 65.5f;
        size.y = 58f;

        timePassed = true;
        canSpawn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (timePassed && canSpawn && ValuesScript.fastestCellSpawnUnlocked == "no")
        {
            timePassed = false;
            StartCoroutine(SpawnConsumablesRandomly());
        }
        else if (ValuesScript.cellsSpawnUpgradeLevel == 10 &&
                 ValuesScript.fastestCellSpawnUnlocked == "yes" && canSpawn)
        {
            Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2));
            GameObject clone1 = Instantiate(consumable, pos, Quaternion.identity);
            clone1.transform.parent = holder.transform;
            ValuesScript.availableCoins++;
        }
    }


    private IEnumerator SpawnConsumablesRandomly()
    {
        pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2));
        GameObject clone1 = Instantiate(consumable, pos, Quaternion.identity);
        clone1.transform.parent = holder.transform;
        yield return new WaitForSeconds(ValuesScript.cellsSpawnTime);
        ValuesScript.availableCoins++;
        timePassed = true;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0.5f, 0.5f, 0.5f, 0.2f);

        Gizmos.DrawCube(center,size);
    }

}
