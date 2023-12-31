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
        // 이동하고자하는 좌표 값과 현재 내 위치의 차이를 구한다
        float dis = Vector3.Distance(transform.position, targetPos);

        if (dis >= 0.01f) // 차이가 아직 있다면
        {
            // 캐릭터를 이동시킨다
            transform.localPosition = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            ani.SetFloat("doRun", speed);

            return true;
        }
        return false;
    }

    public void Turn(Vector3 targetPos)
    {
        // 캐릭터를 이동하고자 하는 좌표값 방향으로 회전시킨다
        Vector3 dir = targetPos - transform.position;
        Vector3 dirXZ = new Vector3(dir.x, 0f, dir.z);
        Quaternion targetRot = Quaternion.LookRotation(dirXZ);
        rid.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, 10000f * Time.deltaTime);
    }
}
