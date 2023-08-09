using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingManager : MonoBehaviour
{
    // 생성된노트를 담는 List -> 판정 범위 내에 있는지 모든 노트를 비교해야 함
    public List<GameObject> boxNoteList = new List<GameObject>();

    [SerializeField] Transform Center = null;
    [SerializeField] RectTransform[] timingRect = null; // Perfect, Good, Cool, Bad
    Vector2[] timingBoxs = null;
    // Start is called before the first frame update
    void Start()
    {
        timingBoxs = new Vector2[timingRect.Length];

        for(int i = 0; i < timingRect.Length; i++) {
            timingBoxs[i].Set(Center.localPosition.y - timingRect[i].rect.height / 2, 
                            Center.localPosition.y + timingRect[i].rect.height / 2);
        }
    }

    public void CheckTiming() {
        for(int i = 0; i < boxNoteList.Count; i++) {
            float t_notePosY = boxNoteList[i].transform.localPosition.y;

            for(int x = 0 ; x < timingBoxs.Length; x++) {
                if(timingBoxs[x].x <= t_notePosY && t_notePosY <= timingBoxs[x].y) {
                    boxNoteList[i].GetComponent<Note>().HideNote();
                    boxNoteList.RemoveAt(i);
                    switch(x) {
                        case 0 :
                            Debug.Log("Perfect");
                            break;
                        case 1:
                            Debug.Log("Good");
                            break;
                        case 2:
                            Debug.Log("Cool");
                            break;
                        case 3:
                            Debug.Log("Bad");
                            break;
                    }
                    // Debug.Log("Hit" + x);
                    return;
                }
            }
        }
        Debug.Log("Miss");
    }
}
