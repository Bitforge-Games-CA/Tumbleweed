using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tumbleweed.Core.Managers
{

    public class Building : MonoBehaviour
    {
        public bool Placed { get; set; }
        public BoundsInt BuildingSize;
        public Vector3 BuildingWorldPosition { get; set; }
    }

}