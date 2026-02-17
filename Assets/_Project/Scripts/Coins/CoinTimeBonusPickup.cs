using UnityEngine;

public class CoinTimeBonusPickup : MonoBehaviour
{
    [SerializeField] private float _timeBonus = 10f;
    [SerializeField] private AudioClip _pickupClip;

    private void OnTriggerEnter(Collider other)
    {
        PlayerCoinCollector collector = other.GetComponent<PlayerCoinCollector>();
        if (collector != null)
        {
            // Trova il LevelTimer in scena
            LevelTimer timer = FindObjectOfType<LevelTimer>();
            if (timer != null)
            {
                timer.AddTime(_timeBonus); // aggiunge tempo
            }

            // Riproduce il suono della time coin
            if (_pickupClip != null)
                AudioSource.PlayClipAtPoint(_pickupClip, transform.position);

            Destroy(gameObject); // rimuove la moneta bonus
        }
    }
}
