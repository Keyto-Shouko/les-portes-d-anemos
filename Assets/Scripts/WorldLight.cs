using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldLight : MonoBehaviour
{
    // we need to call the lightManager so it can gives us the timeElapsed
    private GameManager _gameManager;
    [SerializeField] private Gradient _gradient;
    private UnityEngine.Rendering.Universal.Light2D _light; 
    // set the duration of the whole cycle to be 5 minutes
    public float duration = 300f;

    private float _percentage;

    private float _timeElapsed;
    // Start is called before the first frame update

    public void SetTimeElapsed(float timeElapsed)
    {
        _timeElapsed = timeElapsed;
    }
    void Awake(){
        _gameManager = GameManager.Instance;
        _light = GetComponent<UnityEngine.Rendering.Universal.Light2D>();
        if(_gameManager == null){
            Debug.Log("gameManager in worldLight is null");
        }

    }
    void Start()
    {
        _timeElapsed = _gameManager.timeManager.GetTimeElapsed();
        Debug.Log("timeElapsed: " + _timeElapsed);
    }

    // Update is called once per frame
    void Update(){
        if(_gameManager.timeManager == null){
            Debug.Log("timeManager is null");
            return;
        }
        else if (_timeElapsed == null){
            Debug.Log("timeElapsed is null");
            return;
        }else{
        _timeElapsed = _gameManager.timeManager.GetTimeElapsed();
        _percentage = Mathf.Sin(_timeElapsed / duration * Mathf.PI * 2) *0.5f + 0.5f;
        _percentage = Mathf.Clamp01(_percentage);
        _light.color = _gradient.Evaluate(_percentage);
        }
       
    }
}
