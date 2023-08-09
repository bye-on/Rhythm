using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    public int bpm = 0; // beat per minute
    double currentTime = 0d; // 더 높은 정확성을 위해 double
    [SerializeField] Transform tfNoteAppear = null; // 노트가 생성될 위치

    TimingManager theTimingManager;
    
    void Start() {
        theTimingManager = GetComponent<TimingManager>();
    }
    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        if(currentTime >= 60d/bpm) { // 1 beat per Appear Time
            GameObject t_note = ObjectPool.instance.noteQueue.Dequeue();
            t_note.transform.position = tfNoteAppear.position;
            t_note.SetActive(true);
            theTimingManager.boxNoteList.Add(t_note);
            currentTime -= 60d/bpm; // 오차 손실 때문
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag("Note")){
            theTimingManager.boxNoteList.Remove(collision.gameObject);
            
            ObjectPool.instance.noteQueue.Enqueue(collision.gameObject);
            collision.gameObject.SetActive(false);
        }
    }
}
