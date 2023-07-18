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

        transform.RotateAround(player.transform.position, Vector3.up, 40 * Time.deltaTime); // 아리를 중심으로 돌아라
        Destroy(gameObject, 5f); // 5초 뒤에 삭제

        if (dis <= 3f)
        {
            // 위치가 미니언에게로 움직임
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
