using UnityEngine;
using UnityEngine.UI;
public class LoadingCircle : MonoBehaviour {

    // Use this for initialization
    [SerializeField] Image fillImage;

    float currentValue = 0;

    private void OnEnable()
    {
        currentValue = 0;
    }

    // Update is called once per frame
    void Update () {
		if(currentValue>= 1)
        {
            currentValue = 0;
        }
        fillImage.fillAmount = currentValue / 1;
        currentValue += 0.01f;
	}
}
