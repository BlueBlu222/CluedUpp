using UnityEngine;
//Gives an object a vibrate and spin effect
public class Wiggle : MonoBehaviour
{
    public float speed = 100;
    float rotationSpeed = 1;
    public Vector3 movement,rotation;
    Vector3 startPos;
    public bool flipText;
    public static bool canSpin = false;

    // Update is called once per frame
    void Update()
    {
     if(startPos == Vector3.zero)
        {
            RectTransform rt = GetComponent<RectTransform>();
            startPos = new Vector3(rt.position.x, rt.position.y, 0);
            if (movement == Vector3.zero)
            {
                movement = RandomV3;
            }
        }
        transform.position = startPos + (new Vector3(Mathf.Sin(movement.x * speed * Time.deltaTime), Mathf.Sin(movement.y * speed * Time.deltaTime), Mathf.Sin(movement.z * speed * Time.deltaTime)));
        
        if (flipText)
        {
            Vector3 v = transform.rotation.eulerAngles;
            rotationSpeed = OrginalRotation(v) ? 1 : canSpin ? 10 : 0;
            if (!canSpin)
            {
                if (rotationSpeed == 0)
                {
                    transform.rotation = Quaternion.Euler(Vector3.zero);
                } else
                {
                    Debug.Log(v);
                }
            }
        }
        transform.Rotate(rotation * rotationSpeed);
    }
    Vector3 RandomV3
    {
        get
        {
            return new Vector3(Random.Range(0f, speed), Random.Range(0f, speed), Random.Range(0f, speed));
        }
    }
    bool OrginalRotation(Vector3 v)
    {
        return v.x > 90 && v.x < 270 && v.y > 90 && v.y < 270 && v.z > 90 && v.z < 270;
    }
}
