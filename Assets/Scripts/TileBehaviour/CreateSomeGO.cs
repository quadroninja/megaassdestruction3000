using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateSomeGO : MonoBehaviour
{
    [SerializeField] GameObject obj;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = -20; i < 10; i++) {
            for (int j = -20; j < 10; j++)
            {
                Instantiate(obj, gameObject.GetComponent<Grid>().CellToWorld(new Vector3Int(i, j)), Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
