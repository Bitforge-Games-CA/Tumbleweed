using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tumbleweed.Core.Utilities
{

    public class Tools : MonoBehaviour
    {
        public static Tools current;

        public void Start()
        {
            // instatiate the singleton
            if (current == null)
            {
                current = this;
            }
            else if (current != this)
            {
                Destroy(current);
            }
        }


        // 3D and Rendering
    
        public static Transform CreatePrimitiveRed(Vector3 pos, string name = "sphere1", PrimitiveType type = PrimitiveType.Sphere, float size = 0.05f)
        {
            var primitive = GameObject.CreatePrimitive(type).transform;
            primitive.position = pos;
            primitive.localScale = Vector3.one * size;
            primitive.name = name;

            var renderer = primitive.GetComponent<MeshRenderer>();
            renderer.material.color = Color.red;
            renderer.sortingOrder = 2;
            renderer.material.enableInstancing = true;

            return primitive;
        }

        public static Transform CreatePrimitiveGreen(Vector3 pos, string name = "sphere1", PrimitiveType type = PrimitiveType.Sphere, float size = 0.05f)
        {
            var primitive = GameObject.CreatePrimitive(type).transform;
            primitive.position = pos;
            primitive.localScale = Vector3.one * size;
            primitive.name = name;

            var renderer = primitive.GetComponent<MeshRenderer>();
            renderer.material.color = Color.green;
            renderer.sortingOrder = 2;
            renderer.material.enableInstancing = true;

            return primitive;
        }



        // Maths and Math functions

        public Vector2 TranslateToIso(float x, float y)
        {
            float newX = x - y;
            float newY = (x + y) * .577f;

            return new Vector2(newX * 0.5f, newY * 0.5f);
        }

        public Vector2 TranslateFromIso(float x, float y)
        {
            float cartesianX = (2.0f * y + x) * 0.5f;
            float cartesianY = (2.0f * y - x) * 0.5f;
            return new Vector2(cartesianX, cartesianY);
        }



        // More functions





    }

}
