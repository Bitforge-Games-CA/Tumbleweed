using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tumbleweed.Core.Managers
{

    public class Blueprint : MonoBehaviour
    {

        private void OnTriggerEnter2D(Collider2D collision)
        {
            BlueprintManager.current.CantBePlaced2 = true;
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            BlueprintManager.current.SR.color = new Vector4(1, 0, 0, 0.5f);
        }


        private void OnTriggerExit2D(Collider2D collision)
        {
            BlueprintManager.current.CantBePlaced2 = false;
        }

    }

}