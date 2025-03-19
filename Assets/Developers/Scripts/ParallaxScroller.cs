using UnityEngine;

public class ParallaxScroller : MonoBehaviour
{
    [System.Serializable]
    public class ParallaxLayer
    {
        public Transform layerTransform; // The background object
        public float speedMultiplier = 1f; // The speed of this layer
    }

    public ParallaxLayer[] layers; // Array of background layers
    public float baseScrollSpeed = 2f; // Base speed of the parallax scroll
    public float resetPosition = 20f; // Reset position to loop backgrounds

    void Update()
    {
        foreach (var layer in layers)
        {
            // Scroll each layer to the left, scaled by its speedMultiplier
            layer.layerTransform.position += Vector3.left * (baseScrollSpeed * layer.speedMultiplier) * Time.deltaTime;

            // Reset the background position when it's out of view
            if (layer.layerTransform.position.x <= -resetPosition)
            {
                layer.layerTransform.position += new Vector3(resetPosition * 2, 0, 0);
            }
        }
    }
}
