using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject bullet; // 기본 공격
    public GameObject skillQ; // Q스킬 총알
    public GameObject skillR; // R스킬 총알
    public GameObject skillW; // W스킬 총알
    public Transform firePos; // 총알이 나갈 위치
    public GameObject eEffect; // e스킬을 쓸 시 나올 effect
    public GameObject rEffect; // R스킬 쓸 시 나올 effect

    Vector3 targetPos; // 캐릭터의 이동 타겟 위치

    Behavior behavior; // 캐릭터 움직임 스크립트

    public bool isQ, isW, isE, isR = true; // 큐발사 여부
    public bool isAttack; // 기본 공격 반복을 위한 거

    public GameObject target; // 미니언
    private void Awake()
    {
        isAttack = false; // 스킬마다 false를 해줘야 한다
    }
    void Start()
    {
        behavior = GetComponent<Behavior>();

        InvokeRepeating("NormalAttack", 0.01f, 1f); // 인보크를 이용하여 1f마다 반복

    }
    void Update()
    {
        // 마우스의 위치를 반환
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (isQ) // 이즈 큐가 true면
        {
            if (Input.GetKeyDown(KeyCode.Q)) // 이즈리얼 Q, 공격을 할때 공격할 위치를 보고 있는다
            {
                behavior.ani.SetTrigger("doQ"); // 공격 애니메이션
                
                isQ = false;
                isAttack = false;

                behavior.speed = 0; // 스피드는 0

                // 월드 좌표로 하늘방향에 크기가 1인 단위 벡터와 원점을 갖는다
                Plane GroupPlane = new Plane(Vector3.up, Vector3.zero);

                float rayLength;

                // 레이가 평면과 교차했는지 여부 체크
                if (GroupPlane.Raycast(ray, out rayLength))
                {
                    // rayLength거리 에 위치값 반환
                    Vector3 pointTolook = ray.GetPoint(rayLength);

                    // pointToLook 위치 값을 캐릭터가 바라보도록 한다
                    transform.LookAt(new Vector3(pointTolook.x, transform.position.y, pointTolook.z));

                }
                StartCoroutine("MagicOneHit"); // 바로 공격하면 회전하기 직전에 공격이 나가기 때문에 코루틴으로 지체를 준다
                StartCoroutine("QcoolTime"); // 스킬 쿨타임
            }
        }
       

        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            behavior.speed = 2; // 스피드 되돌려주기

            // 마우스로 찍은 위치의 좌표 값을 가져온다
            if (Physics.Raycast(ray, out hit, 1000f))
            {
                targetPos = hit.point;
                
                if (hit.transform.name == "Minion" ) // 미니언을 클릭하면서 거리가 5f 이하하면 멈추고 기본공격
                {
                    isAttack = true;
                    behavior.speed = 0;
                    transform.LookAt(hit.transform); // 찍은 미니언을 바라본다

                }
                else // 아니라면 움직인다
                {
                    behavior.speed = 2;
                    isAttack = false;
                }
            }
            
            
        }

        if (isW)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                
                isW = false;
                isAttack = false;

                behavior.speed = 0; // 스피드는 0

                // 월드 좌표로 하늘방향에 크기가 1인 단위 벡터와 원점을 갖는다
                Plane GroupPlane = new Plane(Vector3.up, Vector3.zero);

                float rayLength;

                // 레이가 평면과 교차했는지 여부 체크
                if (GroupPlane.Raycast(ray, out rayLength))
                {
                    // rayLength거리 에 위치값 반환
                    Vector3 pointTolook = ray.GetPoint(rayLength);

                    // pointToLook 위치 값을 캐릭터가 바라보도록 한다
                    transform.LookAt(new Vector3(pointTolook.x, transform.position.y, pointTolook.z));

                }
                StartCoroutine("SkillW");
                StartCoroutine("WcoolTime");
            }
        }


        if (isE)
        {
            if (Input.GetKeyDown(KeyCode.E)) // 비전 이동 구현
            {
               
                isAttack = false;
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    // 플레이어와 찍은 위치의 거리를 구한다
                    float dist = Vector3.Distance(transform.position, hit.point);
                    // 플레이어와 미니언의 거리를 구한다
                    float attDist = Vector3.Distance(transform.position, target.transform.position);

                    // 그 거리의 차이가 3f보다 작다면 찍은 위치로 순간이동 한다
                    if (dist <= 5f)
                    {
                        isE = false; // 여기 있어야 거리가 멀때 찍혀도 발동 안한다
                        behavior.ani.SetTrigger("doE");
                        behavior.speed = 0;
                        transform.position = hit.point;

                        if(attDist <= 15f) 
                        {
                            Instantiate(bullet, firePos.position, firePos.rotation);
                        }

                        StartCoroutine("Eeffect"); // 이동 한 후 이펙트가 나오게 지연
                    }

                }
                StartCoroutine("EcoolTime"); // 스킬 쿨타임
            }
        }

        if (isR) // Q스킬과 똑같다고 보면 된다
        {
            if (Input.GetKeyDown(KeyCode.R)) // 정조준 일격 구현
            {
                behavior.ani.SetTrigger("doR");

                isR = false;
                isAttack = false;

                behavior.speed = 0; // 스피드는 0

                // 월드 좌표로 하늘방향에 크기가 1인 단위 벡터와 원점을 갖는다
                Plane GroupPlane = new Plane(Vector3.up, Vector3.zero);

                float rayLength;

                // 레이가 평면과 교차했는지 여부 체크
                if (GroupPlane.Raycast(ray, out rayLength))
                {
                    // rayLength거리 에 위치값 반환
                    Vector3 pointTolook = ray.GetPoint(rayLength);

                    // pointToLook 위치 값을 캐릭터가 바라보도록 한다
                    transform.LookAt(new Vector3(pointTolook.x, transform.position.y, pointTolook.z));

                    Instantiate(rEffect, transform.position, rEffect.transform.rotation);
                    StartCoroutine("TargetOneHit"); // 공격에 걸리는 대기 시간
                }
                
                StartCoroutine("RcoolTime");
            }
        }


        // 캐릭터가 움직인다면
        if (behavior.Run(targetPos))
        {
            behavior.Turn(targetPos); // 회전도 시켜준다
        }
        else
        {
            behavior.ani.SetFloat("doRun", 0);
        }
        
    }

    void NormalAttack()
    {
        if (isAttack == true)
        {
            behavior.ani.SetTrigger("doAttack");
            Instantiate(bullet, firePos.position, firePos.rotation); // 총알 생성
        }
    }

    IEnumerator MagicOneHit() // Q스킬
    {
        yield return new WaitForSeconds(0.01f);
        Instantiate(skillQ, firePos.position, firePos.rotation);
    }

    IEnumerator QcoolTime() // 3초 뒤에 쿨타임을 돌려준다
    {
        yield return new WaitForSeconds(3f);
        isQ = true;
    }

    IEnumerator SkillW() // w스킬
    {
        yield return new WaitForSeconds(0.01f);
        Instantiate(skillW, firePos.position, firePos.rotation);
    }

    IEnumerator WcoolTime()
    {
        yield return new WaitForSeconds(5f);
        isW = true;
    }
    IEnumerator EcoolTime()
    {
        yield return new WaitForSeconds(5f);
        isE = true;
    }

    IEnumerator Eeffect()
    {
        yield return new WaitForSeconds(0.1f);
        Instantiate(eEffect, transform.position, transform.rotation);
    }
    IEnumerator TargetOneHit()
    {
        yield return new WaitForSeconds(0.5f);
        Instantiate(skillR, firePos.position, firePos.rotation);
    }
    IEnumerator RcoolTime()
    {
        yield return new WaitForSeconds(10f);
        isR = true;
    }
  
  
}
