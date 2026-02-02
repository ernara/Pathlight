using UnityEngine;

public class MapRandomizer : MonoBehaviour
{
    public Transform ground;
    public Vector2 scaleRange = new Vector2(20f, 60f);

    void Start()
    {
        if (ground == null)
        {
            GameObject g = GameObject.Find("Ground");
            if (g != null) ground = g.transform;
        }

        float scale = Random.Range(scaleRange.x, scaleRange.y);
        ground.localScale = new Vector3(scale, 1f, scale);
    }
}
