using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Firebase; 
using Firebase.Extensions;

public class FirebaseInit : MonoBehaviour
{
    public UnityEvent OnFirebaseInitialized = new UnityEvent();

    // Start is called before the first frame update
    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
            if(task.Exception != null){
                Debug.Log($"Failed to init firebase with {task.Exception}");
                return;
            }
            OnFirebaseInitialized.Invoke();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
