using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobOverMind : MonoBehaviour {
    public GameObject mainTower;
    public int maxMobs = 10;
    public GameObject spawnPoint;
    public Vector3 spawnRange = new Vector3(5, 0, 0);
    public List<GameObject> mobPrefabs = new List<GameObject>();

    private List<GameObject> mobs = new List<GameObject>();
    private float ticker = 0;

    void SpawnMob() {
        mobs.RemoveAll(mob => mob == null);

        if (mobs.Count >= maxMobs)
            return;

        GameObject mob = Instantiate(
            mobPrefabs[0],
            spawnPoint.transform.position + new Vector3(
                Random.Range(-spawnRange.x, spawnRange.x),
                Random.Range(-spawnRange.y, spawnRange.y),
                Random.Range(-spawnRange.z, spawnRange.z)),
            Quaternion.identity);
        mob.GetComponent<MobMind>().target = mainTower;
        
        mobs.Add(mob);
    }

    void Start() {
        
    }

    void Update() {
        ticker += Time.deltaTime;

        if (ticker > 1) {
            SpawnMob();
            ticker = 0;
        }
    }
}
