using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlock : MonoBehaviour
{
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private float speed = 2f;

    private Vector3 target;

    private void Start()
    {
        if (pointA != null)
            transform.position = pointA.position;

        target = pointB.position;
    }

    private void Update()
    {
        if (pointA == null || pointB == null) return;

        // Movimento lineare tra A e B
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            // Cambia direzione
            target = target == pointA.position ? pointB.position : pointA.position;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        // Se il player è sopra, lo sposta insieme al blocco
        Rigidbody rb = collision.collider.GetComponent<Rigidbody>();
        if (rb != null && collision.collider.CompareTag("Player"))
        {
            rb.MovePosition(rb.position + (target - transform.position).normalized * speed * Time.deltaTime);
        }
    }
}
