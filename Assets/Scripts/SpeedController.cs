using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(TrainController))]
public class SpeedController : MonoBehaviour
{
    [SerializeField, Min(0.0001f)] private float maxSpeed = 100f;
    [SerializeField, Min(0.0001f)] private float minSpeed = 20f;
    
    private TrainController _trainController;

    private void Start()
    {
        _trainController = GetComponent<TrainController>();
    }

    private float GetMaxSpeed(float x)
    {
        return Mathf.PerlinNoise1D(x  * .001f) * Mathf.PerlinNoise1D(x  * .001f) * (maxSpeed - minSpeed) + minSpeed;
    }

    private void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Update()
    {
        float trainVelocity = _trainController.trainVelocity;
        float zPosition = transform.position.z;
        float trainVelocityKpH = trainVelocity * 3.6f;
        Debug.Log((int) zPosition + " > " + (int) trainVelocityKpH + " : " + (int) GetMaxSpeed(zPosition));
        
        if ((int) trainVelocityKpH > (int) GetMaxSpeed(zPosition))
            Die();
        
    }
}
