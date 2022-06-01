

// Your web app's Firebase configuration
var firebaseConfig = {
  apiKey: "AIzaSyDuBGf-NbDuw-r_JWupZUGum853p8dAoqg",
  authDomain: "remote-immersive.firebaseapp.com",
  databaseURL: "https://remote-immersive-default-rtdb.firebaseio.com",
  projectId: "remote-immersive",
  storageBucket: "gs://remote-immersive.appspot.com",
  // messagingSenderId: "1069402189378",
  appId: "1:534660787892:ios:c1a0815bbfa9b9a7fbb1e1",
  measurementId: "G-8JB2KE736F",
};
// Initialize Firebase
firebase.initializeApp(firebaseConfig);
firebase.analytics();

//add listeners for device motion and device orientation to change queued planet

// if (typeof DeviceMotionEvent.requestPermission === "function") {
//   DeviceMotionEvent.requestPermission()
//     .then((permissionState) => {
//       if (permissionState === "granted") {
//         window.addEventListener("devicemotion", handleMotionEvent, {
//           passive: true,
//         });
//       }
//     })
//     .catch(console.error);
// } else {
//   window.addEventListener("devicemotion", handleMotionEvent, { passive: true });
// }

// if (typeof DeviceOrientationEvent.requestPermission === "function") {
//   DeviceOrientationEvent.requestPermission()
//     .then((permissionState) => {
//       if (permissionState === "granted") {
//         window.addEventListener("deviceorientation", handleOrientation, {
//           passive: true,
//         });
//       }
//     })
//     .catch(console.error);
// } else {
//   window.addEventListener("deviceorientation", handleOrientation, {
//     passive: true,
//   });
// }

const btn = document.getElementById("request");
btn.addEventListener("touchend", permission);

function permission() {
  console.log("clicked");
  btn.style.display = "none";
  DeviceOrientationEvent.requestPermission()
    .then((response) => {
      if (response == "granted") {
        console.log("permission granted");
        window.addEventListener(
          "deviceorientation",
          function (event) {
            var a = event.alpha;
            var b = event.beta;
            var g = event.gamma;
            firebase.database().ref(queryUIDString + '/rotation/x/').set(a);
            firebase.database().ref(queryUIDString + '/rotation/y/').set(b);
            firebase.database().ref(queryUIDString + '/rotation/z/').set(g);
          },
          false
        );
      }
    })
    .catch(console.error);
}


// map values to a given range
Number.prototype.mapRange = function (in_min, in_max, out_min, out_max) {
  return ((this - in_min) * (out_max - out_min)) / (in_max - in_min) + out_min;
};
