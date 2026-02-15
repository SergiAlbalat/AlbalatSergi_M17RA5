using System;
using UnityEngine;
[Serializable]
public class SaveData
{
    [SerializeField] private Vector3 _playerPosition;
    public SaveData(Vector3 playerPosition)
    {
        _playerPosition = playerPosition;
    }
    public Vector3 GetPlayerPosition => _playerPosition;
}
