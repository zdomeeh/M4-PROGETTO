using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelTimer : MonoBehaviour
{
    [SerializeField] private float _timeInSeconds = 60f;

    public UnityEvent<float> OnTimeChanged;
    public UnityEvent OnTimeEnded;

    private float _currentTime;
    private bool _isRunning = true;

    void Start()
    {
        _currentTime = _timeInSeconds;
        OnTimeChanged.Invoke(_currentTime);
    }

    void Update()
    {
        if (!_isRunning)
            return;

        _currentTime -= Time.deltaTime;

        if (_currentTime < 0)
            _currentTime = 0;

        OnTimeChanged.Invoke(_currentTime);

        if (_currentTime <= 0)
        {
            _isRunning = false;
            OnTimeEnded.Invoke();
        }
    }

    public void AddTime(float seconds)
    {
        _currentTime += seconds;

        // Non superare il tempo iniziale (opzionale)
        if (_currentTime > _timeInSeconds)
            _currentTime = _timeInSeconds;

        // Aggiorna subito l'UI
        OnTimeChanged.Invoke(_currentTime);
    }
}
