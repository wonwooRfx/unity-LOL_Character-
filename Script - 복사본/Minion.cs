using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : MonoBehaviour
{
    public GameObject wHitObj; // w에 맞을 시 나올 이펙트

    public GameObject player; // 타겟

    float speed; // 이동속도

    public bool isFascination; // 매혹에 걸렸나 확인 변수
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player"); // 플레이어 태그로 찾기
        isFascination = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(wHitObj.activeSelf == true) // 오브젝트가 활성화 되있으면 2초 뒤 비활성화
        {
            StartCoroutine("WcoolTime");
        }

        if (isFascination == true)
        {
            // 플레이어를 바라본다
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
            if(collision.gameObject.tag == "Attack") // 부딫히는 태그가 Attack일 때
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
        // 1초 뒤에 멈춘다
        yield return new WaitForSeconds(1f);
        isFascination = false;
    }
}
