using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : MonoBehaviour
{
    public GameObject wHitObj; // w�� ���� �� ���� ����Ʈ

    public GameObject player; // Ÿ��

    float speed; // �̵��ӵ�

    public bool isFascination; // ��Ȥ�� �ɷȳ� Ȯ�� ����
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player"); // �÷��̾� �±׷� ã��
        isFascination = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(wHitObj.activeSelf == true) // ������Ʈ�� Ȱ��ȭ �������� 2�� �� ��Ȱ��ȭ
        {
            StartCoroutine("WcoolTime");
        }

        if (isFascination == true)
        {
            // �÷��̾ �ٶ󺻴�
            transform.LookAt(player.transform.position);
           
            speed = 2f;
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            StartCoroutine("FascinationTime");
        }
       
    }

    IEnumerator WcoolTime()
    {
        yield return new WaitForSeconds(2f);
        wHitObj.SetActive(false);
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "SkillW")
        {
            wHitObj.SetActive(true);
        }

        if(wHitObj.activeSelf == true)
        {
            if(collision.gameObject.tag == "Attack") // �΋H���� �±װ� Attack�� ��
            {
                wHitObj.SetActive(false);
            }
        }

        if (collision.gameObject.CompareTag("Fascination"))
        {
            isFascination = true;
        }
    }

    IEnumerator FascinationTime()
    {
        // 1�� �ڿ� �����
        yield return new WaitForSeconds(1f);
        isFascination = false;
    }
}
