using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class item : MonoBehaviour
{
    public GameObject objectToDestroy;
    public GameObject objectToDestroy1;
    public GameObject objectToDestroy2;
    public GameObject laserdoor;
    public Image checkpanel;
    public RawImage substance;
    public float fadeSpeed = 1f;
    public TextMeshProUGUI textmeshpro_objective_text;
    public TextMeshProUGUI  textmeshpro_objective_text1;
    public TextMeshProUGUI textmeshpro_objective_text2;
    private bool isFading = false;
    private int pickedItemsCount = 0;
    Audiomanager audiomanager;

    private void Awake()
    {
        audiomanager = GameObject.FindGameObjectWithTag("audio").GetComponent<Audiomanager>();
    }
    private void Start()
    {

        Allitems();

       

    }
    private void invisible()
    {
        float alphaPanel = Mathf.MoveTowards(checkpanel.color.a, 0, fadeSpeed * Time.deltaTime);
        checkpanel.color = new Color(checkpanel.color.r, checkpanel.color.g, checkpanel.color.b, alphaPanel);

        float alphaText = Mathf.MoveTowards(textmeshpro_objective_text.color.a, 0, fadeSpeed * Time.deltaTime);
        textmeshpro_objective_text.color = new Color(textmeshpro_objective_text.color.r, textmeshpro_objective_text.color.g, textmeshpro_objective_text.color.b, alphaText);

        float alphaText1 = Mathf.MoveTowards(textmeshpro_objective_text1.color.a, 0, fadeSpeed * Time.deltaTime);
        textmeshpro_objective_text1.color = new Color(textmeshpro_objective_text1.color.r, textmeshpro_objective_text1.color.g, textmeshpro_objective_text1.color.b, alphaText);

        float alphaText2 = Mathf.MoveTowards(textmeshpro_objective_text2.color.a, 0, fadeSpeed * Time.deltaTime);
        textmeshpro_objective_text2.color = new Color(textmeshpro_objective_text2.color.r, textmeshpro_objective_text2.color.g, textmeshpro_objective_text2.color.b, alphaText);


        float alphaSubstance = Mathf.MoveTowards(substance.color.a, 0, fadeSpeed * Time.deltaTime);
        substance.color = new Color(substance.color.r, substance.color.g, substance.color.b, alphaSubstance);

        if (alphaPanel <= 0f && alphaText <= 0f && alphaSubstance <= 0f && alphaText1 <= 0f && alphaText2 <= 0f)
        {
            isFading = false;
        }
    }

    private void Update()
    {
       
        if (objectToDestroy == null && objectToDestroy1 == null && objectToDestroy2 == null)
        {
            isFading = true;
            DestroyLaserDoor();
        }
        if (isFading)
        {
            invisible();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("item") )
        {
            audiomanager.PlaySFX(audiomanager.interactitem);
            pickedItemsCount++;
            UpdateObjectiveText();
            DestroyObject();
        }
        if (collision.gameObject.CompareTag("item1"))
        {
            audiomanager.PlaySFX(audiomanager.interactitem);
            pickedItemsCount++;
            UpdateObjectiveText();
            DestroyObject1();
        }
        if (collision.gameObject.CompareTag("item2"))
        {
            audiomanager.PlaySFX(audiomanager.interactitem);
            pickedItemsCount++;
            UpdateObjectiveText();
            DestroyObject2();
        }
    }
    private void DestroyObject()
    {
        if (objectToDestroy != null)
        {
            Destroy(objectToDestroy);
        }
    }
    private void DestroyObject1()
    {
        if (objectToDestroy1 != null)
        {
            Destroy(objectToDestroy1);
        }
    }

    private void DestroyObject2()
    {
        if (objectToDestroy2 != null)
        {
            Destroy(objectToDestroy2);
        }
    }
    private void DestroyLaserDoor()
    {
        if (laserdoor != null)
        {
            Destroy(laserdoor);
        }
    }

    private void Allitems()
    {
        int totalItemsWithTagItem = GameObject.FindGameObjectsWithTag("item").Length; 
        int totalItemsWithTagItem1 = GameObject.FindGameObjectsWithTag("item1").Length;
        int totalItemsWithTagItem2 = GameObject.FindGameObjectsWithTag("item2").Length;

        int totalItems = totalItemsWithTagItem + totalItemsWithTagItem1 + totalItemsWithTagItem2;
        textmeshpro_objective_text1.text = "/" + totalItems.ToString();
    }
    void UpdateObjectiveText()
    {
        


        textmeshpro_objective_text.text = pickedItemsCount.ToString(); 



    }


}
