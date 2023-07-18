using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AhriSkillE : MonoBehaviour
{
    public int damage;
    public float speed = 800f;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * speed);
    }

    // Update is called once per frame
    void Update()
    {
        // 시간이 지나면 삭제
        Destroy(gameObject, 1f);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Minion")
        {
            Destroy(gameObject);
            Debug.Log(collision.gameObject.tag);
        }

    }
}
