using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckDropzone : MonoBehaviour
{
    [SerializeField] private GameObject enemyDropzone;
    [SerializeField] private GameObject playerDropzone;

    private static GameObject _enemyDropzone;
    private static GameObject _playerDropzone;

    public static bool IsEnemyDropzoneEmpty { get => _enemyDropzone.transform.childCount <= 0; }
    public static bool IsPlayerDropzoneEmpty { get => _playerDropzone.transform.childCount <= 0; }

    private void Awake()
    {
        _enemyDropzone = enemyDropzone;
        _playerDropzone = playerDropzone;
    }
}
