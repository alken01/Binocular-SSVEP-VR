using UnityEngine;

public class ExperimentVisibility : MonoBehaviour
{
    public bool IsVisible { get; private set; }

    private SSVEPManager ssvepManager;

    private void Awake()
    {
        ssvepManager = GetComponent<SSVEPManager>();
    }

    public void SetVisibility(bool visible)
    {
        IsVisible = visible;
        if (visible) ssvepManager.StartSSVEPManager();
        else ssvepManager.StopSSVEPManager();

        gameObject.SetActive(visible);
    }
}
