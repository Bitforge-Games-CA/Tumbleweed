using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tumbleweed.Core.Player
{

    public class MainCamera : MonoBehaviour
    {

        public int SpeedModifier = 5;
        public int Speed = 1;

        // Start is called before the first frame update
        void Start()
        {
            Camera.main.orthographicSize = 7.5f;
        }

        // Update is called once per frame
        void Update()
        {
            // set local variables
            float xAxisValue = Input.GetAxis("Horizontal") / SpeedModifier;
            float yAxisValue = Input.GetAxis("Vertical") / SpeedModifier;

            // if the current camera isn't null do a transform or two
            if (Camera.main != null)
            {
                // WASD control
                Camera.main.transform.Translate(new Vector3(xAxisValue, yAxisValue, 0.0f));

                // zoom control
                if (Input.mouseScrollDelta.y > 0 && Camera.main.orthographicSize < 15.5f)
                {
                    Camera.main.orthographicSize += Speed;
                }
                if (Input.mouseScrollDelta.y < 0 && Camera.main.orthographicSize > 2.5f)
                {
                    Camera.main.orthographicSize -= Speed;
                }
            }

        }

    }

}