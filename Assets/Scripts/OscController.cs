/* Copyright (c) 2021 dr. ext (Vladimir Sigalkin) */

using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEditor.VFX;
using UnityEditor.VFX.UI;


namespace extOSC.Examples
{
	public class OscController : MonoBehaviour
	{
		 float x,y,z = 0;
		 float accel = 0;
        GameObject controlledObject;
            float smooth = 5.0f;


    Gradient gradient;
    GradientColorKey[] colorKey;
    GradientAlphaKey[] alphaKey;

	//rigidbody for physics based motion of gameobject
    // Rigidbody m_Rigidbody;
	[SerializeField]
    [Range(1,50)]
    float m_Speed;

	[SerializeField]
	GameObject dummyRotation;

    private VisualEffect visualEffect;
    float exposedParameter;

		#region Private Vars


		private OSCReceiver _receiver;

		private const string _address = "/*"; // Also, you cam use mask in address: /example/*/

		#endregion

		#region Unity Methods

		protected virtual void Start()
		{
            controlledObject = gameObject;
	

			// Creating a receiver.
			_receiver = gameObject.AddComponent<OSCReceiver>();

			// Set local port.
			_receiver.LocalPort = 7001;

			// Bind "MessageReceived" method to special address.
			_receiver.Bind(_address, MessageReceived);

			visualEffect = this.transform.GetComponent<UnityEngine.VFX.VisualEffect>();

		//setup gradient
        gradient = new Gradient();

        // Populate the color keys at the relative time 0 and 1 (0 and 100%)
        colorKey = new GradientColorKey[2];
        colorKey[0].color = Color.red;
        colorKey[0].time = 0.0f;
        colorKey[1].color = Color.blue;
        colorKey[1].time = 1.0f;

        // Populate the alpha  keys at relative time 0 and 1  (0 and 100%)
        alphaKey = new GradientAlphaKey[2];
        alphaKey[0].alpha = 1.0f;
        alphaKey[0].time = 0.0f;
        alphaKey[1].alpha = 0.0f;
        alphaKey[1].time = 1.0f;

        gradient.SetKeys(colorKey, alphaKey);
		visualEffect.SetGradient("gradient1", gradient);

		//setup rigidbody
        // m_Rigidbody = transform.GetComponent<Rigidbody>();


		}

		protected virtual void Update()
		{
			// Remap(x,)
					SetGradientColor(x,y,z);
		visualEffect.SetVector3("position", dummyRotation.transform.forward * m_Speed);

		}

		#endregion

		#region Protected Methods

		protected void MessageReceived(OSCMessage message)
		{
			
			if(message.Address == "/gyrosc/gyro"){
             x = message.Values[0].FloatValue * Mathf.Rad2Deg;
             y = message.Values[1].FloatValue * Mathf.Rad2Deg;
             z = message.Values[2].FloatValue * Mathf.Rad2Deg;
                    Quaternion target = Quaternion.Euler(x, y, z);
					Quaternion direction = Quaternion.Slerp(transform.rotation, target,  Time.deltaTime * smooth);
                    dummyRotation.transform.rotation = target; // set dummy rotation to keep track of forward direction using vector3.forward
					visualEffect.SetVector3("rotation", new Vector3(x,y,z));
					}else if(message.Address == "/gyrosc/accel"){
						accel = message.Values[0].FloatValue;
					}
		}

		protected void SetGradientColor(float r, float g, float b){
		Color c = new Color(ExtensionMethods.Remap(r, -90, 90, 0,255),ExtensionMethods.Remap(g, -90, 90, 0,255),ExtensionMethods.Remap(b, -90, 90, 0,255));
		colorKey[0].color = c;
		alphaKey[0].alpha = ExtensionMethods.Remap(accel, 0, 3, (float)0.2,1);
		gradient.SetKeys(colorKey, alphaKey);
		visualEffect.SetGradient("gradient1", gradient);
		visualEffect.SetVector3("particleColor", new Vector3(c.r,c.g,c.b));
		// visualEffect.SetFloat("size", ExtensionMethods.Remap(accel, 0, 1, (float)0.2,1));
		// visualEffect.SetVector3("getRotation", transform.forward * m_Speed;);
	}


		#endregion
	}
public static class ExtensionMethods {
 
public static float Remap (this float value, float from1, float to1, float from2, float to2) {
    return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
}
   
}

}

/*
/* Copyright (c) 2021 dr. ext (Vladimir Sigalkin) */




// using UnityEngine;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.VFX;
// using UnityEditor.VFX;
// using UnityEditor.VFX.UI;
// using System.Linq;


// namespace extOSC.Examples
// {
//     public class OscController : MonoBehaviour
//     {
//         float x, y, z = 0;
//         float accel = 0;
//         GameObject controlledObject;
//         float smooth = 5.0f;


