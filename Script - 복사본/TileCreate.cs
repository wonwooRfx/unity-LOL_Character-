using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCreate : MonoBehaviour
{
    public GameObject tilePre; // Ÿ�� ������
    public GameObject wallPre; // �� ������

    public List<GameObject> tiles = new List<GameObject>(); // Ÿ���� ���� ��ġ�� ���ӿ�����Ʈ
    public List<GameObject> walls = new List<GameObject>(); // ���� ���� ��ġ�� ���ӿ�����Ʈ
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
