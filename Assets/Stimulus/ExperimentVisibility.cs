using UnityEngine;

public class ExperimentVisibility : MonoBehaviour
{
    private SSVEPManager ssvepManager;

    private void Awake()
    {
        ssvepManager = GetComponent<SSVEPManager>();

    }

    public void SetVisibility(bool visible)
    {
        gameObject.SetActive(visible);
    }

}
