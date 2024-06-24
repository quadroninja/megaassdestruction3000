using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


//требует применения на объект Grid
//baseTilemap разбивает по логическим Tilemap-ам
public class MapManager : MonoBehaviour
{
    [SerializeField] Tilemap baseTilemap;
    [SerializeField] TileDataObject[] tileDatas;
    Dictionary<TileDataObject, Tilemap> dataToTilemap;
    Dictionary<Tilemap, TileDataObject> tilemapToData;
    Dictionary<TileBase, TileDataObject> tileToData;
    

    void Awake()
    {
        dataToTilemap = new Dictionary<TileDataObject, Tilemap>();
        tilemapToData = new Dictionary<Tilemap, TileDataObject>();
        tileToData = new Dictionary<TileBase, TileDataObject>();
        

        int i = 0;
        foreach (TileDataObject dataObject in tileDatas) //инициализация логических Tilemap-ов
        {
            GameObject go = new GameObject($"{dataObject.Id}");
            Tilemap newTilemap = go.AddComponent<Tilemap>();
            TilemapCollider2D collider = go.AddComponent<TilemapCollider2D>();
            go.AddComponent<TilemapRenderer>();
            go.AddComponent(System.Type.GetType(dataObject.ScriptName));

            tilemapToData[newTilemap] = dataObject;
            dataToTilemap[dataObject] = newTilemap;

            collider.isTrigger = true;
            go.transform.SetParent(this.transform);
            i++;
        }

        BoundsInt bounds = baseTilemap.cellBounds;
        TileBase[] allTiles = baseTilemap.GetTilesBlock(bounds);
        Vector3Int topLeftPos = bounds.min;

        for (int x = 0; x < bounds.size.x; x++)
        {
            for (int y = 0; y < bounds.size.y; y++)
            {
                int index = x + y * bounds.size.x;
                TileBase tile = allTiles[index];
                if (tile == null)
                    continue;

                TileDataObject dataValue;
                if (!tileToData.TryGetValue(tile, out dataValue))
                {
                    foreach (TileDataObject dataObject in tileDatas)
                    {
                        if (dataObject.Tile == tile)
                        {
                            tileToData[tile] = dataObject;
                            dataValue = dataObject;
                            break;
                        }
                    }
                }
                dataToTilemap[dataValue].SetTile(topLeftPos + new Vector3Int(x, y), tile);
            }

        }
    }
};
