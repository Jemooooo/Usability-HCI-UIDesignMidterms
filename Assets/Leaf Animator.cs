using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LeafAnimator : MonoBehaviour
{
    public GameObject leaf; 

    
    public Button scaleButton;
    public Button zoomButton;
    public Button fadeUpButton;
    public Button fadeRightButton;
    public Button horizontalFlipButton;
    public Button verticalFlipButton;

    private Vector3 originalScale;
    private Vector3 flippedScaleHorizontal;
    private Vector3 flippedScaleVertical;
    private Vector3 originalPosition;

    
    private bool isScaled = false;
    private bool isZoomed = false;
    private bool isHorizontallyFlipped = false;
    private bool isVerticallyFlipped = false;

    void Start()
    {
        
        originalScale = leaf.transform.localScale;
        originalPosition = leaf.transform.position;

        
        flippedScaleHorizontal = new Vector3(-originalScale.x, originalScale.y, originalScale.z);
        flippedScaleVertical = new Vector3(originalScale.x, -originalScale.y, originalScale.z);

        
        scaleButton.onClick.AddListener(() => StartCoroutine(ToggleScaleLeaf(0f, 0.5f)));
        zoomButton.onClick.AddListener(() => StartCoroutine(ToggleZoomLeaf(2f, 0.5f)));
        fadeUpButton.onClick.AddListener(() => StartCoroutine(FadeUpLeaf()));
        fadeRightButton.onClick.AddListener(() => StartCoroutine(FadeRightLeaf()));
        horizontalFlipButton.onClick.AddListener(() => StartCoroutine(ToggleHorizontalFlipLeaf(0.5f)));
        verticalFlipButton.onClick.AddListener(() => StartCoroutine(ToggleVerticalFlipLeaf(0.5f)));
    }

   
    IEnumerator ToggleScaleLeaf(float targetScaleMultiplier, float duration)
    {
        Vector3 targetScale = isScaled ? originalScale : originalScale * targetScaleMultiplier;
        Vector3 initialScale = leaf.transform.localScale;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            leaf.transform.localScale = Vector3.Lerp(initialScale, targetScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

       
        leaf.transform.localScale = targetScale;
        isScaled = !isScaled; 
    }

   
    IEnumerator ToggleZoomLeaf(float targetScaleMultiplier, float duration)
    {
        Vector3 targetScale = isZoomed ? originalScale : originalScale * targetScaleMultiplier;
        Vector3 initialScale = leaf.transform.localScale;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            leaf.transform.localScale = Vector3.Lerp(initialScale, targetScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        
        leaf.transform.localScale = targetScale;
        isZoomed = !isZoomed; 
    }

    
    IEnumerator FadeUpLeaf()
    {
        float moveDuration = 0.5f; 
        float elapsedTime = 0f;
        Vector3 initialPosition = leaf.transform.position;
        Vector3 targetPosition = initialPosition + new Vector3(0, 1, 0); 

        
        StartCoroutine(FadeLeaf(leaf, 1f, 0f, moveDuration));

        while (elapsedTime < moveDuration)
        {
            leaf.transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        
        leaf.transform.position = targetPosition;

        
        yield return new WaitForSeconds(0.5f);

        
        elapsedTime = 0f;
        while (elapsedTime < moveDuration)
        {
            leaf.transform.position = Vector3.Lerp(targetPosition, originalPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        
        leaf.transform.position = originalPosition;

       
        StartCoroutine(FadeLeaf(leaf, 0f, 1f, moveDuration));
    }

    
    IEnumerator FadeRightLeaf()
    {
        float moveDuration = 0.5f; 
        float elapsedTime = 0f;
        Vector3 initialPosition = leaf.transform.position;
        Vector3 targetPosition = initialPosition + new Vector3(1, 0, 0); 

        
        StartCoroutine(FadeLeaf(leaf, 1f, 0f, moveDuration));

        while (elapsedTime < moveDuration)
        {
            leaf.transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        
        leaf.transform.position = targetPosition;

        
        yield return new WaitForSeconds(0.5f);

        
        elapsedTime = 0f;
        while (elapsedTime < moveDuration)
        {
            leaf.transform.position = Vector3.Lerp(targetPosition, originalPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        
        leaf.transform.position = originalPosition;

        
        StartCoroutine(FadeLeaf(leaf, 0f, 1f, moveDuration));
    }

    
    IEnumerator ToggleHorizontalFlipLeaf(float duration)
    {
        Vector3 targetScale = isHorizontallyFlipped ? originalScale : flippedScaleHorizontal;
        Vector3 initialScale = leaf.transform.localScale;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            leaf.transform.localScale = Vector3.Lerp(initialScale, targetScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        
        leaf.transform.localScale = targetScale;
        isHorizontallyFlipped = !isHorizontallyFlipped;
    }

    
    IEnumerator ToggleVerticalFlipLeaf(float duration)
    {
        Vector3 targetScale = isVerticallyFlipped ? originalScale : flippedScaleVertical;
        Vector3 initialScale = leaf.transform.localScale;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            leaf.transform.localScale = Vector3.Lerp(initialScale, targetScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        
        leaf.transform.localScale = targetScale;
        isVerticallyFlipped = !isVerticallyFlipped; 
    }

    
    IEnumerator FadeLeaf(GameObject leaf, float startAlpha, float endAlpha, float duration)
    {
        SpriteRenderer spriteRenderer = leaf.GetComponent<SpriteRenderer>();
        Color color = spriteRenderer.color;
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            color.a = alpha;
            spriteRenderer.color = color;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        
        color.a = endAlpha;
        spriteRenderer.color = color;
    }
}
