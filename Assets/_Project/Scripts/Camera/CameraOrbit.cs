using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    [SerializeField] private Transform targetPivot; // Pivot del player da seguire
    [SerializeField] private Vector3 offset = new Vector3(0, 1.5f, -4f);

    [SerializeField] private float mouseSensitivity = 2f;
    [SerializeField] private float clampMin = -60f;
    [SerializeField] private float clampMax = 60f;

    private float pitch; // rotazione verticale
    private float yaw;   // rotazione orizzontale
    private bool camLock = false;

    private void Awake()
    {
        if (targetPivot == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                // assume che il pivot sia un child del player chiamato "CameraPivot"
                targetPivot = player.transform.Find("CameraPivot");
                if (targetPivot == null)
                    Debug.LogWarning("CameraPivot non trovato nel player!");
            }
        }

        if (targetPivot != null)
            yaw = targetPivot.eulerAngles.y;
    }

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void LateUpdate()
    {
        if (camLock || targetPivot == null)
            return;

        // input mouse
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // aggiorna angoli
        yaw += mouseX * mouseSensitivity;
        pitch -= mouseY * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, clampMin, clampMax);

        // calcola rotazione finale della camera
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);

        // calcola posizione camera con offset relativo al pivot
        Vector3 finalOffset = rotation * offset;
        Vector3 cameraPosition = targetPivot.position + finalOffset;

        // evita che la camera scenda sotto il pivot
        if (cameraPosition.y < targetPivot.position.y + 0.5f)
            cameraPosition.y = targetPivot.position.y + 0.5f;

        transform.position = cameraPosition;
        transform.LookAt(targetPivot.position);
    }

    // blocca la camera (GameOver, Victory, ecc.)
    public void EnableCamLock() => camLock = true;

    // sblocca la camera
    public void DisableCamLock() => camLock = false;
}
