using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCreate : MonoBehaviour
{
    public GameObject tilePre; // 타일 프리팹
    public GameObject wallPre; // 벽 프리팹

    public List<GameObject> tiles = new List<GameObject>(); // 타일이 생길 위치에 게임오브젝트
    public List<GameObject> walls = new List<GameObject>(); // 벽이 생길 위치에 게임오브젝트
    // Start is called before the first frame update
    void Start()
    {

        for (int i = 0; i < tiles.Count; i++)
        {
            tiles[i] = Instantiate(tilePre, tiles[i].transform.position, Quaternion.identity);
        }

        for (int i = 0; i < walls.Count; i++)
        {
            walls[i] = Instantiate(wallPre, walls[i].transform.position, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
