using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AhriAttack : MonoBehaviour
{
    public GameObject skillQ; // Q��ų �Ѿ�
    public GameObject[] skillWs = new GameObject[3]; // w��ų 3��
    public GameObject skillE; // ��ųE ��Ʈ
    public GameObject skillR; // ��ų R �Ѿ�
    
    public Transform firePos; // �Ѿ��� ���� ��ġ

    Vector3 targetPos; // ĳ������ �̵� Ÿ�� ��ġ

    Behavior behavior; // ĳ���� ������ ��ũ��Ʈ

    public bool isQ, isW, isE, isR = true;
    public GameObject target; // �̴Ͼ�

    int skillwSize = 3; // ����
    float circleR =  0.5f; //������
    float deg; //����
    float objSpeed = 5; //��� �ӵ�

    int rCount; // ��ų Ƚ��
    void Start()
    {
        behavior = GetComponent<Behavior>();

    }
    void Update()
    {
        // ���콺�� ��ġ�� ��ȯ
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (isQ) // �Ƹ� ť�� true��
        {
            if (Input.GetKeyDown(KeyCode.Q)) // �Ƹ� Q, ������ �Ҷ� ������ ��ġ�� ���� �ִ´�
            {
                //behavior.ani.SetTrigger("doQ"); // ���� �ִϸ��̼�

                isQ = false;

                behavior.speed = 0; // ���ǵ�� 0

                // ���� ��ǥ�� �ϴù��⿡ ũ�Ⱑ 1�� ���� ���Ϳ� ������ ���´�
                Plane GroupPlane = new Plane(Vector3.up, Vector3.zero);

                float rayLength;

                // ���̰� ���� �����ߴ��� ���� üũ
                if (GroupPlane.Raycast(ray, out rayLength))
                {
                    // rayLength�Ÿ� �� ��ġ�� ��ȯ
                    Vector3 pointTolook = ray.GetPoint(rayLength);

                    // pointToLook ��ġ ���� ĳ���Ͱ� �ٶ󺸵��� �Ѵ�
                    transform.LookAt(new Vector3(pointTolook.x, transform.position.y, pointTolook.z));

                }
                StartCoroutine("Diseption"); // �ٷ� �����ϸ� ȸ���ϱ� ������ ������ ������ ������ �ڷ�ƾ���� ��ü�� �ش�
                StartCoroutine("QcoolTime");

            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            behavior.speed = 2; // ���ǵ� �ǵ����ֱ�

            // ���콺�� ���� ��ġ�� ��ǥ ���� �����´�
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

                // ���ӿ�����Ʈ�� �ڽ�ȭ
                skillw1.transform.SetParent(this.transform, false);
                skillw2.transform.SetParent(this.transform, false);
                skillw3.transform.SetParent(this.transform, false);

                skillw1.transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z + 1);
                skillw2.transform.position = new Vector3(transform.position.x + 0.6f, transform.position.y + 1, transform.position.z - 0.3f);
                skillw3.transform.position = new Vector3(transform.position.x - 0.6f, transform.position.y + 1, transform.position.z - 0.3f);*/


                if (deg < 360)
                {
                    // y���� �������� 120���� ȸ���Ѵ�
                    for (int i = 0; i < skillwSize; i++)
                    {
                        // �������� ����� ���ӿ�����Ʈ
                        GameObject[] skillw = new GameObject[3];
                        skillw[i] = Instantiate(skillWs[i]);

                        // �÷��̾��� �ڽ�ȭ�� �����ش�
                        skillw[i].transform.SetParent(this.transform, false);

                        // 120�� ������ 3���� �� ������� ��ġ��Ų��(����� ���� �� ���� ������Ѵ�)
                        var rad = Mathf.Deg2Rad * (deg + (i * (360 / skillwSize)));
                        var x = circleR * Mathf.Sin(rad);
                        var y = circleR * Mathf.Cos(rad);
                        skillw[i].transform.position = transform.position + new Vector3(x, 1, y); // �� �κ��� Ű����Ʈ
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
            if (Input.GetKeyDown(KeyCode.E)) // �Ƹ� E, ������ �Ҷ� ������ ��ġ�� ���� �ִ´�
            {
                isE = false;
          
                behavior.speed = 0; // ���ǵ�� 0

                // ���� ��ǥ�� �ϴù��⿡ ũ�Ⱑ 1�� ���� ���Ϳ� ������ ���´�
                Plane GroupPlane = new Plane(Vector3.up, Vector3.zero);

                float rayLength;

                // ���̰� ���� �����ߴ��� ���� üũ
                if (GroupPlane.Raycast(ray, out rayLength))
                {
                    // rayLength�Ÿ� �� ��ġ�� ��ȯ
                    Vector3 pointTolook = ray.GetPoint(rayLength);

                    // pointToLook ��ġ ���� ĳ���Ͱ� �ٶ󺸵��� �Ѵ�
                    transform.LookAt(new Vector3(pointTolook.x, transform.position.y, pointTolook.z));

                }
                StartCoroutine("Fascination"); // �ٷ� �����ϸ� ȸ���ϱ� ������ ������ ������ ������ �ڷ�ƾ���� ��ü�� �ش�
                StartCoroutine("EcoolTime"); // ��ų ��Ÿ��
            }
        }

        if (isR)
        {
            if (Input.GetKeyDown(KeyCode.R)) // ȥ������
            {
               
                RaycastHit hit;
                // ���콺�� ���� ��ġ�� ��ǥ ���� �����´�
                if (Physics.Raycast(ray, out hit, 1000f))
                {
                    // �÷��̾�� ���� ��ġ�� �Ÿ��� ���Ѵ�
                    float dist = Vector3.Distance(transform.position, hit.point);
                    // �÷��̾�� �̴Ͼ��� ��ġ�� �Ÿ��� ���Ѵ�
                    float dis = Vector3.Distance(transform.position, target.transform.position);
                    if(dist <= 3f)
                    {
                        behavior.speed = 10; // ���ǵ带 �÷��� ������ �����Ѵ�
                        targetPos = hit.point;

                        rCount++; // ���� ������ �ѹ��� ī��Ʈ �߰�
                        if (rCount == 3) // ���� ������ false
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

        // ĳ���Ͱ� �����δٸ�
        if (behavior.Run(targetPos))
        {
            behavior.Turn(targetPos); // ȸ���� �����ش�
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
