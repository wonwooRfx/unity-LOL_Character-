using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AhriSkillQ : MonoBehaviour
{
    public int damage;
    public float speed = 500f;

    Transform firePos; // �Ƹ��� Ʈ������

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
        // �÷��̾�� �ڽ��� ��ġ�� �Ÿ��� ���Ѵ�
        float dist = Vector3.Distance(transform.position, firePos.position);

        if (dist >= 3f) // 1f�� ������
        {
            // �ٽ� �Ƹ� ��ġ�� ���ƿͶ�
            transform.position = Vector3.Lerp(transform.position, firePos.position, 100f*Time.deltaTime);
            Destroy(gameObject, 1f);

        }
    }

  
}
