using UnityEngine;

public class RailsSpawner : MonoBehaviour
{
    [SerializeField] private float railsScale = 2f;
    [SerializeField] private float trainScale = 5f;
    [SerializeField] private GameObject railsObj;
    [SerializeField] private Transform railsParent;
    [SerializeField] private int railsCount = 10;
    
    private GameObject[] _rails;

    private void SpawnRails(int zPosition)
    {
        if (zPosition < 0) return;
        int z = zPosition % railsCount;
        
        if (!_rails[z])
            _rails[z] = Instantiate(railsObj, new Vector3(0, 0, railsScale * z), Quaternion.identity, railsParent);
        
        _rails[z].transform.position = new Vector3(0, 0, railsScale * zPosition);
    }

    private void Start()
    {
        _rails = new GameObject[railsCount];
    }

    private void Update()
    {
        for (int z = 0; z < railsCount; z++)
        {
            int relativeTrainPosition = (int) ((transform.position.z - trainScale) / railsScale);
            SpawnRails(z + relativeTrainPosition);
        }
    }
}
