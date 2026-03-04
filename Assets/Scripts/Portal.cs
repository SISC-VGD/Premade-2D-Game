using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Portal : MonoBehaviour
{
    [SerializeField] private Portal _destination;

    private readonly List<GameObject> _cantEnter = new();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_cantEnter.Contains(collision.gameObject))
            return;

        if (collision.TryGetComponent(out Rigidbody2D rb2D))
        {
            Debug.Log($"{collision.name} teleported to {_destination.transform.position}");
            
            rb2D.transform.position = _destination.transform.position;
            _destination._cantEnter.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Exit trigger");
        _cantEnter.Remove(collision.gameObject);
    }

    private void OnDrawGizmos()
    {
        if (_destination)
            Gizmos.DrawLine(transform.position, _destination.transform.position);
    }
}
