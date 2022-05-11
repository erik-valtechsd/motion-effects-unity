using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Firebase;
using Firebase.Database;
using Firebase.Extensions; // for ContinueWithOnMainThread
using extOSC.Examples;

public class RTdatabase : MonoBehaviour
{
    [SerializeField]
    public GameObject vfxInstance;

    [SerializeField]
    public GameObject displayManagerObj;

    DisplayManager displayManager;
    OscController oscCtrl;
    public float x, y, z = 0;
    private string addressX = "/rotation/x/";
    private string addressY = "/rotation/y/";
    private string addressZ = "/rotation/z/";

    private FirebaseDatabase db;
    // Start is called before the first frame update

    void Awake()
    {
        oscCtrl = vfxInstance.GetComponent<OscController>();
        displayManager = displayManagerObj.GetComponent<DisplayManager>();

    }
    void Start()
    {

        db = FirebaseDatabase.GetInstance("https://remote-immersive-default-rtdb.firebaseio.com");
        // DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
        GetRemoteValues(addressX);
        GetRemoteValues(addressY);
        db.GetReference(addressX).ValueChanged += HandleValueChanged;
        db.GetReference(addressY).ValueChanged += HandleValueChanged;

    }

    void GetRemoteValues(String address)
    {
        db.GetReference(address)
          .GetValueAsync().ContinueWithOnMainThread(task =>
          {
              if (task.IsFaulted)
              {
                  Debug.Log("error getting values...");
              }
              else if (task.IsCompleted)
              {
                  DataSnapshot snapshot = task.Result;
                //   Debug.Log(snapshot.GetRawJsonValue());

              }
          });
    }

    void HandleValueChanged(object sender, ValueChangedEventArgs args)
    {
        String key = (args.Snapshot.Key).ToString();
        // Single.TryParse(args.Snapshot.Value);
        float val = float.Parse(args.Snapshot.Value.ToString());
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }
        // Debug.Log(args.Snapshot);
        if (key == "x")
        {
            oscCtrl.SetX(val);
        }
        else if (key == "y")
        {
            oscCtrl.SetY(val);
        }
        else if (key == "z")
        {
            oscCtrl.SetZ(val);
        }
    // turn automode off on displayManager script object
        displayManager.ResetIdleMode();
    }


    // Update is called once per frame
    void Update()
    {

    }
}
