//using System.Collections.Generic;
//using UnityEngine;

//[System.Serializable]
//public class ParallaxLayer
//{
//    public Transform layerTransform;
//    public float speedMultiplier;
//}

//public class ParallaxScroller : MonoBehaviour
//{
//    public List<ParallaxLayer> layerList = new List<ParallaxLayer>();
//    public float baseSpeed = 2f;
//    public float resetPosition = -20f; // Links uit beeld

//    void Update()
//    {
//        // Beweeg alle lagen naar links
//        foreach (ParallaxLayer layer in layerList)
//        {
//            layer.layerTransform.position += Vector3.left * baseSpeed * layer.speedMultiplier * Time.deltaTime;
//        }

//        // Controleer of de eerste laag uit beeld is
//        if (layerList[0].layerTransform.position.x <= resetPosition)
//        {
//            // Sla de eerste laag op
//            ParallaxLayer firstLayer = layerList[0];

//            // Bepaal de rechterkant van de laatste laag
//            Vector3 topRight = layerList[layerList.Count - 1].layerTransform.GetComponent<SpriteRenderer>().bounds.max;

//            // Verwijder de eerste laag uit de lijst
//            layerList.RemoveAt(0);

//            // Zet de verwijderde laag achteraan
//            layerList.Add(firstLayer);

//            // Verplaats de laag naar de exacte rechterkant van de laatste laag
//            float layerWidth = firstLayer.layerTransform.GetComponent<SpriteRenderer>().bounds.size.x;
//            firstLayer.layerTransform.position = new Vector3(topRight.x, firstLayer.layerTransform.position.y, firstLayer.layerTransform.position.z);
//        }
//    }

//    private void OnDrawGizmosSelected()
//    {
//        if (layerList.Count > 0)
//        {
//            Vector3 topRight = layerList[layerList.Count - 1].layerTransform.GetComponent<SpriteRenderer>().bounds.max;
//            Gizmos.DrawSphere(topRight, 0.5f);
//        }
//    }
//}
