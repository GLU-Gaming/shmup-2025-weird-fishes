using System.Collections.Generic;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class ParallaxLayer
{
    public Transform layerTransform;
    public float speedMultiplier;
}

public class ParallaxScroller : MonoBehaviour
{


    //public ParallaxLayer[] layers;
    public List<ParallaxLayer> layerList = new List<ParallaxLayer>();

    public float baseSpeed = 2f;
    public float resetPosition = 20f;



    void Update()
    {
        foreach (ParallaxLayer layer in layerList)
        {
            // Beweeg de laag naar links
            layer.layerTransform.position += Vector3.left * baseSpeed * layer.speedMultiplier * Time.deltaTime;
        }
        //        //Het eerste element uit de array in de gaten houden (layers[0]) als die onder de resetPosition komt
        //        //Plaats die dan op dezelfde plek als het laatste element  Vector3 topRight = layerList[layerList.Count - 1].layerTransform.GetComponent<SpriteRenderer>().bounds.max;

        if (layerList[0].layerTransform.localPosition.x <= resetPosition)
        {
            Debug.Log(layerList[0].layerTransform.localPosition.x);
            ParallaxLayer firstLayer = layerList[0];
            Debug.Log("Moving: " + firstLayer.layerTransform.name);


            ParallaxLayer lastLayer = layerList[layerList.Count - 1];
            Vector3 topRight = lastLayer.layerTransform.GetComponent<SpriteRenderer>().bounds.max;
            topRight.z = transform.position.z;

            firstLayer.layerTransform.position = topRight;

            layerList.Remove(firstLayer);
            layerList.Add(firstLayer);
        }
    }


    //private void OnDrawGizmosSelected()
    //{
    //    //layers[0]
    //    Vector3 topRight = layerList[layerList.Count - 1].layerTransform.GetComponent<SpriteRenderer>().bounds.max;
    //    Gizmos.DrawSphere(topRight, 4);
    //}
}



