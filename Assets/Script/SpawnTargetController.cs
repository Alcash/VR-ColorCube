using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTargetController : MonoBehaviour {


    public List<GameObject> targetPrefabs;

    Material signalMaterial;

    float spawnSpeed = 2f;
    float cooldownSpawnTime = 0;

    bool canSpawn = true;
    int indexTarget;
    public Color[] SignalColor = new Color[4] { Color.white, Color.red, Color.green, Color.black };

    private void Start()
    {
        if (targetPrefabs.Count < 1)
        {
            targetPrefabs = new List<GameObject>();
            targetPrefabs.AddRange(Resources.LoadAll<GameObject>("Cubik"));
        }

        signalMaterial = transform.GetChild(0).GetComponent<Renderer>().sharedMaterial;

        indexTarget = 1;
    }

private void FixedUpdate()
    {
        CoolDown(Time.fixedDeltaTime);
    }

    void CoolDown(float deltaTime)
    {
        if (cooldownSpawnTime > 0)
        {
            cooldownSpawnTime -= deltaTime;
        }
        else
        {
            canSpawn = true;
            SpawnTarget();
        }
    }

    void SpawnTarget()
    {
        cooldownSpawnTime = spawnSpeed;

        
        var target = Instantiate(targetPrefabs[indexTarget], transform.position, Quaternion.identity);
        indexTarget = Random.Range(0, targetPrefabs.Count);
        signalMaterial.color = SignalColor[indexTarget];
    }
}
