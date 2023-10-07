using UnityEngine;

public class TrainController : MonoBehaviour
{
    [Header("Rails:")]
    [SerializeField] private GameObject railsObj;
    
    [SerializeField, Min(0.0001f)] private int railsCount = 10;
    [SerializeField, Min(0.0001f)] private float railsScale = 2f;
    [SerializeField, Min(0.0001f)] private float railsOffset = 10f;
    
    [Header("Train:")]
    [SerializeField, Min(0.0001f)] public float trainSpeed = 30f;
    
    private float _trainPosition;
    private GameObject[] _rails;

    private void Start()
    {
        _rails = new GameObject[railsCount];
    }

    private void Update()
    {
        _trainPosition += trainSpeed * Time.deltaTime;
        
        for (int x = 0; x < railsCount; x++)
            SpawnRails(_trainPosition, x);
    }

    private void SpawnRails(float trainPosition, int railsIndex)
    {
        if (!_rails[railsIndex])
        {
            _rails[railsIndex] = Instantiate(railsObj);
            _rails[railsIndex].transform.parent = transform;
        }

        Vector3 move = Vector3.forward * (railsOffset - (trainPosition + railsIndex * railsScale) % (railsCount * railsScale));
        _rails[railsIndex].transform.position = transform.position + move;
    }
}
