using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthButoHealthButoCanvas : MonoBehaviour
{
    [SerializeField] private float timeToDrain = 0.25f;
    [SerializeField] private Gradient hpButoGradient;
    private Image hpButoImage;
    private float target = 1f;
    private Coroutine drainHpButoCoroutine;

    private Color newHpButo;
    private void Start()
    {
        hpButoImage = GetComponent<Image>();
        hpButoImage.color = hpButoGradient.Evaluate(target);
        CheckHealthBarGradient();
    }


    public void UpdateHealthBar(float maxHpButo, float hpButo)
    {
        target = hpButo / maxHpButo;
        drainHpButoCoroutine = StartCoroutine(DrainHealthBar());
        CheckHealthBarGradient();
    }

    private IEnumerator DrainHealthBar()
    {
        float fillAmount = hpButoImage.fillAmount;
        Color currentColor = hpButoImage.color;
        float elapsedTime = 0f;
        while (elapsedTime < timeToDrain)
        {
            elapsedTime += Time.deltaTime;
            hpButoImage.fillAmount = Mathf.Lerp(fillAmount, target,(elapsedTime / timeToDrain));

            hpButoImage.color = Color.Lerp(currentColor, newHpButo, (elapsedTime / timeToDrain));
            yield return null;
        }
    }

    private void CheckHealthBarGradient()
    {
        newHpButo = hpButoGradient.Evaluate(target);
    }
}
