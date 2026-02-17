using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    [SerializeField] private Transform player;   // Player da seguire
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float distance = 3f; // distanza dalla testa del player
    [SerializeField] private float height = 1.5f; // altezza rispetto al player

    private float horizontalAngle = 0f;

    void Start()
    {
        if (player == null)
        {
            Debug.LogWarning("CameraOrbit: player non assegnato!");
        }
    }

    void LateUpdate()
    {
        if (player == null) return;

        // Muove l'angolo orizzontale con il mouse
        horizontalAngle += Input.GetAxis("Mouse X") * rotationSpeed;

        // Calcolo posizione della camera
        Vector3 offset = new Vector3(0, height, -distance);
        Quaternion rotation = Quaternion.Euler(0, horizontalAngle, 0); // Rotazione attorno all’asse Y
        transform.position = player.position + rotation * offset; // Posiziona la camera dietro e sopra il player

        // Guarda sempre il player
        transform.LookAt(player.position + Vector3.up * height);
    }
}
