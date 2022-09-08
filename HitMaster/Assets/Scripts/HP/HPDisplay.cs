using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPDisplay : MonoBehaviour
{
    [SerializeField] private HP hp;

    [SerializeField] private Image HPBar;
    [SerializeField] private Image HPImage;

    private void Start()
    {
        hp.OnChangeHP.AddListener(UpdateHPImage);
        hp.OnLostHP.AddListener(HideHPBar);
    }

    private void LateUpdate()
    {
        HPBar.transform.LookAt(Camera.main.transform.position);
        HPBar.transform.Rotate(0, 180, 0);
    }

    private void UpdateHPImage()
    {
        if (HPImage != null)
        {
            HPImage.fillAmount = hp.HPPercent;
            HPImage.color = Color.Lerp(Color.red, Color.green, hp.HPPercent);
        }
    }

    private void HideHPBar()
    {
        HPBar.gameObject.SetActive(false);
    }
}
