using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MiniMind : MonoBehaviour {
    public string teamName = "";
    public float health = 10;
    public float wait = 0;
    public MiniPointMind owner;
    public GameObject badGuy;
    
    [SerializeField] GameObject punchEffect;

    NavMeshAgent agent;
    Vector3 target;

    float slowTicker = 0;

    void Start() {
        agent = GetComponent<NavMeshAgent>();
        target = GetRandomPoint();
    }

    void Update() {
        slowTicker += Time.deltaTime;

        if (wait > 0 && badGuy == null) {
            wait -= Time.deltaTime;
        }

        if (slowTicker > 0.5f) {
            if (badGuy == null) {
                FindBadGuys();
            } else {
                if (Vector3.Distance(transform.position, badGuy.transform.position) < 1.5f) {
                    Punch();
                }
            }

            slowTicker = 0;
        }

        if (agent.remainingDistance < 0.25f) {
            if (badGuy != null) {
                target = GetBadGuyPoint();
            } else {
                target = GetRandomPoint();
            }
            agent.SetDestination(target);
        }

        if (badGuy != null && Vector3.Distance(target, badGuy.transform.position) > .5f) {
            target = GetBadGuyPoint();
            agent.SetDestination(target);
        }

        if (badGuy != null && badGuy.GetComponent<MiniMind>().owner != owner) {
            badGuy = null;
            target = GetRandomPoint();
            agent.SetDestination(target);
        }
    }

    Vector3 GetRandomPoint() {
        return owner.transform.position + new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
    }

    Vector3 GetBadGuyPoint() {
        return badGuy.transform.position + (transform.position - badGuy.transform.position).normalized;
    }

    void FindBadGuys() {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 5);
        if (colliders.Length > 0) {
            foreach (var collider in colliders) {
                MiniMind miniMind = collider.GetComponentInParent<MiniMind>();
                if (miniMind != null && miniMind.teamName != teamName && miniMind.owner == owner) {
                    badGuy = miniMind.gameObject;
                    if (miniMind.badGuy == null) {
                        miniMind.badGuy = gameObject;
                    }
                }
            }
        }
    }

    void Punch() {
        if (badGuy != null) {
            MiniMind miniMind = badGuy.GetComponent<MiniMind>();
            miniMind.health -= 1;

            if (punchEffect != null) {
                Instantiate(punchEffect, badGuy.transform.position, Quaternion.LookRotation(transform.position - badGuy.transform.position));
            }

            if (miniMind.health <= 0) {
                badGuy = null;
                target = GetRandomPoint();
                agent.SetDestination(target);
            }

            print($"{teamName} punched {miniMind.teamName} for 1 damage. {miniMind.teamName} has {miniMind.health} health left.");
        }
    }
}
