using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillR : MonoBehaviour
{
    public int damage;
    public float speed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        // �ð��� ������ ����
        Destroy(gameObject, 5f);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Minion")
        {
            Debug.Log(collision.gameObject.tag);
            Destroy(gameObject);
        }

    }
}
