using UnityEngine;
//Updates the primary and seconday colours for each material
[ExecuteInEditMode]
public class ChangeColours : MonoBehaviour
{
    public Color c,d;
    public Material m, n;

    // Update is called once per frame
    void Update()
    {
        m.color = c;
        n.color = d;
    }
}
