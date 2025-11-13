using UnityEngine;
using UnityEngine.Rendering.Universal;

[DisallowMultipleComponent]
public class ProximityLight2D : MonoBehaviour
{
    public enum Mode { EnableDisable, DimIntensity }
    private float onDistance = 30f;
    private float offDistance = 30f;

    [Header("Target")]
    public Transform target;
    public bool useCameraAsTarget = false;
    public string playerTag = "Player";

    [Header("Modo")]
    public Mode mode = Mode.EnableDisable;

    

    [Header("Perf")]
    public float refreshInterval = 0.15f;

    [Header("Otros")]
    [Tooltip("Si es Global, no tiene sentido apagarla.")]
    public bool allowGlobal = false;

    private Light2D l2d;
    private float nextRefresh;
    private bool isOn;                
    private float currentIntensity;   

    void Awake()
    {
        l2d = GetComponent<Light2D>();
        if (l2d == null) Debug.LogWarning($"{name}: ProximityLight2D requiere Light2D.");
    }

    void Start()
    {
        if (target == null)
        {
            if (useCameraAsTarget && Camera.main != null)
                target = Camera.main.transform;
            else
            {
                var p = GameObject.FindGameObjectWithTag(playerTag);
                if (p) target = p.transform;
            }
        }

        if (!allowGlobal && l2d != null && l2d.lightType == Light2D.LightType.Global)
        {
            enabled = false; 
            return;
        }

        
        isOn = true;
        currentIntensity = l2d != null ? l2d.intensity : 1f;
    }

    void Update()
    {
        if (l2d == null || target == null) return;

        // Tick
        if (Time.unscaledTime >= nextRefresh)
        {
            nextRefresh = Time.unscaledTime + refreshInterval;

            float sqrDist = (target.position - transform.position).sqrMagnitude;
            float on2 = onDistance * onDistance;
            float off2 = offDistance * offDistance;

            
            if (isOn)
            {
                if (sqrDist > off2) isOn = false;
            }
            else
            {
                if (sqrDist < on2) isOn = true;
            }
        }

        
        if (mode == Mode.EnableDisable)
        {
            if (l2d.enabled != isOn) l2d.enabled = isOn;
        }
       
    }

#if UNITY_EDITOR
    void OnValidate()
    {
        if (offDistance < onDistance) offDistance = onDistance + 0.5f;
        refreshInterval = Mathf.Max(0.02f, refreshInterval);
    }
#endif
}

