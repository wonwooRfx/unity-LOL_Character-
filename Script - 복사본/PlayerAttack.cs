using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject bullet; // �⺻ ����
    public GameObject skillQ; // Q��ų �Ѿ�
    public GameObject skillR; // R��ų �Ѿ�
    public GameObject skillW; // W��ų �Ѿ�
    public Transform firePos; // �Ѿ��� ���� ��ġ
    public GameObject eEffect; // e��ų�� �� �� ���� effect
    public GameObject rEffect; // R��ų �� �� ���� effect

    Vector3 targetPos; // ĳ������ �̵� Ÿ�� ��ġ

    Behavior behavior; // ĳ���� ������ ��ũ��Ʈ

    public bool isQ, isW, isE, isR = true; // ť�߻� ����
    public bool isAttack; // �⺻ ���� �ݺ��� ���� ��

    public GameObject target; // �̴Ͼ�
    private void Awake()
    {
        isAttack = false; // ��ų���� false�� ����� �Ѵ�
    }
    void Start()
    {
        behavior = GetComponent<Behavior>();

        InvokeRepeating("NormalAttack", 0.01f, 1f); // �κ�ũ�� �̿��Ͽ� 1f���� �ݺ�

    }
    void Update()
    {
        // ���콺�� ��ġ�� ��ȯ
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (isQ) // ���� ť�� true��
        {
            if (Input.GetKeyDown(KeyCode.Q)) // ����� Q, ������ �Ҷ� ������ ��ġ�� ���� �ִ´�
            {
                behavior.ani.SetTrigger("doQ"); // ���� �ִϸ��̼�
                
                isQ = false;
                isAttack = false;

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
                StartCoroutine("MagicOneHit"); // �ٷ� �����ϸ� ȸ���ϱ� ������ ������ ������ ������ �ڷ�ƾ���� ��ü�� �ش�
                StartCoroutine("QcoolTime"); // ��ų ��Ÿ��
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
                
                if (hit.transform.name == "Minion" ) // �̴Ͼ��� Ŭ���ϸ鼭 �Ÿ��� 5f �����ϸ� ���߰� �⺻����
                {
                    isAttack = true;
                    behavior.speed = 0;
                    transform.LookAt(hit.transform); // ���� �̴Ͼ��� �ٶ󺻴�

                }
                else // �ƴ϶�� �����δ�
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
                StartCoroutine("SkillW");
                StartCoroutine("WcoolTime");
            }
        }


        if (isE)
        {
            if (Input.GetKeyDown(KeyCode.E)) // ���� �̵� ����
            {
               
                isAttack = false;
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    // �÷��̾�� ���� ��ġ�� �Ÿ��� ���Ѵ�
                    float dist = Vector3.Distance(transform.position, hit.point);
                    // �÷��̾�� �̴Ͼ��� �Ÿ��� ���Ѵ�
                    float attDist = Vector3.Distance(transform.position, target.transform.position);

                    // �� �Ÿ��� ���̰� 3f���� �۴ٸ� ���� ��ġ�� �����̵� �Ѵ�
                    if (dist <= 5f)
                    {
                        isE = false; // ���� �־�� �Ÿ��� �ֶ� ������ �ߵ� ���Ѵ�
                        behavior.ani.SetTrigger("doE");
                        behavior.speed = 0;
                        transform.position = hit.point;

                        if(attDist <= 15f) 
                        {
                            Instantiate(bullet, firePos.position, firePos.rotation);
                        }

                        StartCoroutine("Eeffect"); // �̵� �� �� ����Ʈ�� ������ ����
                    }

                }
                StartCoroutine("EcoolTime"); // ��ų ��Ÿ��
            }
        }

        if (isR) // Q��ų�� �Ȱ��ٰ� ���� �ȴ�
        {
            if (Input.GetKeyDown(KeyCode.R)) // ������ �ϰ� ����
            {
                behavior.ani.SetTrigger("doR");

                isR = false;
                isAttack = false;

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

                    Instantiate(rEffect, transform.position, rEffect.transform.rotation);
                    StartCoroutine("TargetOneHit"); // ���ݿ� �ɸ��� ��� �ð�
                }
                
                StartCoroutine("RcoolTime");
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

    void NormalAttack()
    {
        if (isAttack == true)
        {
            behavior.ani.SetTrigger("doAttack");
            Instantiate(bullet, firePos.position, firePos.rotation); // �Ѿ� ����
        }
    }

    IEnumerator MagicOneHit() // Q��ų
    {
        yield return new WaitForSeconds(0.01f);
        Instantiate(skillQ, firePos.position, firePos.rotation);
    }

    IEnumerator QcoolTime() // 3�� �ڿ� ��Ÿ���� �����ش�
    {
        yield return new WaitForSeconds(3f);
        isQ = true;
    }

    IEnumerator SkillW() // w��ų
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
