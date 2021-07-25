using System.Collections;
using UnityEngine;

// CanvasGroup 컴포넌트의 alpha값을 이용해
// canvas가 fade out 되도록함 
public class CanvasFadeOut : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();        
    }

    public void PanelFadeOut()
    {
        StartCoroutine(DoFade());
    }

    // CanvaeGroup의 alpha값이 낮아지면서 fade out 되도록함 
    IEnumerator DoFade()
    {
        // 초기에 alpha값은 0이고 함수호출후 1로 만들어서 panel 보이도록한 후에 fade out       
        canvasGroup.alpha = 1;

        while(canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime / 2;
            yield return null;
        }
        // fadeout 이후 캔버스 상의 버튼 있다면 못누르도록 
        canvasGroup.interactable = false;
        yield return null;
    }
}
