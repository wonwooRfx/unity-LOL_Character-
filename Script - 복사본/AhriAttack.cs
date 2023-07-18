using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AhriAttack : MonoBehaviour
{
    public GameObject skillQ; // Q스킬 총알
    public GameObject[] skillWs = new GameObject[3]; // w스킬 3개
    public GameObject skillE; // 스킬E 하트
    public GameObject skillR; // 스킬 R 총알
    
    public Transform firePos; // 총알이 나갈 위치

    Vector3 targetPos; // 캐릭터의 이동 타겟 위치

    Behavior behavior; // 캐릭터 움직임 스크립트

    public bool isQ, isW, isE, isR = true;
    public GameObject target; // 미니언

    int skillwSize = 3; // 개수
    float circleR =  0.5f; //반지름
    float deg; //각도
    float objSpeed = 5; //원운동 속도

    int rCount; // 스킬 횟수
    void Start()
    {
        behavior = GetComponent<Behavior>();

    }
    void Update()
    {
        // 마우스의 위치를 반환
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (isQ) // 아리 큐가 true면
        {
            if (Input.GetKeyDown(KeyCode.Q)) // 아리 Q, 공격을 할때 공격할 위치를 보고 있는다
            {
                //behavior.ani.SetTrigger("doQ"); // 공격 애니메이션

                isQ = false;

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
                StartCoroutine("Diseption"); // 바로 공격하면 회전하기 직전에 공격이 나가기 때문에 코루틴으로 지체를 준다
                StartCoroutine("QcoolTime");

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
            }


        }

        if (isW)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                isW = false;
                deg += Time.deltaTime * objSpeed;

               /* GameObject skillw1 = Instantiate(skillWs[0]);
                GameObject skillw2 = Instantiate(skillWs[1]);
                GameObject skillw3 = Instantiate(skillWs[2]);

                // 게임오브젝트의 자식화
                skillw1.transform.SetParent(this.transform, false);
                skillw2.transform.SetParent(this.transform, false);
                skillw3.transform.SetParent(this.transform, false);

                skillw1.transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z + 1);
                skillw2.transform.position = new Vector3(transform.position.x + 0.6f, transform.position.y + 1, transform.position.z - 0.3f);
                skillw3.transform.position = new Vector3(transform.position.x - 0.6f, transform.position.y + 1, transform.position.z - 0.3f);*/


                if (deg < 360)
                {
                    // y축을 기준으로 120도로 회전한다
                    for (int i = 0; i < skillwSize; i++)
                    {
                        // 프리팹을 담아줄 게임오브젝트
                        GameObject[] skillw = new GameObject[3];
                        skillw[i] = Instantiate(skillWs[i]);

                        // 플레이어의 자식화를 시켜준다
                        skillw[i].transform.SetParent(this.transform, false);

                        // 120도 각도로 3개를 원 모양으로 배치시킨다(참고로 도는 건 따로 해줘야한다)
                        var rad = Mathf.Deg2Rad * (deg + (i * (360 / skillwSize)));
                        var x = circleR * Mathf.Sin(rad);
                        var y = circleR * Mathf.Cos(rad);
                        skillw[i].transform.position = transform.position + new Vector3(x, 1, y); // 이 부분이 키포인트
                        skillw[i].transform.rotation = Quaternion.Euler(0, 0, (deg + (i * (360 / skillwSize))) * -1);

                    }
                }
                else
                {
                    deg = 0;
                }

                StartCoroutine("WcoolTime");
            }
        }

        if (isE)
        {
            if (Input.GetKeyDown(KeyCode.E)) // 아리 E, 공격을 할때 공격할 위치를 보고 있는다
            {
                isE = false;
          
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
                StartCoroutine("Fascination"); // 바로 공격하면 회전하기 직전에 공격이 나가기 때문에 코루틴으로 지체를 준다
                StartCoroutine("EcoolTime"); // 스킬 쿨타임
            }
        }

        if (isR)
        {
            if (Input.GetKeyDown(KeyCode.R)) // 혼령질주
            {
               
                RaycastHit hit;
                // 마우스로 찍은 위치의 좌표 값을 가져온다
                if (Physics.Raycast(ray, out hit, 1000f))
                {
                    // 플레이어와 찍은 위치의 거리를 구한다
                    float dist = Vector3.Distance(transform.position, hit.point);
                    // 플레이어와 미니언의 위치의 거리를 구한다
                    float dis = Vector3.Distance(transform.position, target.transform.position);
                    if(dist <= 3f)
                    {
                        behavior.speed = 10; // 스피드를 올려서 빠르게 질주한다
                        targetPos = hit.point;

                        rCount++; // 누를 때마다 한번씩 카운트 추가
                        if (rCount == 3) // 세번 누르면 false
                        {
                            isR = false;
                            StartCoroutine("RcoolTime");
                        }

                        if (dis <= 5f)
                        {
                            Instantiate(skillR, transform.position, transform.rotation);
                        }

                    }

                }
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

    IEnumerator Diseption()
    {
        yield return new WaitForSeconds(0.01f);
        Instantiate(skillQ, firePos.position, firePos.rotation);
    }
    IEnumerator QcoolTime()
    {
        yield return new WaitForSeconds(5f);
        isQ = true;
    }
    IEnumerator WcoolTime()
    {
        yield return new WaitForSeconds(5f);
        isW = true;
    }
    IEnumerator Fascination()
    {
        yield return new WaitForSeconds(0.01f);
        Instantiate(skillE, firePos.position, firePos.rotation);
    }
    IEnumerator EcoolTime()
    {
        yield return new WaitForSeconds(5f);
        isE = true;
    }
    IEnumerator RcoolTime()
    {
        yield return new WaitForSeconds(10f);
        rCount = 0;
        isR = true;
    }



}
