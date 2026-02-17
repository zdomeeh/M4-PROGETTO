using UnityEngine;

public class MovingBlock : MonoBehaviour
{
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private float speed = 2f;

    private Vector3 target;

    private void Start()
    {
        if (pointA != null) // Imposta la posizione iniziale del blocco al punto A
        {
            transform.position = pointA.position;
        }

        target = pointB.position; // Imposta come target iniziale il punto B
    }

    private void Update()
    {
        if (pointA == null || pointB == null) return; // Controlla che entrambi i punti siano assegnati

        // Movimento lineare del blocco verso il target
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            // Cambia direzione
            target = target == pointA.position ? pointB.position : pointA.position;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        Rigidbody rb = collision.collider.GetComponent<Rigidbody>(); // Se il player collide il blocco viene spostato

        if (rb != null && collision.collider.CompareTag("Player")) // Verifica che ci sia un Rigidbody e che il collider sia il Player
        {
            rb.MovePosition(rb.position + (target - transform.position).normalized * speed * Time.deltaTime); // Muove il player nella stessa direzione del blocco, tenendo conto della velocità e deltaTime
        }
    }
}
