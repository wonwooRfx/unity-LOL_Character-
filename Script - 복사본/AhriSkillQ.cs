using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AhriSkillQ : MonoBehaviour
{
    public int damage;
    public float speed = 500f;

    Transform firePos; // 아리의 트랜스폼

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        firePos = GameObject.Find("FireFos").transform;
        GetComponent<Rigidbody>().AddForce(transform.forward * speed);
        
    }

    // Update is called once per frame
    void Update()
    {
        // 플레이어와 자신의 위치의 거리를 구한다
        float dist = Vector3.Distance(transform.position, firePos.position);

        if (dist >= 3f) // 1f이 지나면
        {
            // 다시 아리 위치로 돌아와라
            transform.position = Vector3.Lerp(transform.position, firePos.position, 100f*Time.deltaTime);
            Destroy(gameObject, 1f);

        }
    }

  
}
