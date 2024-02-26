using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.AI.Navigation;
using UnityEngine;

public class MiniPointMind : MonoBehaviour {
    public string teamName = "fungen";
    public Material teamColor;
    public List<GameObject> links = new List<GameObject>();

    [SerializeField] GameObject nextPoint;
    [SerializeField] GameObject miniPrefab;
    [SerializeField] int maxCount = 10;

    [SerializeField] float spawbInterval = 3;
    [SerializeField] float spawbRadius = 2;

    List<GameObject> minis = new List<GameObject>();

    float deltaPile = 0;

    void Start() {
        if (teamColor != null) {
            GetComponent<MeshRenderer>().material = teamColor;
        } else {
            teamColor = GetComponent<MeshRenderer>().material;
        }
    }

    void Update() {
        deltaPile += Time.deltaTime;

        foreach (var mini in minis.ToList()) {
            MiniMind miniMind = mini.GetComponent<MiniMind>();
            if (miniMind.health <= 0) {
                DestroyMini(mini);
            }
        }

        if (nextPoint != null) {
            foreach (var mini in minis.ToList()) {
                MiniMind miniMind = mini.GetComponent<MiniMind>();
                if (miniMind.wait <= 0) {
                    TransferMini(mini, nextPoint);
                }
            }
        }
        
        if (deltaPile > spawbInterval && minis.Count < maxCount) {
            CreateMini();
            deltaPile = 0;
        }
    }

    void CreateMini() {
        GameObject mini = Instantiate(
            miniPrefab,
            transform.position + new Vector3(
                Random.Range(-spawbRadius, spawbRadius),
                0,
                Random.Range(-spawbRadius, spawbRadius)),
            Quaternion.identity);
        
        MiniMind miniMind = mini.GetComponent<MiniMind>();
        miniMind.owner = this;
        miniMind.wait = Random.Range(2, 7);

        miniMind.GetComponentInChildren<MeshRenderer>().material = teamColor;
        miniMind.teamName = teamName;

        minis.Add(mini);
    }

    void DestroyMini(GameObject mini) {
        minis.Remove(mini);
        Destroy(mini);

        VerifyCaptue();
    }

    void TransferMini(GameObject mini, GameObject point) {
        minis.Remove(mini);
        MiniPointMind miniPointMind = point.GetComponent<MiniPointMind>();
        miniPointMind.minis.Add(mini);

        MiniMind miniMind = mini.GetComponent<MiniMind>();
        miniMind.owner = miniPointMind;
        miniMind.wait = 15 + Random.Range(2, 7);
        miniMind.state = MiniMind.State.Walking;

        miniPointMind.VerifyCaptue();
    }

    void VerifyCaptue() {
        List<string> teams = new List<string>();
        foreach (var mini in minis) {
            MiniMind miniMind = mini.GetComponent<MiniMind>();
            if (!teams.Contains(miniMind.teamName)) {
                teams.Add(miniMind.teamName);
            }
        }

        if (teams.Count == 1) {
            teamName = teams[0];
            teamColor = minis[0].GetComponentInChildren<MeshRenderer>().material;
            GetComponent<MeshRenderer>().material = teamColor;
        }
    }
}