//         Gradient gradient;
//         GradientColorKey[] colorKey;
//         GradientAlphaKey[] alphaKey;

//         //rigidbody for physics based motion of gameobject
//         // Rigidbody m_Rigidbody;
//         [SerializeField]
//         [Range(1, 50)]
//         float m_Speed;

//         [SerializeField]
//         GameObject dummyRotation;

//         private VisualEffect visualEffect;
//         float exposedParameter;

//         // get average movement usign RMS

//         float rms = 0;
//         int numCounted = 0;
//         Queue<int> samples = new Queue<int>();
//         int qSamples = 1024;  // array size
//         int active = 0;

//         #region Private Vars


//         private OSCReceiver _receiver;

//         private const string _address = "/*"; // Also, you cam use mask in address: /example/*/

//         #endregion

//         #region Unity Methods

//         protected virtual void Start()
//         {
//             controlledObject = gameObject;


//             // Creating a receiver.
//             _receiver = gameObject.AddComponent<OSCReceiver>();

//             // Set local port.
//             _receiver.LocalPort = 7001;

//             // Bind "MessageReceived" method to special address.
//             _receiver.Bind(_address, MessageReceived);

//             visualEffect = this.transform.GetComponent<UnityEngine.VFX.VisualEffect>();

//             //setup gradient
//             gradient = new Gradient();

//             // Populate the color keys at the relative time 0 and 1 (0 and 100%)
//             colorKey = new GradientColorKey[2];
//             colorKey[0].color = Color.red;
//             colorKey[0].time = 0.0f;
//             colorKey[1].color = Color.blue;
//             colorKey[1].time = 1.0f;

//             // Populate the alpha  keys at relative time 0 and 1  (0 and 100%)
//             alphaKey = new GradientAlphaKey[2];
//             alphaKey[0].alpha = 1.0f;
//             alphaKey[0].time = 0.0f;
//             alphaKey[1].alpha = 0.0f;
//             alphaKey[1].time = 1.0f;

//             gradient.SetKeys(colorKey, alphaKey);
//             visualEffect.SetGradient("gradient1", gradient);

//             //setup rigidbody
//             // m_Rigidbody = transform.GetComponent<Rigidbody>();


//         }

//         protected virtual void Update()
//         {
//             // Remap(x,)
//             SetGradientColor(x, y, z);
//             visualEffect.SetVector3("position", dummyRotation.transform.forward * m_Speed);
//             // samples.Enqueue(active);
// 			// int buffersize=500;
//             // if (samples.Count > buffersize)
//             // {
//             //     samples.Dequeue();
// 			// 	for (int i = 0; i < samples.Count; i++){
//             //     rms += samples.ElementAt(i);
//             //     rms /= (float)buffersize;
//             //     rms = Mathf.Sqrt(rms);
// 			// 	}
// 			// 	// Debug.Log(rms);
//             // }

//             // active = 0;
//         }



//         #endregion

//         #region Protected Methods

//         protected void MessageReceived(OSCMessage message)
//         {

//             if (message.Address == "/gyrosc/gyro")
//             {
//                 x = message.Values[0].FloatValue * Mathf.Rad2Deg;
//                 y = message.Values[1].FloatValue * Mathf.Rad2Deg;
//                 z = message.Values[2].FloatValue * Mathf.Rad2Deg;
//                 Quaternion target = Quaternion.Euler(x, y, z);
//                 Quaternion direction = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
//                 dummyRotation.transform.rotation = target; // set dummy rotation to keep track of forward direction using vector3.forward
//                 visualEffect.SetVector3("rotation", new Vector3(x, y, z));
//             }
//             else if (message.Address == "/gyrosc/accel")
//             {
//                 accel = message.Values[0].FloatValue;
//             }
//             active = 1;
//         }

//         protected void SetGradientColor(float r, float g, float b)
//         {
//             Color c = new Color(ExtensionMethods.Remap(r, -90, 90, 0, 255), ExtensionMethods.Remap(g, -90, 90, 0, 255), ExtensionMethods.Remap(b, -90, 90, 0, 255));
//             colorKey[0].color = c;
//             alphaKey[0].alpha = ExtensionMethods.Remap(accel, 0, 3, (float)0.2, 1);
//             gradient.SetKeys(colorKey, alphaKey);
//             visualEffect.SetGradient("gradient1", gradient);
//             visualEffect.SetVector3("particleColor", new Vector3(c.r, c.g, c.b));
//             // visualEffect.SetFloat("size", ExtensionMethods.Remap(accel, 0, 1, (float)0.2,1));
//             // visualEffect.SetVector3("getRotation", transform.forward * m_Speed;);
//         }


//         #endregion
//     }
//     public static class ExtensionMethods
//     {

//         public static float Remap(this float value, float from1, float to1, float from2, float to2)
//         {
//             return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
//         }

//     }

// }

