using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "ScriptableObjects/TileDataObject")]
public class TileDataObject : ScriptableObject
{
    [SerializeField] Tile tile;
    public Tile Tile { get { return tile; } }
    [SerializeField] int id;
    public int Id { get { return id; } }

    [SerializeField] string scriptName;
    public string ScriptName { get { return scriptName; } }


    public bool isPassable;
}
