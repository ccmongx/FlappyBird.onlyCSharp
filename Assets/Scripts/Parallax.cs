using UnityEngine;

public class Parallax : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    [SerializeField] private float animationSpeed = 1f;
    // [SerializeField] private float _dayLenght;

    // private TimeSpan _currentTime;
    // private float _minuteLenght => _dayLenght / WorldTimeConstants.MinutesInDay;

    // private void Start(){
    //     StartCoroutine(AddMinutes);
    // }

    // private IEnumerable AddMinutes(){
    //     _currentTime += TimeSpan.FromMinutes(1);
    //     yield return new WaitForSeconds(_minuteLenght);
    //     StartCoroutine(AddMinutes);
    // }
    private void Awake(){
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update(){
        meshRenderer.material.mainTextureOffset += new Vector2(animationSpeed * Time.deltaTime, 0);
    }
}
