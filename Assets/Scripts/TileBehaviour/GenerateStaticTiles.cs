using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GenerateStaticTiles : MonoBehaviour
{
    
    [SerializeField] Tilemap tilemap;
    [SerializeField] Tile groundTile;

    private void Awake()
    {
    }
    
    void Start()
    {
        for (int i = -20; i <= 20; i++)
        {
            for (int j = -20; j <= 20; j++)
            {
                tilemap.SetTile(new Vector3Int(i, j), groundTile);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
