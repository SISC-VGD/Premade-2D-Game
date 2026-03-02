using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private uint _worth = 1;
    private static Dictionary<PlayerController, uint> _totalWorths = new();

    [SerializeField] private float _bobSpeed = 1;
    [SerializeField] private float _bobAmount = 0.25f;

    private Vector2 _position;

    private void Awake()
    {
        _position = transform.position;
    }

    private void Update()
    {
        transform.position = _position + new Vector2(0, Mathf.Sin((Time.time * _bobSpeed) + (_position.x * 0.25f)) * _bobAmount);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerController playerController))
        {
            if (!_totalWorths.ContainsKey(playerController))
                _totalWorths.Add(playerController, 0);

            _totalWorths[playerController] += _worth;
            Destroy(gameObject);
        }
    }
}
