using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AhriSkillW : MonoBehaviour
{
    GameObject player;

    GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Ahri");
        target = GameObject.Find("Minion");
    }

    // Update is called once per frame
    void Update()
    {
        float dis = Vector3.Distance(transform.position, target.transform.position);

        transform.RotateAround(player.transform.position, Vector3.up, 40 * Time.deltaTime); // �Ƹ��� �߽����� ���ƶ�
        Destroy(gameObject, 5f); // 5�� �ڿ� ����

        if (dis <= 3f)
        {
            // ��ġ�� �̴Ͼ𿡰Է� ������
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, 0.03f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Minion")
        {
            Destroy(gameObject);
        }
    }
}
