using UnityEditor.Build.Content;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] private Vector3 spawnPosition;
    [SerializeField] private int targetScene;
    [SerializeField] private Vector3 spawnRotation;

    private void OnTriggerEnter(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.instance
        }
    }
