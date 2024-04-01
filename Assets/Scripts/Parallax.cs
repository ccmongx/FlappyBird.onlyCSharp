using UnityEngine;

public class Parallax : MonoBehaviour
{
    [Range(0.0f, 1.0f)]
    public float textureSpeed;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Renderer renderer = GetComponent<Renderer>();
        Vector2 currentOffset = renderer.material.GetTextureOffset("_MainTex");
        renderer.material.SetTextureOffset("_MainTex", currentOffset + new Vector2(textureSpeed, 0) * Time.deltaTime);
    }
}
