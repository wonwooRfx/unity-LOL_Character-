using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behavior : MonoBehaviour
{
    public float speed = 2f;
    Rigidbody rid;
    public Animator ani;
  
    // Start is called before the first frame update
    void Start()
    {
        rid = GetComponent<Rigidbody>();
        ani = GetComponent<Animator>();
        
    }


    public bool Run(Vector3 targetPos)
    {
        // �̵��ϰ����ϴ� ��ǥ ���� ���� �� ��ġ�� ���̸� ���Ѵ�
        float dis = Vector3.Distance(transform.position, targetPos);

        if (dis >= 0.01f) // ���̰� ���� �ִٸ�
        {
            // ĳ���͸� �̵���Ų��
            transform.localPosition = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            ani.SetFloat("doRun", speed);

            return true;
        }
        return false;
    }

    public void Turn(Vector3 targetPos)
    {
        // ĳ���͸� �̵��ϰ��� �ϴ� ��ǥ�� �������� ȸ����Ų��
        Vector3 dir = targetPos - transform.position;
        Vector3 dirXZ = new Vector3(dir.x, 0f, dir.z);
        Quaternion targetRot = Quaternion.LookRotation(dirXZ);
        rid.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, 10000f * Time.deltaTime);
    }
}
