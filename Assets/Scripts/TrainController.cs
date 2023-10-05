using UnityEngine;
using UnityEngine.Serialization;

public class TrainController : MonoBehaviour
{
    [SerializeField, Min(0.0001f)] private float trainAcceleration = 1f;
    [HideInInspector] public float trainVelocity;
    
    private void Update()
    {
        int vertical = (int) Input.GetAxisRaw("Vertical");
        trainVelocity += vertical > 0 ? trainAcceleration * Time.deltaTime :
            vertical < 0 ? -trainAcceleration * Time.deltaTime * 2 : 0;
        trainVelocity = trainVelocity > 0 ? trainVelocity : 0;
        
        transform.Translate(0, 0, trainVelocity * Time.deltaTime);
    }
}
