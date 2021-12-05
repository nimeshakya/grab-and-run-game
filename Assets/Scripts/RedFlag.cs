using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedFlag : MonoBehaviour
{
    private GameObject Player;

    private Player _playerScript;

    private Transform target; // player's position

    [SerializeField]
    private Vector3 offset; // offset for flag to hover above player

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        target = Player.transform;
        _playerScript = Player.GetComponent<Player>();
    }

    private void LateUpdate()
    {
        if (!target)
            return;

        Vector3 targetPosition = target.position + offset;

        if (_playerScript.redFlagContact)
            transform.position = targetPosition;
    }
}
