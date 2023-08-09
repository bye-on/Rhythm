using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectInfo {
    public GameObject goPrefab;
    public int count; 
    public Transform tfPoolParent; // 어느 부모 자식 위치에 생성할 지. 위치 정보
}

public class ObjectPool : MonoBehaviour
{
    
    [SerializeField] ObjectInfo[] objectInfo = null;
    // 오브젝트 풀을 언제 어디서든 쉽게 가져다쓰고 반납할 수 있어야 함.
    // 따라서 공유자원으로 만들어줌.
    public static ObjectPool instance;
    // Queue: 선입선출 자료형
    public Queue<GameObject> noteQueue = new Queue<GameObject>();
    
    void Start()
    {
        instance = this;
        noteQueue = InsertQueue(objectInfo[0]);
    }
    
    Queue<GameObject> InsertQueue(ObjectInfo p_objedctInfo) {
        Queue<GameObject> t_queue = new Queue<GameObject>();
        for(int i=0; i < p_objedctInfo.count; i++) {
            GameObject t_clone = Instantiate(p_objedctInfo.goPrefab, transform.position, Quaternion.identity);
            t_clone.SetActive(false);
            if(p_objedctInfo.tfPoolParent != null)
                t_clone.transform.SetParent(p_objedctInfo.tfPoolParent);
            else
                t_clone.transform.SetParent(this.transform);

            t_queue.Enqueue(t_clone);
        }
        return t_queue;
    }
}
