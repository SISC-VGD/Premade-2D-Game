using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    [SerializeField] private SceneAsset _destination;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SceneManager.LoadScene(_destination.name);
    }
}
