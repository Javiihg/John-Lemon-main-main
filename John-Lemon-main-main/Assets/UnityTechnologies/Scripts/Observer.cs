using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    public Transform player;
    bool m_IsPlayerInRange;
    bool hasPlayedSound;
    public GameEnding gameEnding;

    public AudioSource detectionAudioSource;
    public AudioClip detectionSound;

    public GameObject cubePrefab;

    private List<GameObject> instantiatedCubes = new List<GameObject>();

    void Start()
    {
        if (detectionAudioSource == null)
        {
            detectionAudioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        if (m_IsPlayerInRange && !hasPlayedSound)
        {
            Vector3 direction = player.position - transform.position + Vector3.up;
            Ray ray = new Ray(transform.position, direction);
            RaycastHit raycastHit;

            if (Physics.Raycast(ray, out raycastHit))
            {
                if (raycastHit.collider.transform == player)
                {
                    gameEnding.CaughtPlayer();
                    PlayDetectionSound();
                    ShowCubeOverHead(raycastHit.collider.transform);
                    hasPlayedSound = true;
                }
                else
                {
                    HideCubes();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == player)
        {
            m_IsPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform == player)
        {
            m_IsPlayerInRange = false;
            hasPlayedSound = false;
            HideCubes();
        }
    }

    void PlayDetectionSound()
    {
        if (detectionAudioSource != null && detectionSound != null)
        {
            detectionAudioSource.PlayOneShot(detectionSound);
        }
    }

    void ShowCubeOverHead(Transform targetTransform)
    {
        // cubo cabeza
        GameObject cube = Instantiate(cubePrefab, targetTransform.position + Vector3.up * 2f, Quaternion.identity);
        
        instantiatedCubes.Add(cube);
    }

    void HideCubes()
    {
        foreach (GameObject cube in instantiatedCubes)
        {
            Destroy(cube);
        }

        instantiatedCubes.Clear();
    }
}