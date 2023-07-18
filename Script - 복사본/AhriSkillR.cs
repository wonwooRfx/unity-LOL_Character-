using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AhriSkillR : MonoBehaviour
{
    GameObject minion;
    // Start is called before the first frame update
    void Start()
    {
        minion = GameObject.Find("Minion");
    }

    // Update is called once per frame
    void Update()
    {
        // 위치가 미니언에게로 움직임
        transform.position = Vector3.MoveTowards(transform.position, minion.transform.position, 0.05f);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Minion")
        {
            Destroy(gameObject);
        }
    }

   
}
