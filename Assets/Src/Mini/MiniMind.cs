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
    public State state;
    public GameObject badGuy;
    
    
    [SerializeField] float slowTickerInterval = 0.5f;
    [SerializeField] GameObject punchEffect;

    NavMeshAgent agent;
    Vector3 target;

    float slowTicker = 0;

    public enum State {
        Idle,
        Walking,
        Transport,
        Combat
    }

    void Start() {
        agent = GetComponent<NavMeshAgent>();
        target = GetRandomPoint();
    }

    void Update() {
        switch (state) {
            case State.Idle:
                slowTicker += Time.deltaTime;
                
                if (wait < 0) {
                    wait -= Time.deltaTime;
                }

                if (slowTicker > slowTickerInterval) {
                    if (badGuy == null) {
                        FindBadGuys();
                    }

                    if (badGuy == null) {
                        // agent.
                        GoToPoint(GetRandomPoint());
                    } else {
                        state = State.Combat;
                    }
                }
                break;
            
            case State.Walking:
                if (agent.remainingDistance < 0.25f) {
                    state = State.Idle;
                }
                break;

            case State.Transport:
                if ((owner.transform.position - transform.position).magnitude < 10) {
                    state = State.Idle;
                }
                break;
            
            case State.Combat:
                slowTicker += Time.deltaTime;

                if (slowTicker > slowTickerInterval) {
                    if (badGuy == null) {
                        state = State.Idle;
                    }

                    if (badGuy != null) {
                        if (Vector3.Distance(transform.position, badGuy.transform.position) < 1.5f) {
                            Punch();
                        }
                    }
                }
                break;
        }

        // if (wait > 0 && badGuy == null) {
        //     wait -= Time.deltaTime;
        // }

        // if (slowTicker > 0.5f) {
        //     if (badGuy == null) {
        //         FindBadGuys();
        //     } else {
        //         if (Vector3.Distance(transform.position, badGuy.transform.position) < 1.5f) {
        //             Punch();
        //         }
        //     }

        //     slowTicker = 0;
        // }

        // if (agent.remainingDistance < 0.25f) {
        //     if (badGuy != null) {
        //         target = GetBadGuyPoint();
        //     } else {
        //         target = GetRandomPoint();
        //     }
        //     agent.SetDestination(target);
        // }

        // if (badGuy != null && Vector3.Distance(target, badGuy.transform.position) > .5f) {
        //     target = GetBadGuyPoint();
        //     agent.SetDestination(target);
        // }

        // if (badGuy != null && badGuy.GetComponent<MiniMind>().owner != owner) {
        //     badGuy = null;
        //     target = GetRandomPoint();
        //     agent.SetDestination(target);
        // }
    }

    void GoToPoint(Vector3 point) {
        target = point;
        agent.SetDestination(point);
        state = State.Walking;
    }

    Vector3 GetRandomPoint() {
        return owner.transform.position + new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
    }

    Vector3 GetBadGuyPoint() {
        return badGuy.transform.position + (transform.position - badGuy.transform.position).normalized;
    }

    void FindBadGuys() {
        badGuy = null;

        Collider[] colliders = Physics.OverlapSphere(transform.position, 5);
        if (colliders.Length > 0) {
            foreach (var collider in colliders) {
                MiniMind miniMind = collider.GetComponentInParent<MiniMind>();
                if (miniMind != null && miniMind.teamName != teamName && miniMind.owner == owner && miniMind.state != State.Walking) {
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
