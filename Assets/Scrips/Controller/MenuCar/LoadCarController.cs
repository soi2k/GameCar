using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadCarController : MonoBehaviour,ILoadCar
{
    public RectTransform rectTransform;
    public List<GameObject> carPrefabs;
    private List<int> availableCarIndices;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void LoadCar()
    {
        StartCoroutine(Load());
    }
    IEnumerator Load()
    {   
        availableCarIndices = new List<int>();

        for (int i = 0; i < carPrefabs.Count; i++)
        {
            availableCarIndices.Add(i);
        }

        for (int i = 0; i < carPrefabs.Count; i++)
        {
            int randomIndex = Random.Range(0, availableCarIndices.Count);
            int selectedIndex = availableCarIndices[randomIndex];
            availableCarIndices.RemoveAt(randomIndex);

            GameObject selectedCarPrefab = carPrefabs[selectedIndex];
            GameObject spawnedCar = Instantiate(selectedCarPrefab, rectTransform.position, Quaternion.identity);
            MenuManager.Instance.lstCar.Add(spawnedCar);
            RectTransform carRectTransform = spawnedCar.GetComponent<RectTransform>();
            Rigidbody2D rigidbody2D = spawnedCar.GetComponentInChildren<Rigidbody2D>();
            rigidbody2D.gravityScale = 1f;
            carRectTransform.SetParent(rectTransform);

            float xOffset = i * Constant.distanceBetweenCars;

            Vector3 carPosition = new Vector3(Constant.positionXGenerate + xOffset, Constant.positionYGenerate);
            carRectTransform.anchoredPosition = carPosition;
            carRectTransform.transform.localScale = Vector3.one;
            yield return new WaitForSeconds(0.2f);
        }
        yield return new WaitForSeconds(0.5f);
        MenuManager.Instance.blactiveText = true;
    }
}
