using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System;

public class AI : MonoBehaviour
{
    // todo: ai (ml agents? | likely just basic ai)
    // todo: ai physics (unity? | rigid bodys)
    // todo: ai movement
    // todo: ai consumption
    public float boostPower = 10f;
    Rigidbody body;

    public Transform target;
    public float detectionRadius = 10f;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Check radius for enemy (assign target | circular/potentially only if "aggrod")
        target = GetClosestEnemy(Physics.OverlapSphere(transform.position, detectionRadius, 1 << LayerMask.NameToLayer("Enemy")).ToList().Select(p => p.transform).ToList());
        // Jump at enemy (animation+force | play animation, then jump, then landing animation)
        JumpAtTarget();
        // Fix position (animation+ralign | play animation to readjust self, procedural ani?)
        // Wait (arbitrarytime | maybe randomized for fun)
        // Jump is ready (repeat | clear flags/jump lock)
    }

    public void OnFire()
    {
        //JumpAtTarget();
    }

    object x = new object();

    public void JumpAtTarget() {
        if (target == null) { return; }
        var heading = target.position - transform.position;
        var distance = heading.magnitude;
        var direction = heading / distance;

        // some push force that sends it towards an object
        body.AddForce(direction * boostPower, ForceMode.Impulse);
        //Thread.Sleep(TimeSpan.FromSeconds(5));
    }

    Transform GetClosestEnemy(IList<Transform> enemies)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        for (int i = 0; i < enemies.Count; i++)
        {
            Vector3 directionToTarget = enemies[i].position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = enemies[i];
            }
        }

        return bestTarget;
    }
}
