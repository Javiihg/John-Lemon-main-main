using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameEnding : MonoBehaviour
{
    public float fadeDuration = 1f;
    public float displayImageDuration = 1f;
    public GameObject player;
    public CanvasGroup exitBackgroundImageCanvasGroup;
    public CanvasGroup caughtBackgroundImageCanvasGroup;
    bool m_isPlayerAtExit;
    bool m_isPlayerCaught;
    float m_Timer;

    public UnityEvent onGameExit;
    public UnityEvent onGameRestart;

    void Update()
    {
        if (m_isPlayerAtExit)
        {
            EndLevel(exitBackgroundImageCanvasGroup, false, onGameExit);
        }
        else if (m_isPlayerCaught)
        {
            EndLevel(caughtBackgroundImageCanvasGroup, true, onGameRestart);
        }
    }

    public void CaughtPlayer()
    {
        m_isPlayerCaught = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            m_isPlayerAtExit = true;
        }
    }

    void EndLevel(CanvasGroup imageCanvasGroup, bool doRestart, UnityEvent gameEvent)
    {
        m_Timer += Time.deltaTime;
        imageCanvasGroup.alpha = m_Timer / fadeDuration;
        if (m_Timer > fadeDuration + displayImageDuration)
        {
            gameEvent.Invoke();
        }
    }
}