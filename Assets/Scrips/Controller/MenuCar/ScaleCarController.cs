using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleCarController : MonoBehaviour, IScaleCar
{
    GameObject car;
    private Vector3 targetScale = new Vector3(1.7f, 1.7f, 1.7f);

    private float scaleDuration = 0.7f;
    public void ScaleCar()
    {
        car = MenuManager.Instance.selectedCar;
        StartCoroutine(ScaleOverTime());
    }

    private IEnumerator ScaleOverTime()
    {
        float startTime = Time.time;
        Vector3 initialScale = car.transform.localScale;

        while (Time.time - startTime < scaleDuration)
        {
            float progress = (Time.time - startTime) / scaleDuration;
            car.transform.localScale = Vector3.Lerp(initialScale, targetScale, progress);
            yield return null;
        }
        car.transform.localScale = targetScale;
        MenuManager.Instance.blCompletedScale = true;
    }
}

