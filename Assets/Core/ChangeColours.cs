using UnityEngine;
[ExecuteInEditMode]
public class ChangeColours : MonoBehaviour
{
    public Color c,d;
    Color primary, secondary;
    public Material m, n;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m.color = c;
        n.color = d;
    }
}
