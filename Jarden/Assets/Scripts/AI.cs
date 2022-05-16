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
    // todo: if is touching ground
    // todo: if is not rotated properly
    // todo: make ai face enemy when targeting
    // todo: ai (ml agents? | likely just basic ai)
    // todo: ai physics (unity? | rigid bodys)
    // todo: ai movement
    // todo: ai consumption

    // potentially use events to dictate actions for the ai, event/UnityEvent
    public float boostPower = 10f;
    Rigidbody body;

    public Transform target;
    public float detectionRadius = 10f;

    public delegate IEnumerator Attacking();
    public event Attacking OnAttack;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        OnAttack += JumpAtTarget;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        StartCoroutine(OnAttack());
        Rotate();
        // Fix position (animation+ralign | play animation to readjust self, procedural ani?)
        // Wait (arbitrarytime | maybe randomized for fun)
        // Jump is ready (repeat | clear flags/jump lock)
    }

    public void OnFire()
    {
        //JumpAtTarget();
    }

    public enum AttackState
    {
        Waiting,
        Attacking
    }

    public void Rotate()
    {
        Quaternion q = Quaternion.FromToRotation(transform.up, Vector3.up) * transform.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 10);
    }

    public AttackState stateOfAttack = AttackState.Waiting;

    public IEnumerator JumpAtTarget() {
        if (stateOfAttack == AttackState.Waiting)
        {
            // Check radius for enemy (assign target | circular/potentially only if "aggrod")
            target = GetClosestEnemy(Physics.OverlapSphere(transform.position, detectionRadius, 1 << LayerMask.NameToLayer("Enemy")).ToList().Select(p => p.transform).ToList());
            if (target == null) { yield return null; }
            else
            {
                stateOfAttack = AttackState.Attacking;
                // Jump at enemy (animation+force | play animation, then jump, then landing animation)
                var heading = target.position - transform.position;
                var distance = heading.magnitude;
                var direction = heading / distance;

                // some push force that sends it towards an object
                body.AddForce(direction * boostPower, ForceMode.Impulse);
                yield return new WaitForSeconds(3);
                stateOfAttack = AttackState.Waiting;
            }
        }
    }

    public bool IsUpright
    {
        get {
            var currentx = transform.rotation.x;
            var currentz = transform.rotation.z;
            return currentx == 0 && currentz == 0;
        }
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
